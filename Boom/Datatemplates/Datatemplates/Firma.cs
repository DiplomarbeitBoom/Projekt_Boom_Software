using Datatemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonendarstellungWPF
{
    public class Firma: Rootable
    {
        //Properties
        // Eindeutiger Schlüssel
        public int FirmenID { get; set; }
        // Firmenname
        public String FirmenName { get; set; }

        //Relationen
        // Mitarbeiter der Firma
        //public List<Person> Personen { get; set; }
        public Relation Personen { get; set; }
        // Standort der Firma
        //public Adresse Adresse { get; set; }
        public Relation Adressen { get; set; }

        public Firma()
        {
            Personen = new Relation();
            Adressen = new Relation();
        }

        public override string ToString()
        {
            return FirmenName;
        }
    }
}
