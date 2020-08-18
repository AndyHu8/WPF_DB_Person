using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using WPF_DB_Person.Models;
using WPF_DB_Person.Views;

namespace WPF_DB_Person.Views
{
    public class UpdateViewModel
    {
        public string Name { get; set; }
        public string Alter { get; set; }
        public string Jahr { get; set; }
        public string Herkunft { get; set; }
        public string Beruf { get; set; }
        public string Geschlecht { get; set; }
        public string Nachricht { get; set; }


        public void Initialize(PersonModel personModel)
        {
           Name = personModel.Name;
           Alter = personModel.Alter;
           Jahr = personModel.Geburtsjahr;
           Herkunft = personModel.Herkunft;
           Beruf = personModel.Beruf;
        }
    }

}
