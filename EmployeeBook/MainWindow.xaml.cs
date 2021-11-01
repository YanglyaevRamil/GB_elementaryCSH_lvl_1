using EmployeeBook.Data;
using System.Windows;
using System.Windows.Controls;


namespace EmployeeBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database Database = new Database();
        public MainWindow()
        {
            InitializeComponent();
            DepartmentListView.ItemsSource = Database.Departments;
        }

        /// <summary>
        /// Обрабодчики событий сущности работник 
        /// </summary>
        private void btnAddEmp_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentListView.SelectedItem != null)
            {
                EmployeeCard employeeCard = new EmployeeCard(new Employee());
                employeeCard.Owner = this;
                if (employeeCard.ShowDialog() == true)
                {
                    Database.AddEmployee(employeeCard.Employee, (Department)DepartmentListView.SelectedItem);
                }
            }
        }
        private void btnInfoEmp_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeListView.SelectedItem != null)
            {
                EmployeeCard employeeCard = new EmployeeCard((Employee)EmployeeListView.SelectedItem);
                employeeCard.Owner = this;
                if (employeeCard.ShowDialog() == true)
                {
                    Database.UpdateEmployee(employeeCard.Employee, (Department)DepartmentListView.SelectedItem);
                }
            }
        } 
        private void btnDelitEmp_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeListView.SelectedItem != null) 
                if (MessageBox.Show("Хотите удалить рабочий кадр?", "Удаление рабочего кадра", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
                {
                    Database.RemoveEmployee((Employee)EmployeeListView.SelectedItem, (Department)DepartmentListView.SelectedItem);
                }
        }

        /// <summary>
        /// Обрабодчики событий сущности департамент 
        /// </summary>
        private void btnAddDep_Click(object sender, RoutedEventArgs e)
        {
            DepartmentCard departmentCard = new DepartmentCard(new Department());
            departmentCard.Owner = this;
            if (departmentCard.ShowDialog() == true)
            {
                Database.AddDepartments(departmentCard.Department);
            }  
        }

        private void btnInfoDep_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentListView.SelectedItem != null)
            {
                DepartmentCard departmentCard = new DepartmentCard((Department)DepartmentListView.SelectedItem);
                departmentCard.Owner = this;
                if (departmentCard.ShowDialog() == true)
                {
                    Database.UpdateDepartments(departmentCard.Department);
                }
            }
        }
        private void btnDelitDep_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentListView.SelectedItem != null)
                if (MessageBox.Show("Хотите удалить департамент?", "Удаление департамента", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Database.RemoveDepartments((Department)DepartmentListView.SelectedItem);
                    EmployeeListView.ItemsSource = null;
                }
        }

        // Выделение департамента
        private void DepartmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                EmployeeListView.ItemsSource = ((Department)e.AddedItems[0]).Employees;
            }
        }
    }
}
