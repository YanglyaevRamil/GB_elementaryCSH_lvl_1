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
using System.Windows.Shapes;

namespace EmployeeBook
{
    /// <summary>
    /// Логика взаимодействия для DepartmentCard.xaml
    /// </summary>
    public partial class DepartmentCard : Window, INotifyPropertyChanged
    {
        private Department department;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string prName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prName));
        }

        public DepartmentCard()
        {
            InitializeComponent();
            department = new Department();
        }
        public DepartmentCard(Department dep)
        {
            InitializeComponent();

            department = dep;

            tbID.Text = Convert.ToString(department.IDdepartment);
            tbDep.Text = department.NameDepartment;
            tbSalary.Text = department.Profit;
            tbNumEmp.Text = Convert.ToString(department.NumberEmployees);
        }

        public Department Department
        {
            get { return department; }
            set 
            {
                department = value;
                NotifyPropertyChanged();
            }
        }

        public void UpDepartment()
        {
            department.IDdepartment = tbID.Text;
            department.NameDepartment = tbDep.Text;
            department.Profit = tbSalary.Text;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            UpDepartment();
            
            ((MainWindow)this.Owner).RefreshDep();
            Close();
        }

        private void btnDelit_Click(object sender, RoutedEventArgs e) => Close();
    }
}
