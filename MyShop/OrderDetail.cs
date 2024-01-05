using MyShop.pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop
{
    internal class OrderDetail: INotifyPropertyChanged
    {
        public string phone { get; set; }
        public int amount { get; set; }
        public int total { get; set; }
        public string image { get; set; }
        public int totalProfit { get; set; }    

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
