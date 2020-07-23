using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_DB_Person
{
    public class DataValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string username = (string)value;

            if (string.IsNullOrEmpty(username))
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
