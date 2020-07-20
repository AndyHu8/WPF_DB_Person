using Caliburn.Micro;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using WPF_DB_Person.Models;

namespace WPF_DB_Person.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        private string name;
        private string alter;
        private string jahr;
        private string herkunft;
        private string beruf;

        //readonly Settings Settings;
        public BindableCollection<PersonModel> Personen { get; set; }
        public BindableCollection<PersonModel> SichtbarePersonen { get; set; }
        public string Suchtext { get; set; }

        public ShellViewModel()
        {
            /*this.HasChanged(() => Suchtext)
                .Merge(this.HasChanged(() => Personen))
                .Throttle(TimeSpan.FromMilliseconds(250))
                .ObserveOnDispatcher()
                .Subscribe(_ => SuchePersonen());*/
            //Personen = Listeladen.Person_laden();
            Personen = Liste_laden();
        }


        public string Name { get => name;
            set {
                name = value;
                NotifyOfPropertyChange("Name");
            } }
        public string Alter { get => alter;
            set {
                alter = value;
                NotifyOfPropertyChange("Alter");
            } }
        public string Jahr { get => jahr;
            set {
                jahr = value;
                NotifyOfPropertyChange("Jahr");
            } }
        public string Herkunft { get => herkunft;
            set {
                herkunft = value;
                NotifyOfPropertyChange("Herkunft");
            } }
        public string Beruf { get => beruf;
            set {
                beruf = value;
                NotifyOfPropertyChange("Beruf");
            } }

        public void Senden()
        {
            var sql = @"INSERT INTO [Test_DB_Andy].[dbo].[T_WPF_Person] (Name, [Alter], Geburtsjahr, Herkunft, Beruf)
                        VALUES(@Name, @Alter, @Geburtsjahr, @Herkunft, @Beruf)";

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

        public BindableCollection<PersonModel> Liste_laden()
        {
            var sql = "SELECT * FROM[Test_DB_Andy].[dbo].[T_WPF_Person]";

            using (IDbConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Test_DB_Andy; Integrated Security = True"))
            {
                var result = connection.Query<PersonModel>(sql);
                return new BindableCollection<PersonModel>(result);
            }
        }

        public void SuchePersonen()
        {

        }
    }
}
