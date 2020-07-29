using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            // alle textboxen auf der form holen
            var txtboxen = FindVisualChildren<TextBox>(AssociatedObject);

            foreach (var item in txtboxen)
            {
                // suche textbox ignorieren
                if (item.Name == "Suche")
                    continue;

                var binding = item.GetBindingExpression(TextBox.TextProperty);
                if (binding != null)
                {
                    // validierung hinzufügen
                    binding.ParentBinding.ValidationRules.Add(new DataValidationRule
                    {
                        // wichtig!
                        ValidationStep = ValidationStep.UpdatedValue
                    });

                    // we manually fire the bindings so we get the validation initially
                    binding.UpdateSource();
                }
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
