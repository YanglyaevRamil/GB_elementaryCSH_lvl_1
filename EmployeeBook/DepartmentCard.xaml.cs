using EmployeeBook.Data;
using System.Windows;


namespace EmployeeBook
{
    /// <summary>
    /// Логика взаимодействия для DepartmentCard.xaml
    /// </summary>
    public partial class DepartmentCard : Window
    {
        private Department department;
        private Department bufDep;
        public Department bufDepartment
        {
            get { return bufDep; }
        }

        public Department Department
        {
            get { return department; }
        }

        public DepartmentCard(Department dep)
        {
            InitializeComponent();
            DataContext = this;
            department = dep;
            bufDep = (Department)department.Clone();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SetDepartmen(bufDep);
            DialogResult = true;
        }
        private void SetDepartmen(Department d)
        {
            department.IDdepartment = d.IDdepartment;
            department.NameDepartment = d.NameDepartment;
            department.Profit = d.Profit;
        }

        private void btnСancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
