using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop
{
    class CustomerClass: INotifyPropertyChanged
    {
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
