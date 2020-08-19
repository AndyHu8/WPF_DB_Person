using Caliburn.Micro;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reactive.Linq;
using WPF_DB_Person.Models;

namespace WPF_DB_Person.Views
{
    public class ShellViewModel : PropertyChangedBase
    {
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
        public int? Jahr { get; set; }
        public string Herkunft { get; set; }
        public string Beruf { get; set; }
        public char? Geschlecht { get; set; }
        public string Hobbys { get; set; }
        public string Nachricht { get; set; }

        public bool CanSenden
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Name) &&
                    !string.IsNullOrWhiteSpace(Convert.ToString(Jahr)) &&
                    !string.IsNullOrWhiteSpace(Herkunft) &&
                    !string.IsNullOrWhiteSpace(Beruf);
            }
        }

        public void Senden()
        {

            if (CanSenden)
            {
                var sql = @"INSERT INTO [Test_DB_Andy].[dbo].[T_WPF_Persons] (Name, Geburtsjahr, Herkunft, Beruf, Geschlecht, Hobbys, Nachricht)
                        VALUES(@Name, @Geburtsjahr, @Herkunft, @Beruf, @Geschlecht, @Hobbys, @Nachricht)";

                using (IDbConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Test_DB_Andy; Integrated Security = True"))
                {
                    connection.Execute(sql, new PersonModel { Name = Name, Geburtsjahr = Jahr, Herkunft = Herkunft, Beruf = Beruf });
                }

                Name = null;
                Jahr = null;
                Herkunft = null;
                Beruf = null;
            }
        }

        public void Aktualisieren()
        {
            var sql = "SELECT * FROM[Test_DB_Andy].[dbo].[T_WPF_Persons]";

            using (IDbConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Test_DB_Andy; Integrated Security = True"))
            {
                var result = connection.Query<PersonModel>(sql);
                Personen = new BindableCollection<PersonModel>(result);
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
                || m.Geburtsjahr.ToString().ToUpper().Contains(Suchtext.ToUpper())
                || m.Herkunft.ToUpper().Contains(Suchtext.ToUpper())
                || m.Beruf.ToUpper().Contains(Suchtext.ToUpper()));

            SichtbarePersonen = new BindableCollection<PersonModel>(sichtbarePersonen); //SichtbarePersonen = Neue Var
        }

        public void ShowDetails(PersonModel e)
        {
            UpdateViewModel updateViewModel = new UpdateViewModel();
            updateViewModel.Initialize(e);
            manager.ShowDialog(updateViewModel);
        }
        IWindowManager manager = new WindowManager();

        public void Initialize(UpdateViewModel updateViewModel)
        {
            Name = updateViewModel.Name;
            Jahr = updateViewModel.Jahr;
            Herkunft = updateViewModel.Herkunft;
            Beruf = updateViewModel.Beruf;
            Geschlecht = updateViewModel.Geschlecht;
            Hobbys = updateViewModel.Hobbys;
            Nachricht = updateViewModel.Nachricht;
        }

        public void UpdateData()
        {
            var sql = @"UPDATE [Test_DB_Andy].[dbo].[T_WPF_Persons] SET Name = @Name, Geburtsjahr = @Geburtsjahr,
                        Herkunft = @Herkunft, Beruf = @Beruf, Geschlecht = @Geschlecht, Hobbys = @Hobbys, Nachricht = @Nachricht
                        WHERE Name = @Name";

            using (IDbConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Test_DB_Andy; Integrated Security = True"))
            {
                connection.Execute(sql, new PersonModel { Name = Name, Geburtsjahr = Jahr, Herkunft = Herkunft, Beruf = Beruf, Geschlecht = Geschlecht, Hobbys = Hobbys, Nachricht = Nachricht });
            }
        }
    }
}
