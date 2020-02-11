using Projet4Metier;
using Projet4Metier.Service;
using System;
using System.Collections.Generic;
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

namespace Projet4WPF.Pages
{
    /// <summary>
    /// Logique d'interaction pour Data.xaml
    /// </summary>
    public partial class Data : Page
    {
        Manager man;
        MainWindow mainWindow;
        bool run;

        private void getDataByTime(DateTime tStart, DateTime tEnd)
        {
            tableau.Items.Clear();
            List<MStatement> lms = man.GetAllStatementByDate(mainWindow.captor, tStart, tEnd.AddDays(1));
            foreach (MStatement ms in lms)
            {
                tableau.Items.Add(ms);
            }
            capteurSN.Text = mainWindow.captor.Serial_number.ToString();
            nbrEntries.Text = lms.Count().ToString();
            debut.SelectedDate = tStart;
            fin.SelectedDate = tEnd;
            debut.DisplayDate = tStart;
            fin.DisplayDate = tEnd;
            debut.Text = tStart.ToString();
            fin.Text = tEnd.ToString();

        }

        private void getAllData()
        {
            tableau.Items.Clear();
            List<MStatement> lms = man.GetAllStatement(mainWindow.captor);
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            bool flag = false;
            foreach (MStatement ms in lms)
            {
                tableau.Items.Add(ms);
                if (!flag)
                {
                    start = ms.DateTime;
                    end = ms.DateTime;
                    flag = true;
                }
                else
                {
                    if (start > ms.DateTime)
                        start = ms.DateTime;
                    if (end < ms.DateTime)
                        end = ms.DateTime;
                }
            }
            capteurSN.Text = mainWindow.captor.Serial_number.ToString();
            nbrEntries.Text = lms.Count().ToString();
            debut.SelectedDate = start;
            fin.SelectedDate = end;
            debut.DisplayDate = start;
            fin.DisplayDate = end;
            debut.DisplayDateStart = start;
            debut.DisplayDateEnd = end;
            fin.DisplayDateStart = start;
            fin.DisplayDateEnd = end;
            debut.Text = start.ToString();
            fin.Text = end.ToString();
        }

        public Data(MainWindow mw)
        {
            run = false;
            InitializeComponent();
            mainWindow = mw;
            man = new Manager();
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();
            tableau.Columns.Add(col1);
            tableau.Columns.Add(col2);
            tableau.Columns.Add(col3);
            col1.Binding = new Binding("DateTime");
            col2.Binding = new Binding("Temperature");
            col3.Binding = new Binding("Humidity");
            col1.Header = "DateTime";
            col2.Header = "Temperature";
            col3.Header = "Humidity";
            col1.Width = 400;
            col2.Width = 150;
            col3.Width = 150;
            this.getAllData();
            run = true;
        }
        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (debut.SelectedDate.HasValue && fin.SelectedDate.HasValue && run)
                getDataByTime(debut.SelectedDate.Value, fin.SelectedDate.Value);
        }



        // Menu
        public void GoToAccueil(object sender, RoutedEventArgs e)
        {
            mainWindow._mainwindows.Content = new Accueil(mainWindow);
        }

        public void GoToGraphique(object sender, RoutedEventArgs e)
        {
            mainWindow._mainwindows.Content = new Graphique(mainWindow);
        }

        public void Import(object sender, RoutedEventArgs e)
        {
            mainWindow._mainwindows.Content = new ImportPage(mainWindow);
        }
    }
}
