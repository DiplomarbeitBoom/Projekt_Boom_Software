using Projektarbeit;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projektarbeit.CustomObjects;

namespace Projektarbeit
{
    public class ViewModel: INotifyPropertyChanged
    {
        public Firma[] firmen;
        public Adresse[] adressen;
        public Telefon[] telefone;
        public Person[] personen;
        public List<Object> Vertices;

        private CGraph _graphToVisualize;

        public ViewModel()
        {
            // Testdatensätze
            firmen = new Firma[]
            {
                new Firma { FirmenID = 1, FirmenName = "SSI Schäfer"},
                new Firma { FirmenID = 2, FirmenName = "BOOM Software"},
                new Firma { FirmenID = 3, FirmenName = "Knapp AG"}
            };

            adressen = new Adresse[]
            {
                new Adresse { AdressID = 1, Strasse = "Nebenstraße", Hausnummer = 12, PLZ = "8083", Ort = "Glojach"},
                new Adresse { AdressID = 2, Strasse = "Grazerstraße", Hausnummer = 50, PLZ = "8083", Ort = "St. Stefan i. R."},
                new Adresse { AdressID = 3, Strasse = "Herrengasse", Hausnummer = 60, PLZ = "1353", Ort = "Dollrath"}
            
            };


            telefone = new Telefon[]
            {
                new Telefon { TelefonNummer = "0664 123 456", TelefonTyp = TelefonTyp.Mobiltelefon },
                new Telefon { TelefonNummer = "0664 321 654", TelefonTyp = TelefonTyp.VOIP },
                new Telefon { TelefonNummer = "0676 123 456", TelefonTyp = TelefonTyp.Mobiltelefon },
                new Telefon { TelefonNummer = "0660 123 456", TelefonTyp = TelefonTyp.Festnetz },
                new Telefon { TelefonNummer = "0800 123 456", TelefonTyp = TelefonTyp.Mobiltelefon  },
                new Telefon { TelefonNummer = "03116 123 456", TelefonTyp = TelefonTyp.Festnetz },
            };

            personen = new Person[]
            {
                new Person { PersID = 1, Vorname = "Philipp", Nachname = "Meier", Geburtsdatum = new DateTime(1996, 1, 14) },
                new Person { PersID = 2, Vorname = "Olaf", Nachname = "Günther", Geburtsdatum = new DateTime(1980, 2, 10) },
                new Person { PersID = 3, Vorname = "Florian", Nachname = "Leber", Geburtsdatum = new DateTime(1996, 3, 14)},
                new Person { PersID = 4, Vorname = "Kevin", Nachname = "Gritsch", Geburtsdatum = new DateTime(1983, 7, 10)  },
                new Person { PersID = 5, Vorname = "Marco", Nachname = "Milwisch", Geburtsdatum = new DateTime(1984, 5, 14) },
                new Person { PersID = 6, Vorname = "Valentin", Nachname = "Gruber", Geburtsdatum = new DateTime(1990, 2, 10) },
                new Person { PersID = 7, Vorname = "David", Nachname = "Resch", Geburtsdatum = new DateTime(1986, 6, 14) },
                new Person { PersID = 8, Vorname = "Sepp", Nachname = "Müller", Geburtsdatum = new DateTime(1983, 9, 10) },
                new Person { PersID = 9, Vorname = "Veronika", Nachname = "Neuwirth", Geburtsdatum = new DateTime(1987, 11, 14) },
                new Person { PersID = 10, Vorname = "Roman", Nachname = "Lindt", Geburtsdatum = new DateTime(1981, 2, 10)  },
                new Person { PersID = 11, Vorname = "Simon", Nachname = "Krobath", Geburtsdatum = new DateTime(1993, 5, 14)},
                new Person { PersID = 12, Vorname = "Christian", Nachname = "Hirschmugl", Geburtsdatum = new DateTime(1978, 3, 10) },
                new Person { PersID = 13, Vorname = "Roman", Nachname = "Lindt", Geburtsdatum = new DateTime(1981, 2, 10) },
                new Person { PersID = 14, Vorname = "Simon", Nachname = "Krobath", Geburtsdatum = new DateTime(1993, 5, 14) },
                new Person { PersID = 15, Vorname = "Lisa", Nachname = "Müller", Geburtsdatum = new DateTime(1993, 5, 14) }
            };

            //Relationen
            personen[0].Firma.Add("Innhaber von", new List<Object>{firmen[0]});
            personen[0].Personen.Add("ist Chef von", new List<Object> { personen[1], personen[2],
                                                                        personen[4], personen[3],
                                                                        personen[5], personen[6]});
            personen[1].Personen.Add("arbeitet für", new List<Object> { personen[0] }); 
            personen[1].Firma.Add("Angestellter bei", new List<Object>{ firmen[0] } );
            personen[2].Firma.Add("Angestellter bei", new List<Object> { firmen[0] });
            personen[3].Firma.Add("Angestellter bei", new List<Object> { firmen[0] });
            personen[4].Firma.Add("Angestellter bei", new List<Object> { firmen[0] });
            personen[5].Firma.Add("Angestellter bei", new List<Object> { firmen[0] });
            personen[6].Firma.Add("Angestellter bei", new List<Object> { firmen[0] });
            firmen[0].Personen.Add("Eigentümer ist", new List<Object>{personen[0]});
            firmen[0].Personen.Add("Angestellter ist", new List<Object>{personen[1]});
            // Startselection
            personen[0].SetAsRoot();

            Vertices = new List<Object>();
            Vertices.AddRange(personen);
            Vertices.AddRange(telefone);
            Vertices.AddRange(firmen);
            Vertices.AddRange(adressen);
           
            //Zeichnen 
            CreateGraphToVisualize();
        }

        public void CreateGraphToVisualize()
        {
            var Graph = new CGraph();
            Object root = Vertices.Where(x => x is Rootable).Where(x => ((Rootable)x).isRoot).FirstOrDefault();
            
            //1. Add root to Graph!
            Graph.AddVertex(root);
            foreach (var prop in root.GetType().GetProperties().Where(x => x.PropertyType == typeof(Relation))) {
                //Get Relation
                Relation r = (Relation)prop.GetValue(root);
                //Get all related Objects!
                if (r.Count >= 2) {
                    //Add Relation
                    Graph.AddVertex(r);
                    Graph.AddEdge(new CEdge(root,r));
                }else{
                    foreach(KeyValuePair<String,List<object>> kvp in r){
                        foreach (Object o in kvp.Value) {
                            //Add Vertex to Graph
                            Graph.AddVertex(o);
                            //Add Edges to Graph
                            Graph.AddEdge(new CEdge(root, o, kvp.Key));
                        }
                    }
                }   
            }
            
            GraphToVisualize = Graph;            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public CGraph GraphToVisualize
        {
            get { return _graphToVisualize; }
            set { _graphToVisualize = value; OnPropertyChanged("GraphToVisualize"); }
        }

    }
}
