using EmployeeBook.Data;
using System.Windows;

namespace EmployeeBook
{
    public partial class EmployeeCard : Window
    {
        private Employee employee;
        public Employee Employee { get{ return employee; } }
        public EmployeeCard(Employee Emp)
        {
            InitializeComponent();
            employee = Emp;
            UserCardControl.Employee = (Employee)employee.Clone(); ;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => Close();

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {  
            SetEmployee();
            DialogResult = true;
            //Close();
        }

        private void SetEmployee()
        {
            employee.FirstName = UserCardControl.Employee.FirstName;
            employee.LastName = UserCardControl.Employee.LastName;
            employee.SecondName = UserCardControl.Employee.SecondName;
            employee.Salary = UserCardControl.Employee.Salary;
            employee.Phone = UserCardControl.Employee.Phone;
            employee.Position = UserCardControl.Employee.Position;
        }
    }
}
