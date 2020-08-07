using Caliburn.Micro;
using Dapper;
using PropertyChanged;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_DB_Person.Models;
using WPF_DB_Person.Views;

namespace WPF_DB_Person.ViewModel
{
    public class ShellViewModel : PropertyChangedBase
    {
        //readonly Settings Settings;
        public BindableCollection<PersonModel> Personen { get; set; }
        public BindableCollection<PersonModel> SichtbarePersonen { get; set; }
        public string Suchtext { get; set; }

        public ShellViewModel()
        {
            this.HasChanged(() => Suchtext)
                .Merge(this.HasChanged(() => Personen))
                .Throttle(TimeSpan.FromMilliseconds(250))
                .ObserveOnDispatcher()
                .Subscribe(_ => SuchePersonen());
            Aktualisieren(); 
        }


        public string Name { get; set; }
        public string Alter { get; set; }
        public string Jahr { get; set; }
        public string Herkunft { get; set; }
        public string Beruf { get; set; }

        public bool CanSenden
        {
            get { 
                return !string.IsNullOrWhiteSpace(Name) && 
                    !string.IsNullOrWhiteSpace(Alter) && 
                    !string.IsNullOrWhiteSpace(Jahr) && 
                    !string.IsNullOrWhiteSpace(Herkunft) &&
                    !string.IsNullOrWhiteSpace(Beruf); }           
        }

        public void Senden()
        {

            if (CanSenden)
            {
                var sql = @"INSERT INTO [Test_DB_Andy].[dbo].[T_WPF_Person] (Name, [Alter], Geburtsjahr, Herkunft, Beruf, Geschlecht, Hobbys, Nachricht)
                        VALUES(@Name, @Alter, @Geburtsjahr, @Herkunft, @Beruf, @Geschlecht, @Hobbys, @Nachricht)";

                using (IDbConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Test_DB_Andy; Integrated Security = True"))
                {
                    connection.Execute(sql, new PersonModel { Name = Name, Alter = Alter, Geburtsjahr = Jahr, Herkunft = Herkunft, Beruf = Beruf });
                }

                Name = null;
                Alter = null;
                Jahr = null;
                Herkunft = null;
                Beruf = null;
            } 
        }

        public void Aktualisieren()
        {
            var sql = "SELECT * FROM[Test_DB_Andy].[dbo].[T_WPF_Person]";

            using (IDbConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Test_DB_Andy; Integrated Security = True"))
            {
                var result = connection.Query<PersonModel>(sql);
                Personen =  new BindableCollection<PersonModel>(result);
            }
        }

        public void SuchePersonen()
        {
            if (string.IsNullOrEmpty(Suchtext)) //Wenn Suchtext == null || leer
            {
                SichtbarePersonen = Personen; //Gebe Personen aus (ganze Liste)
                return;
            }

            if (Personen == null) //Wenn Liste null => Gebe aus
                return;

            var sichtbarePersonen = Personen //Neue Var = Liste gleich dem Suchtext
                .Where(m => m.Name.ToString().ToUpper().Contains(Suchtext.ToUpper())
                || m.Alter.ToUpper().Contains(Suchtext.ToUpper())
                || m.Geburtsjahr.ToUpper().Contains(Suchtext.ToUpper())
                || m.Herkunft.ToUpper().Contains(Suchtext.ToUpper())
                || m.Beruf.ToUpper().Contains(Suchtext.ToUpper()));

            SichtbarePersonen = new BindableCollection<PersonModel>(sichtbarePersonen); //SichtbarePersonen = Neue Var
        }

        public void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Diese Textbox darf nicht leer sein.");
            }
        }

        public void Datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowUpdate secondWin = new ShowUpdate(); 
            secondWin.ShowDialog();
        }
    }
}
