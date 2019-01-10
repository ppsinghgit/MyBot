// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BasicBot.Model;
using AdaptiveCards;
using BasicBot.Cards;
using BasicBot.Model;
using cards = BasicBot.Cards;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using BasicBot.AttachmentCard;
using BasicBot.LUISEntity;

namespace Microsoft.BotBuilderSamples
{
    /// <summary>
    /// Main entry point and orchestration for bot.
    /// </summary>
    public class BasicBot : IBot
    {
        LUISEntity entity = new LUISEntity();
        Employee emp = new Employee();
        AttachmentCard attachment = new AttachmentCard();

        // Supported LUIS Intents
        public const string GreetingIntent = "Greeting";
        public const string CancelIntent = "Cancel";
        public const string HelpIntent = "Help";
        public const string NoneIntent = "None";
        public const string GetWeather = "GetWeather";
        public const string EmergencyNumber = "EmergencyNumber";
        public const string GetAllEmployee = "GetAllEmployee";
        public const string EmployeeDetail = "EmployeeDetail";
        public const string EmployeeExperience = "EmployeeExperience";
        public const string EmployeeAddress = "EmployeeAddress";
        public const string EmployeeBirthDay = "EmployeeBirthDay";
        public const string EmployeeUpcomingEvents = "EmployeeUpcomingEvents";
        public const string EmployeeProject = "EmployeeProject";
        public const string EmployeeAnniversaries = "EmployeeAnniversaries";
        public const string GetTrainee = "GetTrainee";

