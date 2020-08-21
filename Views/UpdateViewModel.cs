using Caliburn.Micro;
using WPF_DB_Person.Messages;
using WPF_DB_Person.Models;

namespace WPF_DB_Person.Views
{
    public class UpdateViewModel : Screen
    {
        private readonly IEventAggregator eventAggregator;

        public UpdateViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public string Name { get; set; }
        public int? Jahr { get; set; }
        public string Herkunft { get; set; }
        public string Beruf { get; set; }
        public char? Geschlecht { get; set; }
        public bool CheckedMale { get; set; }
        public bool CheckedFemale { get; set; }
        public string Hobbys { get; set; }
        public string Nachricht { get; set; }


        public void Initialize(PersonModel personModel)
        {
           Name = personModel.Name;
           Jahr = personModel.Geburtsjahr;
           Herkunft = personModel.Herkunft;
           Beruf = personModel.Beruf;
        }

        public void Updaten()
        {
            if (CheckedMale == true)
            {
                Geschlecht = 'M';
            }
            else if(CheckedFemale == true)
            {
                Geschlecht = 'W';
            }
            else
            {
                Geschlecht = null;
            }

            PersonModel person = new PersonModel { //Übergabe der Daten
                Name = Name, 
                Geburtsjahr = Jahr, 
                Herkunft = Herkunft, 
                Beruf = Beruf, 
                Geschlecht = Geschlecht, 
                Hobbys = Hobbys, 
                Nachricht = Nachricht 
            };

            eventAggregator.PublishOnUIThread(new UpdatePerson { Person = person }); //Person von der Klasse UpdatePerson
            TryClose();
        }
    }

}
