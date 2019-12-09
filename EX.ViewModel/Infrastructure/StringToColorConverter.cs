using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace EX.ViewModel.Infrastructure
{
    public class StringColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = "White";
            //"PAID", "UNPAID", "FOC", "CXLD"
            switch ((string)value)
            {
                case "PAID":
                    color = "Lime";
                    break;
                case "UNPAID":
                    color = "Tomato";
                    break;
                case "FOC":
                    color = "Aqua";
                    break;
                case "FREE":
                    color = "Aqua";
                    break;
                case "CXLD":
                    color = "DeepSkyBlue";
                    break;
                default:
                    color = "White";
                    break;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}