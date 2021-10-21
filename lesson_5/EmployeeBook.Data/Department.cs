using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBook.Data
{
    public class Department : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string iddepartment;
        private string nameDepartment;
        private string profit;
        private List<Employee> employees;

        public Department()
        {
            Employees = new List<Employee>();
        }
        public Department(string IDdepartment, string NameDepartment, string Profit, List<Employee> Employees)
        {
            this.IDdepartment = IDdepartment;
            this.NameDepartment = NameDepartment;
            this.Profit = Profit;
            this.Employees = Employees;
        }

        private void NotifyPropertyChanged([CallerMemberName] string prName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prName));
        }

        public string IDdepartment 
        { 
            get { return iddepartment; }
            set
            {
                iddepartment = value;
                NotifyPropertyChanged();
            }
        }
        public string NameDepartment
        {
            get { return nameDepartment; }
            set 
            { 
                nameDepartment = value;
                NotifyPropertyChanged();
            }
        }
        public string Profit 
        { 
            get { return profit; } 
            set 
            { 
                profit = value;
                NotifyPropertyChanged();
            } 
        }
        public int NumberEmployees { get { return Employees.Count; } }
        public List<Employee> Employees 
        { 
            get { return employees; } 
            set 
            { 
                employees = value;
                NotifyPropertyChanged();
            }
        }
    }
}