        /// <summary>
        /// Key in the bot config (.bot file) for the LUIS instance.
        /// In the .bot file, multiple instances of LUIS can be configured.
        /// </summary>
        public static readonly string LuisConfiguration = "IdsWebAppBot-bede";
        private readonly IStatePropertyAccessor<GreetingState> _greetingStateAccessor;
        private readonly IStatePropertyAccessor<DialogState> _dialogStateAccessor;
        private readonly UserState _userState;
        private readonly ConversationState _conversationState;
        private readonly BotServices _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicBot"/> class.
        /// </summary>
        /// <param name="botServices">Bot services.</param>
        /// <param name="accessors">Bot State Accessors.</param>
        public BasicBot(BotServices services, UserState userState, ConversationState conversationState, ILoggerFactory loggerFactory)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _userState = userState ?? throw new ArgumentNullException(nameof(userState));
            _conversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));

            _greetingStateAccessor = _userState.CreateProperty<GreetingState>(nameof(GreetingState));
            _dialogStateAccessor = _conversationState.CreateProperty<DialogState>(nameof(DialogState));

            // Verify LUIS configuration.
            if (!_services.LuisServices.ContainsKey(LuisConfiguration))
            {
                throw new InvalidOperationException($"The bot configuration does not contain a service type of `luis` with the id `{LuisConfiguration}`.");
            }

            Dialogs = new DialogSet(_dialogStateAccessor);
            Dialogs.Add(new GreetingDialog(_greetingStateAccessor, loggerFactory));
        }

        private DialogSet Dialogs { get; set; }

        /// <summary>
        /// Run every turn of the conversation. Handles orchestration of messages.
        /// </summary>
        /// <param name="turnContext">Bot Turn Context.</param>
        /// <param name="cancellationToken">Task CancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var activity = turnContext.Activity;

            // Create a dialog context
            var dc = await Dialogs.CreateContextAsync(turnContext);

            if (activity.Type == ActivityTypes.Message)
            {
                // Perform a call to LUIS to retrieve results for the current activity message.
                var luisResults = await _services.LuisServices[LuisConfiguration].RecognizeAsync(dc.Context, cancellationToken);

                // If any entities were updated, treat as interruption.
                // For example, "no my name is tony" will manifest as an update of the name to be "tony".
                var topScoringIntent = luisResults?.GetTopScoringIntent();

                var topIntent = topScoringIntent.Value.intent;

                // update greeting state with any entities captured
                await UpdateGreetingState(luisResults, dc.Context);

                // Handle conversation interrupts first.
                var interrupted = await IsTurnInterruptedAsync(dc, topIntent, turnContext, cancellationToken);
                if (interrupted)
                {
                    // Bypass the dialog.
                    // Save state before the next turn.
                    await _conversationState.SaveChangesAsync(turnContext);
                    await _userState.SaveChangesAsync(turnContext);
                    return;
                }

                // Continue the current dialog
                var dialogResult = await dc.ContinueDialogAsync();

                // if no one has responded,
                if (!dc.Context.Responded)
                {
                    // examine results from active dialog
                    switch (dialogResult.Status)
                    {
                        case DialogTurnStatus.Empty:
                            switch (topIntent)
                            {
                                case GreetingIntent:
                                    await dc.Context.SendActivityAsync("Hi Ankesh! Ask me questions about your team");

                                    // await dc.BeginDialogAsync(nameof(GreetingDialog));
                                    break;

                                case NoneIntent:
                                default:
                                    // Help or no intent identified, either way, let's provide some help.
                                    // to the user
                                    await dc.Context.SendActivityAsync("I didn't understand what you just said to me.");
                                    break;
                            }

                            break;

                        case DialogTurnStatus.Waiting:
                            // The active dialog is waiting for a response from the user, so do nothing.
                            break;

                        case DialogTurnStatus.Complete:
                            await dc.EndDialogAsync();
                            break;

                        default:
                            await dc.CancelAllDialogsAsync();
                            break;
                    }
                }
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                if (activity.MembersAdded != null)
                {
                    // Iterate over all new members added to the conversation.
                    foreach (var member in activity.MembersAdded)
                    {
                        // Greet anyone that was not the target (recipient) of this message.
                        // To learn more about Adaptive Cards, see https://aka.ms/msbot-adaptivecards for more details.
                        if (member.Id != activity.Recipient.Id)
                        {
                            var welcomeCard = attachment.IDSWelcomeCard();
                            var response = CreateResponse(activity, welcomeCard);
                            await dc.Context.SendActivityAsync(response);
                        }
                    }
                }
            }

            await _conversationState.SaveChangesAsync(turnContext);
            await _userState.SaveChangesAsync(turnContext);
        }

        // Determine if an interruption has occurred before we dispatch to any active dialog.
        private async Task<bool> IsTurnInterruptedAsync(DialogContext dc, string topIntent, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var luisResults = await _services.LuisServices[LuisConfiguration].RecognizeAsync(dc.Context, cancellationToken);
            var activity = turnContext.Activity;

            // See if there are any conversation interrupts we need to handle.
            if (topIntent.Equals(CancelIntent))
            {
                if (dc.ActiveDialog != null)
                {
                    await dc.CancelAllDialogsAsync();
                    await dc.Context.SendActivityAsync("Ok. I've canceled our last activity.");
                }
                else
                {
                    await dc.Context.SendActivityAsync("I don't have anything to cancel.");
                }

                return true;        // Handled the interrupt.
            }

            // Weather Update
            if (topIntent.Equals(GetWeather))
            {
                var reply = turnContext.Activity.CreateReply();

                // Create an attachment.
                var attachment = new Attachment
                {
                    ContentUrl = "https://imagejournal.org/wp-content/uploads/bb-plugin/cache/23466317216_b99485ba14_o-panorama.jpg",
                    ContentType = "image/jpg",
                };

                // Add the attachment to our reply.
                reply.Attachments = new List<Attachment>() { attachment };

                // Send the activity to the user.
                await turnContext.SendActivityAsync(reply);

                // await dc.Context.SendActivityAsync("Getting weather info..");
                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;
            }

            // EmergencyNo
            if (topIntent.Equals(EmergencyNumber))
            {
                if (turnContext.Activity.Text.Contains("police"))
                {
                    await dc.Context.SendActivityAsync("Police Helpline No: 100");
                }
                else if (turnContext.Activity.Text.Contains("fire"))
                {
                    await dc.Context.SendActivityAsync("Fire Helpline No: 101");
                }
                else if (turnContext.Activity.Text.Contains("women"))
                {
                    await dc.Context.SendActivityAsync("Women Helpline No: 1091");
                }
                else
                {
                    var card = attachment.HelplineCard();
                    var response = CreateResponse(activity, card);
                    await dc.Context.SendActivityAsync(response);
                }

                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;
            }

            // Help Intent
            if (topIntent.Equals(HelpIntent))
            {
                await dc.Context.SendActivityAsync("Let me try to provide some help.");
                await dc.Context.SendActivityAsync("I understand greetings, being asked for help, or being asked to cancel what I am doing.");
                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;        // Handled the interrupt.
            }

            // Get All Employees
            if (topIntent.Equals(GetAllEmployee))
            {
                string empName = turnContext.Activity.Text;
                var employee = emp.GetEmployeeListByName(empName);

                if (employee.Count > 1)
                {
                    var reply = turnContext.Activity.CreateReply("Which employee do you want to know?");

                    reply.SuggestedActions = new SuggestedActions();
                    reply.SuggestedActions.Actions = new List<CardAction>();
                    foreach (EmployeeModel model in employee)
                    {
                    reply.SuggestedActions.Actions.Add
                    (
                    new CardAction() { Title = model.Name, Type = ActionTypes.ImBack, Value = model.Name }
                    );
                    }

                    await turnContext.SendActivityAsync(reply, cancellationToken);
                }
                else
                {
                    var entityFound = entity.GetAllEmployeeEntities(luisResults);
                    foreach (KeyValuePair<int, string> data in entityFound)
                    {
                    if (data.Key == 1)
                    {
                        await dc.Context.SendActivityAsync(data.Value);
                    }
                    else if (data.Key == 2)
                    {
                        var card = attachment.GetEmployeeDetailsByName(data.Value);
                        var response = CreateResponse(activity, card);
                        await dc.Context.SendActivityAsync(response);
                    }
                    else if (data.Key == 3)
                    {
                        int id = int.Parse(data.Value);
                        int employeeId = emp.GetEmployeeId(id);
                        if (employeeId != 0)
                            {
                                var card = attachment.GetEmployeeDetailsById(id);
                                var response = CreateResponse(activity, card);
                                await dc.Context.SendActivityAsync(response);
                            }
                            else
                            {
                                await dc.Context.SendActivityAsync("Please enter valid Employee Id");
                            }
                    }
                    else if (data.Key == 4)
                    {
                         var card = attachment.GetMaleEmployees();
                         var response = CreateResponse(activity, card);
                         await dc.Context.SendActivityAsync(response);
                    }
                    else if (data.Key == 5)
                    {
                         var card = attachment.GetFemaleEmployees();
                         var response = CreateResponse(activity, card);
                         await dc.Context.SendActivityAsync(response);
                    }
                    else if (data.Key == 6)
                    {
                         var card = attachment.AllEmployeesCard();
                         var response = CreateResponse(activity, card);
                         await dc.Context.SendActivityAsync(response);
                    }
                    }
                }

                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;        // Handled the interrupt.
            }

            // Upcoming Birthday, Anniversaries
            if (topIntent.Equals(EmployeeUpcomingEvents))
            {
                if (turnContext.Activity.Text.ToLower() == "upcoming")
                {
                    var reply = turnContext.Activity.CreateReply("Which upcoming event do you want to know?");

                    reply.SuggestedActions = new SuggestedActions()
                    {
                        Actions = new List<CardAction>()
                    {
                         new CardAction() { Title = "Upcoming Anniversary", Type = ActionTypes.ImBack, Value = "Upcoming Anniversary" },
                         new CardAction() { Title = "Upcoming Birthday", Type = ActionTypes.ImBack, Value = "Upcoming Birthday" },
                    },
                    };
                    await turnContext.SendActivityAsync(reply, cancellationToken);
                }
                else
                {
                    var entityFound = entity.EmployeeUpcomingEventsEntities(luisResults);

                    if (entityFound.ToString().Equals("birthday"))
                    {
                        var card = attachment.GetUpcomingBirthday();
                        var response = CreateResponse(activity, card);
                        await dc.Context.SendActivityAsync(response);
                    }
                    else if (entityFound.ToString().Equals("anniversary"))
                    {
                        var card = attachment.GetUpcomingAnniversaries();
                        var response = CreateResponse(activity, card);
                        await dc.Context.SendActivityAsync(response);
                    }
                }

                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;        // Handled the interrupt.
            }

            // Age, DOB
            if (topIntent.Equals(EmployeeBirthDay))
            {
                var entityFound = entity.EmployeeBirthDayEntities(luisResults);

                if (entityFound.ToString() != string.Empty)
                {
                    await turnContext.SendActivityAsync(entityFound);
                }

                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;        // Handled the interrupt.
            }

            // All Projects, Specific Project
            if (topIntent.Equals(EmployeeProject))
            {
                var entityFound = entity.EmployeeProjectEntities(luisResults);

                if (entityFound != null)
                {
                    foreach (KeyValuePair<int, string> project in entityFound)
                    {
                        if (project.Key == 1)
                        {
                            string employeeName = project.Value;
                            var employee = emp.GetEmployeeListByName(employeeName);

                            if (employee.Count > 1)
                            {
                                var reply = turnContext.Activity.CreateReply("Which employee do you want to know?");

                                reply.SuggestedActions = new SuggestedActions();
                                reply.SuggestedActions.Actions = new List<CardAction>();
                                foreach (EmployeeModel model in employee)
                                {
                                    reply.SuggestedActions.Actions.Add
                                    (
                                    new CardAction() { Title = model.Name, Type = ActionTypes.ImBack, Value = "projects of " + model.Name }
                                    );
                                }

                                await turnContext.SendActivityAsync(reply, cancellationToken);
                            }
                            else
                            {
                                string projectName = emp.GetProjectByName(project.Value);
                                await turnContext.SendActivityAsync($"{projectName}\n");
                            }
                        }
                        else if (project.Key == 2)
                        {
                            await turnContext.SendActivityAsync($"Total Number of Projects are: {project.Value}\n");
                        }
                    }
                }
                else
                {
                    var card = attachment.GetProject();
                    var response = CreateResponse(activity, card);
                    await dc.Context.SendActivityAsync(response);
                }

                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;        // Handled the interrupt.
            }

            // Designation, Last company, Email, Detail Info, Contact No
            if (topIntent.Equals(EmployeeDetail))
            {
                var entityFound = entity.EmployeeDetailEntities(luisResults);

                if (entityFound.ToString() != string.Empty)
                {
                    await turnContext.SendActivityAsync($"{entityFound}\n");
                }

                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;        // Handled the interrupt.
            }

            // Anniversaries
            if (topIntent.Equals(EmployeeAnniversaries))
            {
                var entityFound = entity.EmployeeAnniversariesEntities(luisResults);

                if (entityFound.ToString() != string.Empty)
                {
                    if (entityFound.ToString().Equals("anniversary"))
                    {
                        var card = attachment.GetEmployeeAnniversaries();
                        var response = CreateResponse(activity, card);
                        await dc.Context.SendActivityAsync(response);
                    }
                    else if (entityFound.ToString() != "anniversary")
                    {
                        await turnContext.SendActivityAsync($"{entityFound}\n");
                    }
                }

                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;        // Handled the interrupt.
            }

            // Full Address, Search by City
            if (topIntent.Equals(EmployeeAddress))
            {
                var entityFound = entity.EmployeeAddressEntities(luisResults);

                foreach (KeyValuePair<int, string> employee in entityFound)
                {
                    if (employee.Key == 1)
                    {
                        await dc.Context.SendActivityAsync($"{employee.Value}");
                    }
                    else if (employee.Key == 2)
                    {
                        var card = attachment.GetEmployeeByCity(employee.Value);
                        var response = CreateResponse(activity, card);
                        await dc.Context.SendActivityAsync(response);
                    }
                    else
                    {
                        await dc.Context.SendActivityAsync("No entity found");
                    }
                }

                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;        // Handled the interrupt.
            }

            // DOJ, Experience Per Employee
            if (topIntent.Equals(EmployeeExperience))
            {
                Dictionary<int, string> record = entity.EmployeeExperienceEntities(luisResults);
                foreach (KeyValuePair<int, string> data in record)
                {
                    if (data.Value != null)
                    {
                        if (data.Key == 1)
                        {
                            string employeeName = data.Value;
                            var employee = emp.GetEmployeeListByName(employeeName);

                            if (employee.Count > 1)
                            {
                                var reply = turnContext.Activity.CreateReply("Which employee do you want to know?");

                                reply.SuggestedActions = new SuggestedActions();
                                reply.SuggestedActions.Actions = new List<CardAction>();
                                foreach (EmployeeModel model in employee)
                                {
                                    reply.SuggestedActions.Actions.Add
                                    (
                                    new CardAction() { Title = model.Name, Type = ActionTypes.ImBack, Value = "experience of " + model.Name }
                                    );
                                }

                                await turnContext.SendActivityAsync(reply, cancellationToken);
                            }
                            else
                            {
                                float exp = emp.GetEmployeeExperience(data.Value);
                                if (exp == 0)
                                {
                                    await turnContext.SendActivityAsync(data.Value + " is a Trainee");
                                }
                                else
                                {
                                    await turnContext.SendActivityAsync($"{exp}" + " years");
                                }
                            }
                        }
                        else if (data.Key == 2)
                        {
                            var card = attachment.GetEmployeeExperience();
                            var response = CreateResponse(activity, card);
                            await dc.Context.SendActivityAsync(response);
                        }
                        else if (data.Key == 3)
                        {
                            string empName = data.Value;
                            var employee = emp.GetEmployeeListByName(empName);

                            if (employee.Count > 1)
                            {
                                var reply = turnContext.Activity.CreateReply("Which employee do you want to know?");

                                reply.SuggestedActions = new SuggestedActions();
                                reply.SuggestedActions.Actions = new List<CardAction>();
                                foreach (EmployeeModel model in employee)
                                {
                                    reply.SuggestedActions.Actions.Add
                                    (
                                    new CardAction() { Title = model.Name, Type = ActionTypes.ImBack, Value = "doj of " + model.Name }
                                    );
                                }

                                await turnContext.SendActivityAsync(reply, cancellationToken);
                            }
                            else
                            {
                                DateTime doj = emp.GetEmployeeDOJ(data.Value);
                                string date = doj.ToString("MM/dd/yyyy");
                                await turnContext.SendActivityAsync($"{date}");
                            }
                        }
                        else if (data.Key == 4)
                        {
                            float experience = float.Parse(data.Value);
                            var card = attachment.GetEmployeeMax(experience);
                            var response = CreateResponse(activity, card);
                            await dc.Context.SendActivityAsync(response);
                        }
                        else if (data.Key == 5)
                        {
                            float experience = float.Parse(data.Value);
                            var card = attachment.GetEmployeeMin(experience);
                            var response = CreateResponse(activity, card);
                            await dc.Context.SendActivityAsync(response);
                        }
                    }
                }

                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;        // Handled the interrupt.
            }

            // Get Trainee
            if (topIntent.Equals(GetTrainee))
            {
                var entityFound = entity.GetAllTraineeEntities(luisResults);

                if (entityFound.ToString() != "trainee")
                {
                    await dc.Context.SendActivityAsync($"{entityFound}");
                }
                else if (entityFound.ToString().Equals("trainee"))
                {
                    var card = attachment.GetTrainees();
                    var response = CreateResponse(activity, card);
                    await dc.Context.SendActivityAsync(response);
                }

                if (dc.ActiveDialog != null)
                {
                    await dc.RepromptDialogAsync();
                }

                return true;        // Handled the interrupt.
            }

            return false;           // Did not handle the interrupt.
        }

        // Create an attachment message response.
        private Activity CreateResponse(Activity activity, Attachment attachment)
        {
            var response = activity.CreateReply();
            response.Attachments = new List<Attachment>() { attachment };
            return response;
        }

        /// <summary>
        /// Helper function to update greeting state with entities returned by LUIS.
        /// </summary>
        /// <param name="luisResult">LUIS recognizer <see cref="RecognizerResult"/>.</param>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        private async Task UpdateGreetingState(RecognizerResult luisResult, ITurnContext turnContext)
        {
            if (luisResult.Entities != null && luisResult.Entities.HasValues)
            {
                // Get latest GreetingState
                var greetingState = await _greetingStateAccessor.GetAsync(turnContext, () => new GreetingState());
                var entities = luisResult.Entities;

                // Supported LUIS Entities
                string[] userNameEntities = { "userName", "userName_patternAny" };
                string[] userLocationEntities = { "userLocation", "userLocation_patternAny" };

                // Update any entities
                // Note: Consider a confirm dialog, instead of just updating.
                foreach (var name in userNameEntities)
                {
                    // Check if we found valid slot values in entities returned from LUIS.
                    if (entities[name] != null)
                    {
                        // Capitalize and set new user name.
                        var newName = (string)entities[name][0];
                        greetingState.Name = char.ToUpper(newName[0]) + newName.Substring(1);
                        break;
                    }
                }

                foreach (var city in userLocationEntities)
                {
                    if (entities[city] != null)
                    {
                        // Capitalize and set new city.
                        var newCity = (string)entities[city][0];
                        greetingState.City = char.ToUpper(newCity[0]) + newCity.Substring(1);
                        break;
                    }
                }

                // Set the new values into state.
                await _greetingStateAccessor.SetAsync(turnContext, greetingState);
            }
        }
    }
}
