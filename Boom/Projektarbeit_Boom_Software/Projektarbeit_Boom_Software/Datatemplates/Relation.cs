using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektarbeit
{
    public class Relation:Dictionary<String,List<Object>>
    {
        private String m_bez;

        public String Bezeichnung
        {
            get { return m_bez; }
            set { m_bez = value; }
        }
        
        public Relation() {
            Bezeichnung = "Mehr anzeigen...";
        }

        public Relation(String bezeichung) {
            Bezeichnung = "Mehr " + bezeichung + " anzeigen...";
        }
        public override string ToString()
        {
            return Bezeichnung;
        }
    }
}
