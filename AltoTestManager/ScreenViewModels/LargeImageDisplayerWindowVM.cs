using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AltoTestManager
{
    class LargeImageDisplayerWindowVM : INotifyPropertyChanged
    {
        private string imagePath;
        private Stretch stretchType;

        public Stretch StretchType
        {
            get { return stretchType; }
            set { stretchType = value; }
        }
        
        
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
            StretchType = Stretch.Uniform;
        }
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
