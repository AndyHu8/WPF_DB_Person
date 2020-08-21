using Autofac;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using System.Windows;
using WPF_DB_Person.Views;

namespace WPF_DB_Person
{
    public class Bootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterType<ShellViewModel>().SingleInstance();
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance(); //erzeugt einmal Windowmanager
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance(); //erzeugt einmal EventAgg.
        }
    }
}
