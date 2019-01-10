using BasicBot.Cards;
using BasicBot.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Schema;
using cards = BasicBot.Cards;

namespace BasicBot.AttachmentCard
{
    public class AttachmentCard
    {
        Employee emp = new Employee();

        // Welcome Card
        public Attachment IDSWelcomeCard()
        {
            var adaptiveCard = File.ReadAllText(@".\Dialogs\Welcome\Resources\welcomeCard.json");
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCard),
            };
        }

        // All Employee Detail
        public Attachment EmployeeListCard()
        {
            var adaptiveCard = File.ReadAllText(@".\Dialogs\Welcome\Resources\EmpList.json");
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCard),
            };
        }

        // Helpline
        public Attachment HelplineCard()
        {
            var adaptiveCard = File.ReadAllText(@".\Dialogs\Welcome\Resources\Helpline.json");
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCard),
            };
        }

        // All Employees
        public Attachment AllEmployeesCard()
        {
            var rootObject = new RootObject();
            var employees = emp.GetEmployee();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**EmpID**",
            });

            // Column for Employee Name[1]
            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**EmpName**",
            });

            foreach (var item in employees)
            {
                // Column for Employee Code[0]
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                // Column for Employee Name[1]
                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get Project with Employees
        public Attachment GetProject()
        {
            var rootObject = new RootObject();
            var employees = emp.GetEmployee();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Project**",
            });

            foreach (var item in employees)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });
                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Projects,
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get Upcoming Birthday
        public Attachment GetUpcomingBirthday()
        {
            var rootObject = new RootObject();
            var employees = emp.GetUpComingBday();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });
            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Birthday**",
            });

            foreach (var item in employees)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });
                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOB.ToString("MM/dd/yyyy"),
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get Upcoming Anniversaries
        public Attachment GetUpcomingAnniversaries()
        {
            var rootObject = new RootObject();
            var employees = emp.GetUpComingAnniversaries();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });
            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Anniversaries**",
            });

            foreach (var item in employees)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });
                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOA.ToString("MM/dd/yyyy"),
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get All Trainees
        public Attachment GetTrainees()
        {
            var rootObject = new RootObject();
            var employees = emp.GetAllTrainee();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Trainee Id**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Trainee Name**",
            });

            foreach (EmployeeModel item in employees)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get Max Employee Experience
        public Attachment GetEmployeeMax(float exp)
        {
            var rootObject = new RootObject();
            var employees = emp.GetMaxEmployeeExp(exp);
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Id[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "15",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "25",
                items = null,
            });

            // Column for Employee Experience[2]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();
            columns[2].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee ID**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            columns[2].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Experience**",
            });

            foreach (EmployeeModel item in employees)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

                columns[2].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Experience.ToString(),
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get Min Employee Experience
        public Attachment GetEmployeeMin(float exp)
        {
            var rootObject = new RootObject();
            var employees = emp.GetMinEmployeeExp(exp);
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Id[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();
            columns[2].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee ID**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            columns[2].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Experience**",
            });

            foreach (EmployeeModel item in employees)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

                columns[2].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Experience.ToString(),
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get All Anniversaries
        public Attachment GetEmployeeAnniversaries()
        {
            var rootObject = new RootObject();
            var employees = emp.GetAllEmpAnniversary();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Anniversary**",
            });

            foreach (EmployeeModel item in employees)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOA.ToString("MM/dd/yyyy"),
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get All DateOfJoining
        public Attachment GetEmployeeJoining()
        {
            var rootObject = new RootObject();
            var employees = emp.GetEmployee();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee DateOfJoining**",
            });

            foreach (EmployeeModel item in employees)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOJ.ToString("MM/dd/yyyy"),
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get Employee Details by Name
        public Attachment GetEmployeeDetailsByName(string name)
        {
            var rootObject = new RootObject();
            var employees = emp.GetEmployeeDetailByName(name);
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Id**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Name**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Designation**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Address**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**PhoneNo**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Experience**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**DateOfBirth**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**DateOfJoining**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**DateOfAnniversary**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Project**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Email Id**",
            });

            foreach (EmployeeModel item in employees)
            {
                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Designation,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Address,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.ContactNo,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Experience.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOB.ToString("MM/dd/yyyy"),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOJ.ToString("MM/dd/yyyy"),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOA.ToString("MM/dd/yyyy"),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Projects,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmailId,
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get Employee Details By Id
        public Attachment GetEmployeeDetailsById(int id)
        {
            var rootObject = new RootObject();
            var employees = emp.GetEmployeeDetailsById(id);
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Id**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Name**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Designation**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Address**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**PhoneNo**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Experience**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**DateOfBirth**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**DateOfJoining**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**DateOfAnniversary**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Project**",
            });

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Email Id**",
            });

            foreach (EmployeeModel item in employees)
            {
                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Designation,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Address,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.ContactNo,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Experience.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOB.ToString("MM/dd/yyyy"),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOJ.ToString("MM/dd/yyyy"),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOA.ToString("MM/dd/yyyy"),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Projects,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmailId,
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get Employee By City
        public Attachment GetEmployeeByCity(string city)
        {
            var rootObject = new RootObject();
            var employees = emp.GetEmployeeByCity(city);
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Id**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            foreach (EmployeeModel item in employees)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get All Projects
        public Attachment GetAllProjects()
        {
            var rootObject = new RootObject();
            var projects = emp.GetEmployee();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Project Name**",
            });

            foreach (EmployeeModel item in projects)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Projects,
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get Max Age
        public Attachment GetAgeMax(int age)
        {
            var rootObject = new RootObject();
            var projects = emp.GetAgeMax(age);
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Code[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Age**",
            });

            foreach (KeyValuePair<string, int?> item in projects)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Key,
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Value.ToString(),
                });

            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get All Male Employees
        public Attachment GetMaleEmployees()
        {
            var rootObject = new RootObject();
            var projects = emp.GetMaleEmployees();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Code[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Id**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            foreach (EmployeeModel item in projects)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get All Female Employees
        public Attachment GetFemaleEmployees()
        {
            var rootObject = new RootObject();
            var projects = emp.GetFemaleEmployees();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Code[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Id**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            foreach (EmployeeModel item in projects)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get All Employee Experience
        public Attachment GetEmployeeExperience()
        {
            var rootObject = new RootObject();
            var projects = emp.GetEmployee();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "15",
                items = null,
            });

            // Column for Employee Code[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();
            columns[2].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Id**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Name**",
            });

            columns[2].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Employee Experience**",
            });

            foreach (EmployeeModel item in projects)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

                columns[2].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Experience.ToString(),
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }

        // Get All Employee Birthday
        public Attachment GetEmployeeBirthday()
        {
            var rootObject = new RootObject();
            var projects = emp.GetEmployee();
            rootObject.type = "AdaptiveCard";
            rootObject.version = "1.0";
            rootObject.body = new List<Body>();

            var columns = new List<cards.Column>();

            // Column for Employee Code[0]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "15",
                items = null,
            });

            // Column for Employee Name[1]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            // Column for Employee Birthday[2]
            columns.Add(new cards.Column()
            {
                type = "Column",
                width = "20",
                items = null,
            });

            columns[0].items = new List<Item>();
            columns[1].items = new List<Item>();
            columns[2].items = new List<Item>();

            columns[0].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Id**",
            });

            columns[1].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Name**",
            });

            columns[2].items.Add(new Item()
            {
                type = "TextBlock",
                horizontalAlignment = "left",
                wrap = false,
                text = "**Birthday**",
            });

            foreach (EmployeeModel item in projects)
            {
                columns[0].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.EmployeeId.ToString(),
                });

                columns[1].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.Name,
                });

                columns[2].items.Add(new Item()
                {
                    type = "TextBlock",
                    horizontalAlignment = "left",
                    wrap = false,
                    text = item.DOB.ToString("MM/dd/yyyy"),
                });
            }

            rootObject.body.Add(new Body()
            {
                type = "ColumnSet",
                columns = columns,
            });

            string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject<RootObject>(json),
            };
        }
    }
}
