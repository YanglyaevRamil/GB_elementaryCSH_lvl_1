using EmployeeBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeBook
{
    public partial class EmployeeCard : Window
    {
        private Employee employee;

        public EmployeeCard()
        {
            
        }
        public EmployeeCard(Employee Emp)
        {
            InitializeComponent();
            employee = Emp;
            this.UserCardControl.SetEmployee(employee);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => Close();

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            UserCardControl.UpEmployee();
            ((MainWindow)this.Owner).RefreshEmp();
            Close();
        }
    }
}
