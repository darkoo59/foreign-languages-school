using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Model
{
    public class School : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private Address _address;
        private List<string> _languages;
        public int Id {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public Address Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }
        public List<string> Languages
        {
            get { return _languages; }
            set
            {
                _languages = value;
                OnPropertyChanged("Languages");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
