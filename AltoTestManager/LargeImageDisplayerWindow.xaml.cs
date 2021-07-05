using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AltoTestManager
{
    /// <summary>
    /// Interaction logic for LargeImageDisplayerWindow.xaml
    /// </summary>
    public partial class LargeImageDisplayerWindow : Window
    {
        public LargeImageDisplayerWindow(string imagePath, Stretch stretchType)
        {
            InitializeComponent();
            this.DataContext = new LargeImageDisplayerWindowVM(imagePath)
            {
                StretchType = stretchType
            };
        }
    }
}
