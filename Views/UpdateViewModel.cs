using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using WPF_DB_Person.Models;

namespace WPF_DB_Person.Views
{
    public class UpdateViewModel
    {
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

        public void Updaten(UpdateViewModel e) //e ist noch null
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

            ShellViewModel shellViewModel = new ShellViewModel();
            shellViewModel.Initialize(e);
        }
    }

}
