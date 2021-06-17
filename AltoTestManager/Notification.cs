using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltoTestManager
{
    class Notification : INotifyPropertyChanged
    {
        private string text;
        private int type;

        public int Type
        {
            get { return type; }
            set
            {
                type = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Type"));
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
