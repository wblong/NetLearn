using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfAppDataBind
{
    /// <summary>
    /// Converter
    /// </summary>
    [ValueConversion(typeof(double),typeof(Brush))]
    public class TemperatureToBrushCoverter : IValueConverter
    {
        public double Hot { get; set; }
        public double Cold { get; set; }
        public TemperatureToBrushCoverter()
        {

        }
        public TemperatureToBrushCoverter(double hot,double cold):this()
        {
            this.Hot = hot;
            this.Cold = cold;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double temp;
            Brush brush = Brushes.Black;
            if(Double.TryParse((string)value,out temp))
            {
                if (temp > Hot)
                    brush = Brushes.Red;
                else if (temp < Cold)
                    brush = Brushes.Blue;
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
