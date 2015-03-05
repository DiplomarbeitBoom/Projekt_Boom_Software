using Projektarbeit.CustomObjects;
using Projektarbeit;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projektarbeit
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ViewModel m_VM;
        private String m_EdgeBez;
        private Object lastRoot;

        public String EdgeBezeichnung
        {
            get { return m_EdgeBez; }
            set { m_EdgeBez = value; OnPropertyChanged("EdgeBezeichnung"); }
        }
        

        public ViewModel viewModel
        {
            get { return m_VM; }
            set { m_VM = value; OnPropertyChanged("viewModel"); }
        }
        

        public MainWindow()
        {
            InitializeComponent();
            viewModel = (ViewModel)this.Resources["Viewmodel"];


            this.DataContext = viewModel;
    
        }

        //private void onButtonDownFirma(object sender, RoutedEventArgs e)
        //{

        //    String firmenname = ((Button)e.Source).Content.ToString();

        //    // Root "deaktivieren"
        //    foreach (Firma p in viewModel.firmen) p.isRoot = false;

        //    Firma root = viewModel.firmen.Where(f => f.FirmenName == firmenname).FirstOrDefault();
        //    viewModel.firmen.Where(f => f.FirmenName == firmenname).FirstOrDefault().isRoot = true;
        //    viewModel.CreateGraphToVisualize();
        //}

        //private void onButtonDownPerson(object sender, RoutedEventArgs e)
        //{
        //    String name = ((Button)e.Source).Content.ToString();

        //    // Root "deaktivieren"
        //    foreach (Person p in viewModel.personen) p.isRoot = false;

        //    Person root = viewModel.personen.Where(p => p.Name == name).FirstOrDefault();
        //    viewModel.personen.Where(p => p.Name == name).FirstOrDefault().isRoot = true;
        //    viewModel.CreateGraphToVisualize();

        //    // Assoc
        //    //EdgeBezeichnung = viewModel.getPersAssoc();

        //    RefreshList();
        //}
        private void OnButtonDownObject(object sender, RoutedEventArgs e){
            //Neue Root!
            var vm = ((Button)sender).DataContext;
            //Überprüfen ob es sich mit Sicherheit im eine Rootable handelt!
            //if (vm.GetType() == typeof(Rootable)) {
                foreach (Rootable RootableVertices in m_VM.Vertices) {
                    if (RootableVertices.isRoot == true) { lastRoot = RootableVertices; }
                    RootableVertices.UnsetAsRoot();
                }
                Rootable r = (Rootable)vm;
                ((Rootable)m_VM.Vertices.Find(x => x == r)).SetAsRoot();
                m_VM.CreateGraphToVisualize();
            //}
        }

        //private void OnButtonDownAdresse(object sender, RoutedEventArgs e)
        //{

        //    String adresse = ((Button)e.Source).Content.ToString();

        //    // Root "deaktivieren"
        //    foreach (Adresse p in viewModel.adressen) p.isRoot = false;

        //    Adresse root = viewModel.adressen.Where(a => a.Anschrift == adresse).FirstOrDefault();
        //    viewModel.adressen.Where(a => a.Anschrift == adresse).FirstOrDefault().isRoot = true;
        //    viewModel.CreateGraphToVisualize();
        //}

        //private void OnButtonDownTelefon(object sender, RoutedEventArgs e)
        //{
        //    String telNr = ((Button)e.Source).Content.ToString();

        //    // Root "deaktivieren"
        //    foreach (Telefon p in viewModel.telefone) p.isRoot = false;

        //    Telefon root = viewModel.telefone.Where(t => t.TelefonNummer == telNr).FirstOrDefault();
        //    viewModel.telefone.Where(t => t.TelefonNummer == telNr).FirstOrDefault().isRoot = true;
        //    viewModel.CreateGraphToVisualize();
        //}


        private void OnChangeName(object sender, RoutedEventArgs e)
        {
            String[] namen = NamenBox.Text.Split(' ');

            viewModel.personen.Where(p => p.isRoot).FirstOrDefault().changeName(namen[0], namen[1]);
            viewModel.CreateGraphToVisualize();
        }





        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        private void OnButtonDownRelation(object sender, RoutedEventArgs e)
        {
            //Neue Root!
            var vm = ((Button)sender).DataContext;
            Relation r = (Relation)vm;

        }

        private void OnGoBack(object sender, RoutedEventArgs e)
        {

            //Neue Root!
            var vm = ((Button)sender).DataContext;
            //Überprüfen ob es sich mit Sicherheit im eine Rootable handelt!



            foreach (Rootable RootableVertices in m_VM.Vertices)
            {
                RootableVertices.UnsetAsRoot();
            }
            Rootable r = (Rootable)lastRoot;
            ((Rootable)m_VM.Vertices.Find(x => x == lastRoot)).SetAsRoot();
            m_VM.CreateGraphToVisualize();
        }
    }
}


   
