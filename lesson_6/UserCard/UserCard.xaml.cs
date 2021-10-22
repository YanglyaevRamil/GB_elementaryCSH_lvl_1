using EmployeeBook.Data;
using System.Windows.Controls;

namespace UserCard
{
    /// <summary>
    /// Логика взаимодействия для UserCard.xaml
    /// </summary>
    public partial class UserCard : UserControl
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
            }
        }

        public UserCard()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
