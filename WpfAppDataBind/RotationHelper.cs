using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfAppDataBind
{
   public class RotationHelper:DependencyObject
    {
        public static double GetAngle(DependencyObject obj)
        {
            return (double)obj.GetValue(AngleProperty);
        }
        public static void SetAngle(DependencyObject obj, double value)
        {
            obj.SetValue(AngleProperty, value);
        }
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.RegisterAttached("AngleProperty", typeof(double), typeof(RotationHelper),
                new PropertyMetadata(0.0, OnAngleChanged));
        private static void OnAngleChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            UIElement element= d as UIElement;
            if (element != null)
            {
                element.RenderTransformOrigin = new Point(0.5, 0.5);
                element.RenderTransform = new RotateTransform((double)e.NewValue);
            }
        }
    }
}
