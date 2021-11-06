using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CRM.Desktop.View.Converters
{
    [ValueConversion(typeof(int), typeof(ImageSource))]
    public class GenderToImageConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "female":
                    return string.Format("/CRM.Desktop.View;component/Graphics/woman.png");
                case "male":
                    return string.Format("/CRM.Desktop.View;component/Graphics/man.png");
                case "add":
                    return string.Format("/CRM.Desktop.View;component/Graphics/addUser.png");
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
