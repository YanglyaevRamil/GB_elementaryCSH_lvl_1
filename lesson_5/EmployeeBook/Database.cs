using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeBook.Data;

namespace EmployeeBook
{
    public class Database
    {
        private static string[] PHONE_PREFIX = { "906", "495", "499" }; // Префексы телефонных номеров
        private static int CHAR_BOUND_L = 65; // Номер начального символа (для генерации последовательности символов)
        private static int CHAR_BOUND_H = 90; // Номер конечного  символа (для генерации последовательности символов)

        private Random random = new Random();
        private List<Employee> Employees { get; set; }
        public List<Department> Departments { get; set; }

        public Database()
        {
            Departments = new List<Department>();
            Employees = new List<Employee>();

            Departments.Add(new Department("01", "RTL", "300000000", GenerateEmployees(20)));
            
            Departments.Add(new Department("02", "Verification", "200050000", GenerateEmployees(15)));
          
            Departments.Add(new Department("03", "WebDev", "100000000", GenerateEmployees(5)));
     
            Departments.Add(new Department("04", "Analytics", "200005000", GenerateEmployees(13)));
   
            Departments.Add(new Department("05", "Lawyers", "5000000", GenerateEmployees(3)));
        }

        private string GenerateSymbols(int amount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < amount; i++)
                stringBuilder.Append((char)(CHAR_BOUND_L + random.Next(CHAR_BOUND_H - CHAR_BOUND_L)));
            return stringBuilder.ToString();
        }

        private string GeneratePhone()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("+7").Append(PHONE_PREFIX[random.Next(3)]);
            for (int i = 0; i < 6; i++)
                stringBuilder.Append(random.Next(10));
            return stringBuilder.ToString();
        }

        private List<Employee> GenerateEmployees(int contactCount)
        {
            Employees.Clear();

            List<Employee> L = new List<Employee>();

            for (int i = 0; i < contactCount; i++)
            {
                string phone = GeneratePhone();
                L.Add(new Employee(GenerateSymbols(random.Next(6) + 5), GenerateSymbols(random.Next(6) + 5), GenerateSymbols(random.Next(6) + 5), GenerateSymbols(random.Next(6) + 5), Convert.ToString(random.Next(30000, 200000)), GeneratePhone()));
            }
            return L; 

        }

    }
}
