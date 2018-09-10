using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Model;

namespace View.Converters
{
    public class MicroparticleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mic = value as List<Microparticle>;
            if (mic != null && mic.Count > 0)
            {
                var strings = new string[mic.Count];
                for (int i = 0; i < mic.Count; i++)
                {
                    strings[i] = "Микрочастица номер " + mic[i].Number + "(" + mic[i].Pixels.Count + ")";
                }
                return strings;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
