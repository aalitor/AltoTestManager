using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltoTestManager
{
    class LargeImageDisplayerWindowVM : INotifyPropertyChanged
    {
        private string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImagePath"));
            }
        }

        public LargeImageDisplayerWindowVM(string imagePath)
        {
            this.ImagePath = imagePath;
        }
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
