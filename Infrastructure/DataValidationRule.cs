using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF_DB_Person
{
    public class DataValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Diese Textbox darf nicht leer sein.");
            }

            //string username = (string)value;
            var bg = value as BindingExpression;

            var property = bg.ResolvedSourcePropertyName;
            var src = bg.ResolvedSource;

            var wert = src.GetType().GetProperty(property)?.GetValue(src)?.ToString();

            if (string.IsNullOrEmpty(wert))
            {
                return new ValidationResult(false, "Diese Textbox darf nicht leer sein.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
