using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeBook.Data
{
    public class Department : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string iddepartment;
        private string nameDepartment;
        private string profit;
        private ObservableCollection<Employee> employees;

        public Department()
        {
            Employees = new ObservableCollection<Employee>();
        }
        public Department(string IDdepartment, string NameDepartment, string Profit, ObservableCollection<Employee> Employees)
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

        public object Clone()
        {
            return this.MemberwiseClone();
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
        public int NumberEmployees { get { return Employees.Count; } set { } }
        public ObservableCollection<Employee> Employees 
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
