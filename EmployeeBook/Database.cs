using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using EmployeeBook.Data;

namespace EmployeeBook
{
    public class Database
    {
        private const string ConnectionString = "Data Source=DESKTOP-EBFCT1J\\SQLEXPRESS;Initial Catalog=EmployeeBook;User ID=UserRoot;Password=user";

        //private static string[] PHONE_PREFIX = { "906", "495", "499" }; // Префексы телефонных номеров
        //private static int CHAR_BOUND_L = 65; // Номер начального символа (для генерации последовательности символов)
        //private static int CHAR_BOUND_H = 90; // Номер конечного  символа (для генерации последовательности символов)

        private Random random = new Random();
        private ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Department> Departments { get; set; }

        public Database()
        {

            Departments = new ObservableCollection<Department>();
            Employees = new ObservableCollection<Employee>();
            LoadFromDatabase();

            //Departments.Add(new Department("01", "RTL", "300000000", GenerateEmployees(20)));

            //Departments.Add(new Department("02", "Verification", "200050000", GenerateEmployees(15)));

            //Departments.Add(new Department("03", "WebDev", "100000000", GenerateEmployees(5)));

            //Departments.Add(new Department("04", "Analytics", "200005000", GenerateEmployees(13)));

            //Departments.Add(new Department("05", "Lawyers", "5000000", GenerateEmployees(3)));
        }

        private void LoadFromDatabase()
        {
            string sqlExpressionEmployee = "SELECT * FROM EmployeeTable";
            string sqlExpressionDepartment = "SELECT * FROM DepartmentTable";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpressionDepartment, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var department = new Department()
                            {
                                IDdepartment = reader.GetString(0),
                                NameDepartment = reader.GetString(1),
                                Profit = reader.GetString(2)
                            };
                            Departments.Add(department);
                        }
                    }
                }

                command = new SqlCommand(sqlExpressionEmployee, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)        // Если есть данные
                    {
                        while (reader.Read())  // Построчно считываем данные
                        {
                            var employee = new Employee()
                            {
                                FirstName = reader.GetString(0),
                                LastName = reader.GetString(1),
                                SecondName = reader.GetString(2),
                                Position = reader.GetString(3),
                                Salary = reader.GetString(4),
                                Phone = reader.GetString(5)
                            };

                            for (int i = 0; i < Departments.Count; i++)
                            {
                                if (Departments[i].IDdepartment == reader["IDdepartment"].ToString())
                                {
                                    Departments[i].Employees.Add(employee);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public int AddEmployee(Employee employee, Department department)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpression = $@"INSERT INTO EmployeeTable (FirstName, LastName, SecondName, Position, Salary, Phone, IdDepartment)
                                     VALUES ( '{employee.FirstName}', '{employee.LastName}', '{employee.SecondName}', '{employee.Position}', '{employee.Salary}', '{employee.Phone}', '{department.IDdepartment}' )";
                var command = new SqlCommand(sqlExpression, connection);
                var res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    department.Employees.Add(employee);
                }
                return res;
            }
        }

        public int UpdateEmployee(Employee employee, Department department)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpression = $@"UPDATE EmployeeTable 
                    SET FirstName = '{employee.FirstName}', LastName = '{employee.LastName}', SecondName = '{employee.SecondName}', Position = '{employee.Position}', Salary = '{employee.Salary}', IdDepartment = '{department.IDdepartment}'
                    WHERE Phone = '{employee.Phone}'";
                var command = new SqlCommand(sqlExpression, connection);
                return command.ExecuteNonQuery();
            }
        }

        public int RemoveEmployee(Employee employee, Department department)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpression = $@"DELETE FROM EmployeeTable WHERE Phone = '{employee.Phone}'";
                var command = new SqlCommand(sqlExpression, connection);
                var res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    department.Employees.Remove(employee);
                }
                return res;
            }
        }

        public int AddDepartments(Department department)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpression = $@"INSERT INTO DepartmentTable (IdDepartment, NameDepartment, Profit)
                                     VALUES ( '{department.IDdepartment}', '{department.NameDepartment}', '{department.Profit}')";
                var command = new SqlCommand(sqlExpression, connection);
                var res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    Departments.Add(department);
                }
                return res;
            }
        }

        public int UpdateDepartments(Department department)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpression = $@"UPDATE EmployeeTable 
                    SET NameDepartment = '{department.NameDepartment}', Profit = '{department.Profit}'
                    WHERE Phone = '{department.IDdepartment}'";
                var command = new SqlCommand(sqlExpression, connection);
                return command.ExecuteNonQuery();
            }
        }

        public int RemoveDepartments(Department department)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpressionDepDB = $@"DELETE FROM DepartmentTable WHERE IdDepartment = '{department.IDdepartment}'";
                string sqlExpressionEmpDB = $@"DELETE FROM EmployeeTable WHERE IdDepartment = '{department.IDdepartment}'";

                var commandDepDB = new SqlCommand(sqlExpressionDepDB, connection);
                var resDepDB = commandDepDB.ExecuteNonQuery();
                if (resDepDB > 0)
                {
                    Departments.Remove(department);
                }
                var commandEmpDB = new SqlCommand(sqlExpressionEmpDB, connection);
                var resEmpDB = commandEmpDB.ExecuteNonQuery();

                return resDepDB;
            }
        }

        //private Department GetDep(Employee employee)
        //{
        //    for (int i = 0; i < Departments.Count; i++)
        //    {
        //        for (int j = 0; j < Departments[i].Employees.Count; j++)
        //        {
        //            if (employee.Phone == Departments[i].Employees[j].Phone)
        //            {
        //                return Departments[i];
        //            }
        //        }
                
        //    }
        //    return null;
        //}

        //private string GenerateSymbols(int amount)
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    for (int i = 0; i < amount; i++)
        //        stringBuilder.Append((char)(CHAR_BOUND_L + random.Next(CHAR_BOUND_H - CHAR_BOUND_L)));
        //    return stringBuilder.ToString();
        //}

        //private string GeneratePhone()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    stringBuilder.Append("+7").Append(PHONE_PREFIX[random.Next(3)]);
        //    for (int i = 0; i < 6; i++)
        //        stringBuilder.Append(random.Next(10));
        //    return stringBuilder.ToString();
        //}

        //private ObservableCollection<Employee> GenerateEmployees(int contactCount)
        //{
        //    Employees.Clear();

        //    ObservableCollection<Employee> L = new ObservableCollection<Employee>();

        //    for (int i = 0; i < contactCount; i++)
        //    {
        //        string phone = GeneratePhone();
        //        L.Add(new Employee(GenerateSymbols(random.Next(6) + 5), GenerateSymbols(random.Next(6) + 5), GenerateSymbols(random.Next(6) + 5), GenerateSymbols(random.Next(6) + 5), Convert.ToString(random.Next(30000, 200000)), GeneratePhone()));
        //    }
        //    return L; 
        //}

    }
}
