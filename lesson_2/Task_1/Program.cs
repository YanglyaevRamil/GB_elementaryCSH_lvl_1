using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //б) Создать на базе абстрактного класса массив сотрудников и заполнить его.
            Employee[] employees =
            {
                new FixSalaryEmployee(001, "Petr", "Zzz", (float)20000, 23),
                new HourlySalaryEmployee(024, "Roman", "Xxx", (float)550, 22),
                new FixSalaryEmployee(102, "Ivan", "Ccc", (float)100000, 44),
                new HourlySalaryEmployee(044, "Olya", "Vvv", (float)900, 37),
                new FixSalaryEmployee(083, "Nikolay", "Bbb", (float)26500, 27),
                new HourlySalaryEmployee(054, "Roman", "Ggg", (float)400, 54)
            };

            //в) *Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort().
            Array.Sort(employees);

            foreach (var item in employees)
            {
                Console.WriteLine($"Name - {item.EmpName} : Age - {item.EmpAge} : Salary - {item.EmpSalary}");
            }

            Console.WriteLine("++++++++++++++++++++++++");

            ArrEmployee employee = new ArrEmployee();

            ArrEmployee.GetConsoleListEmployee(employee.employees);

            Console.WriteLine("++++++++++++++++++++++++");

            ArrEmployee.GetSortArrEmpoyee(employee.employees, "id");

            Console.WriteLine("++++++++++++++++++++++++");

            ArrEmployee.GetSortArrEmpoyee(employee.employees, new ArrEmployee.IDComparer());

            Console.ReadLine();
        }


    }



    public abstract class Employee : IComparable<Employee>
    {
        protected int empID;
        protected string empName;
        protected string empSurname;
        protected double empSalary;
        protected int empAge;

        public int EmpID
        {
            get { return empID; }
            set
            {
                if (value >= 0)
                {
                    empID = value;
                }
            }
        }
        public string EmpName
        {
            get { return empName; }
            set
            {
                if (value.Length <= 20)
                {
                    if (!IsNumberContains(value))
                    {
                        empName = value;
                    }
                }
            }
        }
        public string EmpSurname
        {
            get { return empSurname; }
            set
            {
                if (value.Length <= 20)
                {
                    if (!IsNumberContains(value))
                    {
                        empSurname = value;
                    }
                }
            }
        }
        public double EmpSalary
        {
            get { return GetSalary(); }
            set
            {
                if (value >= 0)
                {
                    empSalary = value;
                }
            }
        }
        public int EmpAge
        {
            get { return empAge; }
            set
            {
                if (value >= 16)
                {
                    empAge = value;
                }
            }
        }

        public Employee()
        {

        }
        public Employee(int empID, string empName, string empSurname, double empSalary, int empAge)
        {
            EmpID = empID;
            EmpName = empName;
            EmpSurname = empSurname;
            EmpSalary = empSalary;
            EmpAge = empAge;
        }

        //а) Описать в базовом классе абстрактный метод для расчёта среднемесячной заработной платы.
        //Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовая ставка», для работников с
        //фиксированной оплатой «среднемесячная заработная плата = фиксированная месячная оплата».
        protected abstract double GetSalary();


        protected static bool IsNumberContains(string input)
        {
            foreach (char c in input)
                if (Char.IsNumber(c))
                    return true;
            return false;
        }

        public int CompareTo(Employee obj)
        {
            return empAge - obj.EmpAge;
        }
    }

    public class FixSalaryEmployee : Employee
    {

        public FixSalaryEmployee(int empID, string empName, string empSurname, float empSalary, int empAge) : base(empID, empName, empSurname, empSalary, empAge)
        {

        }
        protected override double GetSalary()
        {
            return empSalary;
        }
    }

    public class HourlySalaryEmployee : Employee
    {
        private double hourlyRate;
        public double HourlyRate
        {
            get { return hourlyRate; }
            set
            {
                if (value > 0)
                {
                    hourlyRate = value;
                }
            }
        }

        public HourlySalaryEmployee(int empID, string empName, string empSurname, double hourlyRate, int empAge) : base(empID, empName, empSurname, 0, empAge)
        {
            HourlyRate = hourlyRate;
            EmpSalary = GetSalary();
        }
        protected override double GetSalary()
        {
            return 20.8 * 8 * hourlyRate;
        }
    }

    //г) *Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
    public class ArrEmployee
    {
        public Employee[] employees =
            {
                new FixSalaryEmployee(001, "Petr", "Zzz", (float)20000, 23),
                new HourlySalaryEmployee(024, "Roman", "Xxx", (float)550, 22),
                new FixSalaryEmployee(102, "Ivan", "Ccc", (float)100000, 44),
                new HourlySalaryEmployee(044, "Olya", "Vvv", (float)900, 37),
                new FixSalaryEmployee(083, "Nikolay", "Bbb", (float)26500, 27),
                new HourlySalaryEmployee(054, "Roman", "Ggg", (float)400, 54)
            };
        public class IDComparer : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                return x.EmpID - y.EmpID;
            }
        }
        class NameComparer : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                return String.Compare(x.EmpName, y.EmpName);
            }
        }
        class SurnameComparer : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                return String.Compare(x.EmpSurname, y.EmpSurname);
            }
        }
        class SalaryComparer : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                return Convert.ToInt32(x.EmpSalary - y.EmpSalary);
            }
        }
        class AgeComparer : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                return x.EmpAge - y.EmpAge;
            }
        }

        public static void GetConsoleListEmployee(Employee[] employees)
        {
            foreach (var emp in employees)
            {
                Console.WriteLine($"ID - {emp.EmpID} : Name - {emp.EmpName} : Surname - {emp.EmpSurname} : Salary - {emp.EmpSalary} : Age - {emp.EmpAge}");
            }
        }

        public static void GetSortArrEmpoyee(Employee[] employees, IComparer<Employee> obj)
        {
            Array.Sort(employees, obj);
            GetConsoleListEmployee(employees);
        }

        public static void GetSortArrEmpoyee(Employee[] employees, string field)
        {
            switch (field)
            {
                case "id":
                    Array.Sort(employees, new IDComparer());
                    GetConsoleListEmployee(employees);
                    break;
                case "name":
                    Array.Sort(employees, new NameComparer());
                    GetConsoleListEmployee(employees);
                    break;
                case "surname":
                    Array.Sort(employees, new SurnameComparer());
                    GetConsoleListEmployee(employees);
                    break;
                case "salary":
                    Array.Sort(employees, new SalaryComparer());
                    GetConsoleListEmployee(employees);
                    break;
                case "age":
                    Array.Sort(employees, new AgeComparer());
                    GetConsoleListEmployee(employees);
                    break;
                default:
                    break;
            }
        }
    } 
}
