using BasicBot.Model;
using Microsoft.Bot.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicBot.LUISEntity
{
    public class LUISEntity
    {
        Employee emp = new Employee();

        // Project
        public Dictionary<int, string> EmployeeProjectEntities(RecognizerResult recognizerResult)
        {
            Dictionary<int, string> record = new Dictionary<int, string>();

            // recognizerResult.Entities returns type JObject.
            foreach (var entity in recognizerResult.Entities)
            {
                var employeeName = JObject.Parse(entity.Value.ToString())["employeeName"];
                var amtInNum = JObject.Parse(entity.Value.ToString())["amtInNumber"];

                // We will return info on the first entity found.
                if (employeeName != null)
                {
                    // use JsonConvert to convert entity.Value to a dynamic object.
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());

                    if (o.employeeName[0] != null)
                    {
                        // Find and return the entity type and score.
                        string employeesName = o.employeeName[0].text;

                        // string projectName = emp.GetProjectByName(employeesName);
                        record.Add(1, employeesName);
                        return record;
                    }
                }

                if (amtInNum != null)
                {
                        string result = emp.GetNoOfProjects().ToString();
                        record.Add(2, result);
                        return record;
                }
            }

            // No entities were found, empty string returned.
            return record;
        }

        // Upcoming Events
        public string EmployeeUpcomingEventsEntities(RecognizerResult recognizerResult)
        {
            var result = string.Empty;

            foreach (var entity in recognizerResult.Entities)
            {
                var employeeBirthDay = JObject.Parse(entity.Value.ToString())["birthday"];
                var employeeAnniversaries = JObject.Parse(entity.Value.ToString())["anniversary"];

                // We will return info on the first entity found.
                if (employeeBirthDay != null)
                {
                        result = "birthday";
                        return result;
                }

                if (employeeAnniversaries != null)
                {
                        result = "anniversary";
                        return result;
                }
            }

            // No entities were found, empty string returned.
            return result;
        }

        // Bday, Age
        public string EmployeeBirthDayEntities(RecognizerResult recognizerResult)
        {
            var result = string.Empty;

            foreach (var entity in recognizerResult.Entities)
            {
                var empName = JObject.Parse(entity.Value.ToString())["employeeName"];
                var empBDay = JObject.Parse(entity.Value.ToString())["birthday"];
                var empAge = JObject.Parse(entity.Value.ToString())["Age"];

                // We will return info on the first entity found.
                if (empName != null && empBDay != null)
                {
                    // use JsonConvert to convert entity.Value to a dynamic object.
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.employeeName[0] != null)
                    {
                        string employeeName = o.employeeName[0].text;
                        result = emp.GetEmployeeBDay(employeeName).ToString("MM/dd/yyyy");
                        return result;
                    }
                }

                if (empName != null && empAge != null)
                {
                    // use JsonConvert to convert entity.Value to a dynamic object.
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.employeeName[0] != null)
                    {
                        string employeeName = o.employeeName[0].text;
                        DateTime date = emp.GetEmployeeBDay(employeeName);
                        result = emp.GetAgeInYears(date).ToString();
                        return result;
                    }
                }
            }

            // No entities were found, empty string returned.
            return result;
        }

        // EmpAnniversaries
        public string EmployeeAnniversariesEntities(RecognizerResult recognizerResult)
        {
            var result = string.Empty;

            foreach (var entity in recognizerResult.Entities)
            {
                var employeeName = JObject.Parse(entity.Value.ToString())["employeeName"];
                var employeeAnniversary = JObject.Parse(entity.Value.ToString())["anniversary"];

                // We will return info on the first entity found.
                if (employeeName != null)
                {
                    // use JsonConvert to convert entity.Value to a dynamic object.
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.employeeName[0] != null)
                    {
                        string employeesName = o.employeeName[0].text;
                        result = emp.GetDateOfAnniversary(employeesName).ToString("MM/dd/yyyy");
                        if (result != null)
                        {
                            return result;
                        }
                        else
                        {
                            result = "Single";
                            return result;
                        }
                    }
                }

                if (employeeAnniversary != null)
                {
                    result = "anniversary";
                    return result;
                }
            }

            // No entities were found, empty string returned.
            return result;
        }

        // Full Address, Search Per City
        public Dictionary<int, string> EmployeeAddressEntities(RecognizerResult recognizerResult)
        {
            Dictionary<int, string> record = new Dictionary<int, string>();

            foreach (var entity in recognizerResult.Entities)
            {
                var employeeName = JObject.Parse(entity.Value.ToString())["employeeName"];
                var employeeLoc = JObject.Parse(entity.Value.ToString())["Address"];
                var employeeCity = JObject.Parse(entity.Value.ToString())["geographyV2_city"];

                if (employeeName != null)
                {
                    // use JsonConvert to convert entity.Value to a dynamic object.
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.employeeName[0] != null)
                    {
                        string employeesName = o.employeeName[0].text;
                        string address = emp.GetEmployeeAddressByName(employeesName);
                        record.Add(1, address);
                        return record;
                    }
                }

                if (employeeCity != null)
                {
                    // use JsonConvert to convert entity.Value to a dynamic object.
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.geographyV2_city[0] != null)
                    {
                        string city = o.geographyV2_city[0].text;
                        record.Add(2, city);
                        return record;
                    }
                }
            }

            // No entities were found, empty string returned.
            return record;
        }

        // Contact, Designation, Email, AllEmployeeBday
        public string EmployeeDetailEntities(RecognizerResult recognizerResult)
        {
            var result = string.Empty;

            foreach (var entity in recognizerResult.Entities)
            {
                var employeeName = JObject.Parse(entity.Value.ToString())["employeeName"];
                var employeeDesignation = JObject.Parse(entity.Value.ToString())["designation"];
                var employeePhoneno = JObject.Parse(entity.Value.ToString())["phoneno"];
                var employeeBday = JObject.Parse(entity.Value.ToString())["birthday"];
                var employeeEmail = JObject.Parse(entity.Value.ToString())["Email"];
                var employeeId = JObject.Parse(entity.Value.ToString())["EmployeeCode"];
                var amtInNum = JObject.Parse(entity.Value.ToString())["amtInNumber"];
                var employeeMale = JObject.Parse(entity.Value.ToString())["male"];
                var employeeFemale = JObject.Parse(entity.Value.ToString())["female"];

                // We will return info on the first entity found.
                if (employeeDesignation != null)
                {
                    // use JsonConvert to convert entity.Value to a dynamic object.
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.employeeName[0] != null)
                    {
                        string employeesName = o.employeeName[0].text;
                        result = emp.GetEmployeeDesignation(employeesName);
                        return result;
                    }
                }

                if (employeePhoneno != null)
                {
                    // use JsonConvert to convert entity.Value to a dynamic object.
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.employeeName[0] != null)
                    {
                        string employeesName = o.employeeName[0].text;
                        result = emp.GetEmployeePhno(employeesName);
                        return result;
                    }
                }

                if (employeeEmail != null)
                {
                    // use JsonConvert to convert entity.Value to a dynamic object.
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.employeeName[0] != null)
                    {
                        string employeesName = o.employeeName[0].text;
                        result = emp.GetEmailId(employeesName);
                        return result;
                    }
                }

                if (employeeId != null)
                {
                    // use JsonConvert to convert entity.Value to a dynamic object.
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.employeeName[0] != null)
                    {
                        string employeesName = o.employeeName[0].text;
                        result = emp.GetEmployeeIdByName(employeesName).ToString();
                        return result;
                    }
                }

                if (amtInNum != null)
                {
                    if (employeeMale != null)
                    {
                        result = emp.GetNoOfMaleEmployee().ToString();
                        return result;
                    }

                    if (employeeFemale != null)
                    {
                        result = emp.GetNoOfFemaleEmployee().ToString();
                        return result;
                    }
                }
            }

            // No entities were found, empty string returned.
            return result;
        }

        // Get Experience, No Of Experience
        public Dictionary<int, string> EmployeeExperienceEntities(RecognizerResult recognizerResult)
        {
            Dictionary<int, string> record = new Dictionary<int, string>();

            foreach (var entity in recognizerResult.Entities)
            {
                var employeeName = JObject.Parse(entity.Value.ToString())["employeeName"];
                var employeeExperience = JObject.Parse(entity.Value.ToString())["experience"];
                var employeeExpInc = JObject.Parse(entity.Value.ToString())["greater"];
                var employeeExpDec = JObject.Parse(entity.Value.ToString())["less"];
                var noOfExp = JObject.Parse(entity.Value.ToString())["number"];
                var employeeJoining = JObject.Parse(entity.Value.ToString())["joining"];

                if (employeeExperience != null)
                {
                    if (employeeName != null)
                    {
                        // use JsonConvert to convert entity.Value to a dynamic object.
                        dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                        if (o.employeeName[0] != null)
                        {
                            string employeesName = o.employeeName[0].text;
                            record.Add(1, employeesName);
                            return record;
                        }
                    }

                    if (employeeExpInc != null && noOfExp != null)
                    {
                        dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                        if (o.number[0] != null)
                        {
                            float empExp = o.number[0].text;
                            record.Add(4, empExp.ToString());
                            return record;
                        }
                    }

                    if (employeeExpDec != null && noOfExp != null)
                    {
                        dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                        if (o.number[0] != null)
                        {
                            float empExp = o.number[0].text;
                            record.Add(5, empExp.ToString());
                            return record;
                        }
                    }
                    else
                    {
                        record.Add(2, "Experience");
                        return record;
                    }
                }

                if (employeeJoining != null)
                {
                        // use JsonConvert to convert entity.Value to a dynamic object.
                        dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                        if (o.employeeName[0] != null)
                        {
                            string employeesName = o.employeeName[0].text;
                            record.Add(3, employeesName);
                            return record;
                        }
                 }
            }

            // No entities were found, empty string returned.
            return record;
        }

        // Get All Employees, No of Employees
        public Dictionary<int, string> GetAllEmployeeEntities(RecognizerResult recognizerResult)
        {
            Dictionary<int, string> record = new Dictionary<int, string>();

            foreach (var entity in recognizerResult.Entities)
            {
                var employeeName = JObject.Parse(entity.Value.ToString())["employeeName"];
                var amtInNum = JObject.Parse(entity.Value.ToString())["amtInNumber"];
                var employeeId = JObject.Parse(entity.Value.ToString())["number"];
                var employeeMale = JObject.Parse(entity.Value.ToString())["male"];
                var employeeFemale = JObject.Parse(entity.Value.ToString())["female"];

                // We will return info on the first entity found.
                if (amtInNum != null)
                {
                    string value = emp.GetNoOfEmployees().ToString();
                    record.Add(1, value);
                    return record;
                }

                if (employeeName != null)
                {
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.employeeName[0] != null)
                    {
                        string employeesName = o.employeeName[0].text;
                        record.Add(2, employeesName);
                        return record;
                    }
                }

                if (employeeId != null)
                {
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(entity.Value.ToString());
                    if (o.number[0] != null)
                    {
                        string employeesId = o.number[0].text;
                        record.Add(3, employeesId);
                        return record;
                    }
                }

                if (employeeMale != null)
                {
                    record.Add(4, "male");
                    return record;
                }

                if (employeeFemale != null)
                {
                    record.Add(5, "female");
                    return record;
                }
                else
                {
                    record.Add(6, "list");
                    return record;
                }
            }

            // No entities were found, empty string returned.
            return record;
        }

        // Get All Trainee, Number of Trainee
        public string GetAllTraineeEntities(RecognizerResult recognizerResult)
        {
            var result = string.Empty;
            foreach (var entity in recognizerResult.Entities)
            {
                var trainee = JObject.Parse(entity.Value.ToString())["trainee"];
                var amtInNum = JObject.Parse(entity.Value.ToString())["amtInNumber"];

                if (amtInNum != null)
                {
                        result = emp.GetNoOfTrainee().ToString();
                        return result;
                }
                else
                {
                    result = "trainee";
                    return result;
                }
            }

            // No entities were found, empty string returned.
            return result;
        }
    }
}