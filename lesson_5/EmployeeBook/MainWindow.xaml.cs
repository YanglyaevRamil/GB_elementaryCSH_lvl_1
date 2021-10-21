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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database Database = new Database();
        public Database SetDatabase { set; get; }
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
                ((Department)DepartmentListView.SelectedItem).Employees.Add(new Employee());
                EmployeeCard employeeCard = new EmployeeCard(((Department)DepartmentListView.SelectedItem).Employees.Last());
                employeeCard.Owner = this;
                employeeCard.Show();
            }
        }
        private void btnInfoEmp_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeListView.SelectedItem != null)
            {
                EmployeeCard employeeCard = new EmployeeCard((Employee)EmployeeListView.SelectedItem);
                employeeCard.Owner = this; 
                employeeCard.Show();
                
            }
        } 
        private void btnDelitEmp_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeListView.SelectedItem != null) 
                if (MessageBox.Show("Хотите удалить рабочий кадр?", "Удаление рабочего кадра", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
                {  
                    ((Department)DepartmentListView.SelectedItem).Employees.Remove((Employee)EmployeeListView.SelectedItem);

                    EmployeeListView.ItemsSource = null;
                    EmployeeListView.ItemsSource = ((Department)DepartmentListView.SelectedItem).Employees;
                }
        }

        /// <summary>
        /// Обрабодчики событий сущности департамент 
        /// </summary>
        private void btnAddDep_Click(object sender, RoutedEventArgs e)
        {
            Database.Departments.Add(new Department());
            DepartmentCard departmentCard = new DepartmentCard(Database.Departments.Last());
            departmentCard.Owner = this;
            departmentCard.Show();
        }

        private void btnInfoDep_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentListView.SelectedItem != null)
            {
                DepartmentCard departmentCard = new DepartmentCard((Department)DepartmentListView.SelectedItem);
                departmentCard.Owner = this;
                departmentCard.Show();
            }
        }
        private void btnDelitDep_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentListView.SelectedItem != null)
                if (MessageBox.Show("Хотите удалить департамент?", "Удаление департамента", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Database.Departments.Remove((Department)DepartmentListView.SelectedItem);

                    DepartmentListView.ItemsSource = null;
                    DepartmentListView.ItemsSource = Database.Departments;

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

        /// <summary>
        /// кастыли, не успел убрать ) 
        /// </summary>
        public void RefreshDep()
        {
            DepartmentListView.ItemsSource = null;
            DepartmentListView.ItemsSource = Database.Departments;
        }
        public void RefreshEmp()
        {
            EmployeeListView.ItemsSource = null;
            EmployeeListView.ItemsSource = ((Department)DepartmentListView.SelectedItem).Employees;
        }

    }
}
