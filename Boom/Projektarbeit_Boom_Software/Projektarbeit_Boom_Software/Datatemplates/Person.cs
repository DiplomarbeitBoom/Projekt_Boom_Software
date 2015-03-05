using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Projektarbeit;
using System.ComponentModel;

namespace Projektarbeit
{
    public class Person: Rootable, INotifyPropertyChanged
    {
        // Properties
        // Root -> Relationtabelle anzeigen, und nicht Roots löschen
        public int Ebene = 0;

        // Personendaten
        public int PersID { get; set; }
        public DateTime Geburtsdatum { get; set; }
        private String m_Nachname;
        private String m_Vorname;
        public String Nachname
        {
            get { return m_Nachname; }
            set { m_Nachname = value; onPropertyChanged("Nachname"); }
        }
        public String Vorname
        {
            get { return m_Vorname; }
            set { m_Vorname = value; onPropertyChanged("Vorname"); }
        }
        public BitmapImage Icon { get; set; }
        private String m_Name;
        public String Name
        {
            get { return Vorname + " " + Nachname; }
            set { m_Name = value; onPropertyChanged("Name"); }
        }
        
        // -- Relationen --
        // Firma der Person
        public Relation Firma { get; set; }
        
        // Adressen der Person
        public Relation Adressen { get; set; }
        
        // Telefonnummern
        public Relation Telefonnummern { get; set; }
        
        // Personen
        public Relation Personen { get; set; }

        public Person()
        {
            isRoot = false;
            Adressen = new Relation();
            Firma = new Relation();
            Telefonnummern = new Relation();
            Personen = new Relation();
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", Vorname, Nachname);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(String info)
        {
            if(PropertyChanged!=null){
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void changeName(String vorname, String nachname)
        {
            Vorname = vorname;
            Nachname = nachname;
            Name = vorname + " " + nachname;
        }
    }
}
