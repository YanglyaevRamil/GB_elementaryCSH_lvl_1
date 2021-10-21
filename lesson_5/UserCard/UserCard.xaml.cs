using EmployeeBook.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserCard
{
    /// <summary>
    /// Логика взаимодействия для UserCard.xaml
    /// </summary>
    public partial class UserCard : UserControl, INotifyPropertyChanged
    {
        private Employee employee;

        public Employee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UserCard()
        {
            InitializeComponent();
            employee = new Employee();
        }

        public void SetEmployee(Employee employee)
        {
            this.employee = employee;
            tbFirstName.Text = employee.FirstName;
            tbLastName.Text = employee.LastName;
            tbSecondName.Text = employee.SecondName;
            tbPosition.Text = employee.Position;
            tbSalary.Text = employee.Salary;
            tbPhone.Text = employee.Phone;
        }
        public void UpEmployee()
        {
            if (tbFirstName.Text.Length >= 2)
                employee.FirstName = tbFirstName.Text;
            if (tbLastName.Text.Length >= 2)
                employee.LastName = tbLastName.Text;
            if (tbSecondName.Text.Length >= 2)
                employee.SecondName = tbSecondName.Text;
            if (tbPosition.Text.Length >= 2)
                employee.Position = tbPosition.Text;
            if (tbSalary.Text.Length >= 2)
                employee.Salary = tbSalary.Text;
            if (tbPhone.Text.Length >= 2)
                employee.Phone = tbPhone.Text;
        }
        public Employee GetEmployee()
        {
            UpEmployee();
            return employee;
        }
    }
}
