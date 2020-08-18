using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System.Windows.Input;

namespace WPF_DB_Person.Views
{
    /// <summary>
    /// Interaktionslogik für ShellView.xaml
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();
            ThemeManager.Current.ChangeTheme(this, "Light.Blue");
        }

        //public void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    ShowUpdate secondWin = new ShowUpdate();
        //    secondWin.ShowDialog();
        //}
    }
}
