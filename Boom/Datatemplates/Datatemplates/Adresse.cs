using Datatemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonendarstellungWPF
{
    public class Adresse: Rootable
    {
        //Properties
        public int AdressID { get; set; }
        public String Strasse { get; set; }
        public int Hausnummer { get; set; }
        public String Ort { get; set; }
        public String PLZ { get; set; }

        public String Anschrift { get { return String.Format("{0} {1}\n{2} {3}", Strasse, Hausnummer, PLZ, Ort); } }

        //Relationen
        //public List<Firma> Firmen { get; set; }
        //public List<Person> Personen { get; set; }
        public Relation Firmen { get; set; }
        public Relation Personen { get; set; }
        public Adresse()
        {
            Firmen = new Relation();
            Personen = new Relation();
        }

        public override string ToString()
        {
            return String.Format("{0} {1}\n{2} {3}", Strasse, Hausnummer, PLZ, Ort);
        }
    }
}
