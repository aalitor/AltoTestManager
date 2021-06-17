using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AltoTestManager.Converters
{
    class NotificationTypeToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                var str = (int)value;
                return str == 1 ? Brushes.Green : str == -1 ? Brushes.Red : Brushes.Wheat;
            }
            return Brushes.Wheat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
