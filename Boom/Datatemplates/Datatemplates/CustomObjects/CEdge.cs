using GraphSharp.Controls;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datatemplates.CustomObjects
{
    public class CEdge: Edge<Object>, INotifyPropertyChanged
    {
        private String m_Bez;

        public String Bezeichnung
        {
            get { return m_Bez; }
            set { m_Bez = value; OnPropertyChanged("Bezeichnung"); }
        }
        

        // Default Konstruktor
        public CEdge(Object source, Object target)
            : base(source, target)
        {
            Bezeichnung = "";
        }

        // Konstruktor
        public CEdge(Object source, Object target, String bezeichnung)
            : base(source, target)
        {
            Bezeichnung = bezeichnung;
        }

        public void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
