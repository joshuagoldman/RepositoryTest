using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AddNewProductGUI.Models
{
    public class WindowBehaviors
    {

        public static double GetActualWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(ActualWidthProperty);
        }

        public static void SetActualWidth(DependencyObject obj, double value)
        {
            obj.SetValue(ActualWidthProperty, value);
        }

        public static readonly DependencyProperty ActualWidthProperty =
            DependencyProperty.RegisterAttached("ActualWidth",
                typeof(double),
                typeof(WindowBehaviors),
                new FrameworkPropertyMetadata(double.NaN, new PropertyChangedCallback(ActualWidthChanged))
                { BindsTwoWayByDefault = true });


        private static void ActualWidthChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Window w = source as Window;
            if (w != null)
            {
                w.SizeChanged += (se, ev) =>
                {
                    SetActualWidth((DependencyObject)se, ev.NewSize.Width);
                };
            }
        }


        //Height
        public static double GetActualHeigt(DependencyObject obj)
        {
            return (double)obj.GetValue(ActualHeightProperty);
        }

        public static void SetActualHeight(DependencyObject obj, double value)
        {
            obj.SetValue(ActualHeightProperty, value);
        }

        public static readonly DependencyProperty ActualHeightProperty =
            DependencyProperty.RegisterAttached("ActualHeight",
                typeof(double),
                typeof(WindowBehaviors),
                new FrameworkPropertyMetadata(double.NaN, new PropertyChangedCallback(ActualHeightChanged))
                { BindsTwoWayByDefault = true });


        private static void ActualHeightChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Window w = source as Window;
            if (w != null)
            {
                w.SizeChanged += (se, ev) =>
                {
                    SetActualHeight((DependencyObject)se, ev.NewSize.Height);
                };
            }
        }
    }
}
