using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace EmployeeBook.Data
{
    public class Employee : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string firstName;
        private string lastName;
        private string secondName;
        private string position;
        private string salary;
        private string phone;
        private string iddep;

        public string FirstName 
        {
            get { return firstName; }
            set 
            {
                firstName = value;
                NotifyPropertyChanged();
            } 
        }
        public string LastName
        { 
            get { return lastName; }
            set
            {
                lastName = value;
                NotifyPropertyChanged();
            } 
        }
        public string SecondName 
        { 
            get { return secondName; }
            set 
            {
                secondName = value;
                NotifyPropertyChanged();
            }
        }
        public string Position 
        { 
            get { return position; }
            set 
            {
                position = value;
                NotifyPropertyChanged();
            }
        }
        public string Salary
        { 
            get { return salary; }
            set 
            {
                salary = value;
                NotifyPropertyChanged();
            }
        }
        public string Phone
        { 
            get { return phone; }
            set
            {
                phone = value;
                NotifyPropertyChanged();
            }
        }

        public string IdDep
        {
            get { return iddep; }
            set
            {
                iddep = value;
            }
        }

        public Employee(){}
        public Employee(string FirstName, string LastName, string SecondName, string Position, string Salary, string Phone)
        {
            this.FirstName = FirstName ;
            this.LastName  = LastName  ;
            this.SecondName= SecondName;
            this.Position  = Position  ;
            this.Salary    = Salary    ;
            this.Phone = Phone;
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
    }
}
