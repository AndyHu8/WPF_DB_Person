
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace WPF_DB_Person.Infrastructure
{
    public class AutoValidationBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;



            base.OnAttached();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            var txtboxen = FindVisualChildren<TextBox>(AssociatedObject);

            foreach (var item in txtboxen)
            {
                if (item.Name == "Suche")
                {
                    continue;
                }
                // we manually fire the bindings so we get the validation initially
                var binding = item.GetBindingExpression(TextBox.TextProperty);
                binding.ParentBinding.ValidationRules.Add(new DataValidationRule());
                //binding.ParentBinding.Mode = System.Windows.Data.BindingMode.TwoWay;
                binding.UpdateSource();
            }
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }
                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
    
}
