using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AltoTestManager.Converters
{
    class ObjectArrayConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var tc = (TestCase)values.First();
            var lv = (System.Windows.Controls.ListView)values.Last();
            return new Tuple<TestCase, System.Windows.Controls.ListView>(tc, lv);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
