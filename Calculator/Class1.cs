using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class Class1
    {
        public static ObservableCollection<Record> moc = new ObservableCollection<Record>();
        

    }
    public class Record
    {
        public string record { get; set; }
    }
}
