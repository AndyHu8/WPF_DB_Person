using Caliburn.Micro;
using System.Windows;
using WPF_DB_Person.Views;

namespace WPF_DB_Person
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
