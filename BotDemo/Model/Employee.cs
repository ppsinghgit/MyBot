using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicBot.Model
{
    public class Employee
    {
        public List<EmployeeModel> GetEmployee()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27123,
                Name = "Parminder Pal Singh",
                Address = "Jalandhar",
                ContactNo = "9988545755",
                Experience = 5.5f,
                DOB = new DateTime(1989, 03, 07),
                DOJ = new DateTime(2018, 08, 29),
                Projects = "Amuze,IOT,Bot",
                Designation = "SSE",
                EmailId = "parminderpal.s@idsil.com",
                IsMarried = false,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27124,
                Name = "Vidush Kandpal",
                Address = "Mani Majra, Chandigarh",
                ContactNo = "9814673439",
                DOB = new DateTime(1992, 01, 15),
                DOJ = new DateTime(2018, 03, 15),
                Projects = "Bot",
                EmailId = "ssb.trainee@idsil.com",
                Designation = "Trainee",
                IsMarried = false,
                IsTrainee = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 11482,
                Name = "Aditya Krishna",
                Address = "Karnal",
                ContactNo = "9988545755",
                DOB = new DateTime(1992, 03, 15),
                DOJ = new DateTime(2018, 08, 27),
                Projects = "ML.Net",
                Designation = "Trainee",
                EmailId = "ssb.trainee@idsil.com",
                IsMarried = false,
                IsTrainee = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27126,
                Name = "Ashutosh Guleria",
                Address = "Maudi",
                ContactNo = "9988545755",
                DOB = new DateTime(1993, 04, 04),
                DOJ = new DateTime(2018, 09, 08),
                Projects = "Blockchain",
                Designation = "Trainee",
                EmailId = "ssb.trainee@idsil.com",
                IsMarried = false,
                IsTrainee = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 26853,
                Name = "Lakhvinder Kumar",
                Address = "Una",
                ContactNo = "9569435447",
                Experience = 5.6f,
                DOB = new DateTime(1986, 11, 20),
                DOA = new DateTime(2015, 01, 29),
                DOJ = new DateTime(2017, 05, 23),
                Projects = "3E Automation",
                Designation = "SSE",
                EmailId = "lakhvinder.k@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 26226,
                Name = "Manjit Singh",
                Address = "Bhatinda, Punjab",
                ContactNo = "9781608040",
                Experience = 5.6f,
                DOB = new DateTime(1991, 01, 11),
                DOA = new DateTime(2018, 02, 24),
                DOJ = new DateTime(2016, 01, 11),
                Projects = "Viggeby Data,Data Migration",
                Designation = "SSE",
                EmailId = "manjit.singh@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27129,
                Name = "Nitin Kumar",
                Address = "Kangra",
                ContactNo = "9914147535",
                Experience = 9.0f,
                DOB = new DateTime(1985, 07, 09),
                DOA = new DateTime(1992, 05, 30),
                DOJ = new DateTime(2011, 09, 19),
                Projects = "CTM,Complace Calender,EEZER,IP Vault",
                Designation = "TL",
                EmailId = "nitin.k@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27130,
                Name = "Raj Kumar",
                Address = "Jalandhar City, Punjab",
                ContactNo = "7837701814",
                Experience = 7.5f,
                DOB = new DateTime(1985, 04, 20),
                DOA = new DateTime(1992, 06, 07),
                DOJ = new DateTime(2016, 09, 16),
                Projects = "3E Automation",
                Designation = "SSE",
                EmailId = "parminderpal.s@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27131,
                Name = "Vivek",
                Address = "Jalandhar City, Punjab",
                ContactNo = "9988545755",
                Experience = 5.1f,
                DOB = new DateTime(1992, 09, 15),
                DOA = new DateTime(1992, 01, 15),
                DOJ = new DateTime(1992, 01, 15),
                Projects = "Bot",
                Designation = "SSE",
                EmailId = "parminderpal.s@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 26802,
                Name = "Dharmesh Piplani",
                Address = "Ambala",
                ContactNo = "9467759840",
                Experience = 13.0f,
                DOB = new DateTime(1979, 06, 05),
                DOA = new DateTime(1992, 12, 09),
                DOJ = new DateTime(2017, 03, 30),
                Projects = "3E VBA",
                Designation = "SSE",
                EmailId = "dharmesh.p@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27093,
                Name = "Harinder Kumar",
                Address = "Jalandhar City, Punjab",
                ContactNo = "9988545755",
                Experience = 1.8f,
                DOB = new DateTime(1992, 11, 15),
                DOJ = new DateTime(1992, 01, 15),
                Projects = "Bot",
                Designation = "SSE",
                EmailId = "parminderpal.s@idsil.com",
                IsMarried = false,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27171,
                Name = "Hitesh Kumar",
                Address = "Solan",
                ContactNo = "9041571047",
                Experience = 5.6f,
                DOB = new DateTime(1989, 09, 23),
                DOA = new DateTime(2018, 11, 12),
                DOJ = new DateTime(2018, 11, 11),
                Projects = "Upwork",
                Designation = "SSE",
                EmailId = "Hitesh.k@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27017,
                Name = "Vipul Sharma",
                Address = "Nalagarh",
                ContactNo = "9855846402",
                Experience = 7.0f,
                DOB = new DateTime(1988, 01, 18),
                DOJ = new DateTime(2018, 03, 22),
                Projects = "Research Workflow Management",
                Designation = "SSE",
                EmailId = "vipul.s@idsil.com",
                IsMarried = false,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27094,
                Name = "Pargat Singh",
                Address = "Punjab",
                ContactNo = "9463917404",
                Experience = 5.1f,
                DOB = new DateTime(1992, 01, 14),
                DOA = new DateTime(2017, 09, 09),
                DOJ = new DateTime(2018, 07, 11),
                Projects = "3E VBA",
                Designation = "SSE",
                EmailId = "Pargat.s@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 25342,
                Name = "Mukesh Saini",
                Address = "Kurukshetra",
                ContactNo = "9466241640",
                Experience = 10.0f,
                DOB = new DateTime(1984, 08, 10),
                DOA = new DateTime(2011, 02, 21),
                DOJ = new DateTime(2013, 03, 12),
                Projects = "Research Workflow Management",
                Designation = "SSE",
                EmailId = "Mukesh.saini@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 26225,
                Name = "Naveen Sharma",
                Address = "Jammu",
                ContactNo = "9463939452",
                Experience = 5.2f,
                DOB = new DateTime(1987, 01, 24),
                DOA = new DateTime(1992, 02, 16),
                DOJ = new DateTime(2016, 01, 06),
                Projects = "Container Tracking Management,ASP Vue/Typescript",
                Designation = "SSE",
                EmailId = "naveen.sha@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27210,
                Name = "Pritam Jha",
                Address = "Jalandhar City, Punjab",
                ContactNo = "9988545755",
                Experience = 4.0f,
                DOB = new DateTime(1992, 08, 21),
                DOJ = new DateTime(2018, 12, 20),
                Projects = "3E VBA",
                Designation = "SSE",
                EmailId = "pritam.j@idsil.com",
                IsMarried = false,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 26686,
                Name = "Kshitish Ranjan Mohanty",
                Address = "Cuttack",
                ContactNo = "9988545755",
                Experience = 11.0f,
                DOB = new DateTime(1978, 06, 23),
                DOA = new DateTime(1992, 01, 15),
                DOJ = new DateTime(2017, 01, 13),
                Projects = "Bot",
                Designation = "SSE",
                EmailId = "kshitish.m@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 26775,
                Name = "Pushpinder Kumar",
                Address = "Cuttack",
                ContactNo = "8437929609",
                Experience = 7.5f,
                DOB = new DateTime(1985, 02, 15),
                DOA = new DateTime(2013, 10, 28),
                DOJ = new DateTime(2017, 03, 07),
                Projects = "Amuze",
                Designation = "SSE",
                EmailId = "pushpinder.k@idsil.com",
                IsMarried = true,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 27217,
                Name = "Tejinder Singh",
                Address = "Mukatsar",
                ContactNo = "8284045404",
                Experience = 2.5f,
                DOB = new DateTime(1993, 03, 01),
                DOJ = new DateTime(2018, 12, 26),
                Projects = "DMT,Millnet",
                Designation = "SE",
                EmailId = "tejinder.s@idsil.com",
                IsMarried = false,
                Gender = 'M',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 26787,
                Name = "Geetika Nautiyal",
                Address = "Chandigarh",
                ContactNo = "8054924101",
                Experience = 11.0f,
                DOB = new DateTime(1988, 12, 14),
                DOJ = new DateTime(2017, 03, 20),
                Projects = "Dpeweb Automation",
                Designation = "SSE",
                EmailId = "kshitish.m@idsil.com",
                IsMarried = false,
                Gender = 'F',
            });

            employees.Add(new EmployeeModel()
            {
                EmployeeId = 26787,
                Name = "Karandeep Kaur",
                Address = "Chandigarh",
                ContactNo = "9814204959",
                Experience = 11.0f,
                DOB = new DateTime(1988, 12, 14),
                DOJ = new DateTime(2018, 11, 01),
                Projects = "Upwork Channel",
                Designation = "BDE",
                EmailId = "karandeep.k@idsil.com",
                IsMarried = false,
                Gender = 'F',
            });
            return employees;
        }

        public List<EmployeeModel> GetEmployeeDetailByName(string name)
        {
            var e = GetEmployee();
            var result = (from emp in e where emp.Name.ToLower().Contains(name.ToLower()) select emp).ToList();
            return result;
        }

        public List<EmployeeModel> GetEmployeeListByName(string name)
        {
            var e = GetEmployee();
            var result = (from emp in e where emp.Name.ToLower().Contains(name.ToLower()) select emp).ToList();
            return result;
        }

        public List<EmployeeModel> GetMaxEmployeeExp(float exp)
        {
            var e = GetEmployee();
            var result = (from emp in e where emp.Experience >= exp select emp).ToList();
            return result;
        }

        public List<EmployeeModel> GetMinEmployeeExp(float exp)
        {
            var e = GetEmployee();
            var result = (from emp in e where emp.Experience < exp select emp).ToList();
            return result;
        }

        public string GetEmployeeAddressByName(string name)
        {
            var e = GetEmployee();
            var result = e.Where(emp => emp.Name.ToLower().Contains(name.ToLower())).Select(emp => emp.Address).SingleOrDefault();
            return result;
        }

        public List<EmployeeModel> GetEmployeeDetailsById(int id)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.EmployeeId == id).ToList();
            return result;
        }

        public string GetProjectByName(string name)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Name.ToLower().Contains(name.ToLower())).Select(e => e.Projects).SingleOrDefault();
            return result;
        }

        public int? GetAgeInYears(DateTime birthDay)
        {
            if (birthDay > DateTime.Now)
                return null;
            int years = DateTime.Now.Year - birthDay.Year;
            if (DateTime.Now.Month < birthDay.Month ||
               (DateTime.Now.Month == birthDay.Month &&
                DateTime.Now.Day < birthDay.Day))
            {
                years--;
            }
            if (years >= 0)
                return years;
            else
                return 0;
        }

        public List<EmployeeModel> GetUpComingBday()
        {
            var e = GetEmployee();
            var result = (from emp in e where emp.DOB.Month <= DateTime.Now.AddMonths(3).Month orderby emp.DOB select emp).ToList();
            return result;
        }

        public List<EmployeeModel> GetUpComingAnniversaries()
        {
            var e = GetEmployee();
            var result = (from emp in e where emp.DOA.Month <= DateTime.Now.AddMonths(3).Month && emp.IsMarried == true orderby emp.DOB select emp).ToList();
            return result;
        }

        public List<string> GetAllProjects()
        {
            var emp = GetEmployee();
            var result = emp.Select(e => e.Projects).Distinct().ToList();
            return result;
        }

        public DateTime GetEmployeeBDay(string name)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Name.ToLower().Contains(name.ToLower())).Select(e => e.DOB).SingleOrDefault();
            return result;
        }

        public int GetNoOfProjects()
        {
            List<string> list = GetAllProjects();
            int result = list.Distinct().Count();
            return result;
        }

        public int GetNoOfEmployees()
        {
            var emp = GetEmployee();
            int count = emp.Select(e => e.Name).Count();
            return count;
        }

        public int GetNoOfTrainee()
        {
            var emp = GetEmployee();
            int result = emp.Where(e => e.IsTrainee == true).ToList().Count();
            return result;
        }

        public int GetNoOfMaleEmployee()
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Gender.Equals('M')).ToList().Count();
            return result;
        }

        public int GetNoOfFemaleEmployee()
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Gender.Equals('F')).ToList().Count();
            return result;
        }

        public List<EmployeeModel> GetAllTrainee()
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.IsTrainee == true).ToList();
            return result;
        }

        public string GetEmployeePhno(string name)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Name.ToLower().Contains(name.ToLower())).Select(e => e.ContactNo).FirstOrDefault();
            return result;
        }

        public float GetEmployeeExperience(string name)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Name.ToLower().Contains(name.ToLower())).Select(e => e.Experience).SingleOrDefault();
            return result;
        }

        public string GetEmailId(string name)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Name.ToLower().Contains(name.ToLower())).Select(e => e.EmailId).SingleOrDefault();
            return result;
        }

        public DateTime GetEmployeeDOJ(string name)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Name.ToLower().Contains(name.ToLower())).Select(e => e.DOJ).SingleOrDefault();
            return result;
        }

        public string GetEmployeeDesignation(string name)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Name.ToLower().Contains(name.ToLower())).Select(e => e.Designation).SingleOrDefault();
            return result;
        }

        public List<EmployeeModel> GetEmployeeByCity(string city)
        {
            var emp = GetEmployee();
            var result = emp.Where(x => x.Address.ToLower().Contains(city.ToLower())).ToList();
            return result;
        }

        public int GetEmployeeIdByName(string name)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Name.ToLower().Contains(name.ToLower())).Select(e => e.EmployeeId).SingleOrDefault();
            return result;
        }

        public string GetLastCompanyName(string name)
        {
            var emp = GetEmployee();
            var result = emp.Where(x => x.Name.ToString().Contains(name.ToString())).Select(x => x.LastCompanyName).FirstOrDefault();
            return result;
        }

        public List<EmployeeModel> GetMaleEmployees()
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Gender.Equals('M')).ToList();
            return result;
        }

        public Dictionary<string, int?> GetAgeMax(int age)
        {
            Dictionary<string, int?> empAge = new Dictionary<string, int?>();
            var emp = GetEmployee();
            foreach (EmployeeModel model in emp)
            {
                int? empage = GetAgeInYears(model.DOB);
                empAge.Add(model.Name, empage);
            }
            return empAge;
        }

        public List<EmployeeModel> GetFemaleEmployees()
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Gender.Equals('F')).ToList();
            return result;
        }

        public DateTime GetDateOfAnniversary(string name)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.Name.ToLower().Contains(name.ToLower())).Select(e => e.DOA).FirstOrDefault();
            return result;
        }

        public List<EmployeeModel> GetAllEmpAnniversary()
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.IsMarried == true).ToList();
            return result;
        }

        public int GetEmployeeId(int id)
        {
            var emp = GetEmployee();
            var result = emp.Where(e => e.EmployeeId == id).Select(e => e.EmployeeId).FirstOrDefault();
            return result;
        }
    }

    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        public string Designation { get; set; }

        public string Name { get; set; }

        public DateTime DOA { get; set; }

        public string Address { get; set; }

        public string EmailId { get; set; }

        public string ContactNo { get; set; }

        public string Projects { get; set; }

        public float Experience { get; set; }

        public DateTime DOB { get; set; }

        public DateTime DOJ { get; set; } // Date of Joining

        public char Gender { get; set; } // F- Female, M- Male

        public bool IsOnNoticePeriod { get; set; }

        public bool IsMarried { get; set; }

        public bool IsTrainee { get; set; }

        public bool IsOnProbationPeriod { get; set; }

        public string LastCompanyName { get; set; }
    }
}
