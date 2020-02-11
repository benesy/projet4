using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
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
    /// Logique d'interaction pour Graphique.xaml
    /// </summary>
    public partial class Graphique : Page
    {
        Manager man;
        SeriesCollection sc;
        MainWindow mainWindow;
        bool run;
        public Func<double, string> Formatter { get; set; }

        private void getDataByTime(DateTime tStart, DateTime tEnd)
        {
            List<MStatement> lms = man.GetAllStatementByDate(mainWindow.captor, tStart, tEnd);
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            bool flag = false;
            var dayConfig = Mappers.Xy<DateModel>()
                .X(dateModel => dateModel.DateTime.Ticks)
                .Y(dateModel => dateModel.Value);
            sc = new SeriesCollection(dayConfig);
            LineSeries temp = new LineSeries();
            LineSeries humidity = new LineSeries();
            sc.Add(temp);
            sc.Add(humidity);
            temp.Title = "Température";
            humidity.Title = "Humidité";
            temp.Values = new ChartValues<DateModel>();
            humidity.Values = new ChartValues<DateModel>();
            foreach (MStatement ms in lms)
            {
                DateModel tempModel = new DateModel() { DateTime = ms.DateTime, Value = ms.Temperature };
                DateModel humidityModel = new DateModel() { DateTime = ms.DateTime, Value = ms.Humidity };
                Console.WriteLine(ms.DateTime.ToLongTimeString());
                temp.Values.Add(tempModel);
                humidity.Values.Add(humidityModel);
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
           Formatter = value => new DateTime((long)value).ToShortDateString() + " " + new DateTime((long)value).ToLongTimeString();
            graph.Series = sc;
            DataContext = this;
            capteurSN.Text = mainWindow.captor.Serial_number.ToString();
            nbrEntries.Text = lms.Count().ToString();

        }

        private void getAllData()
        {
            List<MStatement> lms = man.GetAllStatement(mainWindow.captor);
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            bool flag = false;
            var dayConfig = Mappers.Xy<DateModel>()
                .X(dateModel => dateModel.DateTime.Ticks)
                .Y(dateModel => dateModel.Value);
            sc = new SeriesCollection(dayConfig);
            LineSeries temp = new LineSeries();
            LineSeries humidity = new LineSeries();
            sc.Add(temp);
            sc.Add(humidity);
            temp.Title = "Température";
            humidity.Title = "Humidité";
            temp.Values = new ChartValues<DateModel>();
            humidity.Values = new ChartValues<DateModel>();

            foreach (MStatement ms in lms)
            {
                DateModel tempModel = new DateModel() {DateTime = ms.DateTime, Value = ms.Temperature };
                DateModel humidityModel = new DateModel() { DateTime = ms.DateTime, Value = ms.Humidity };
                Console.WriteLine(ms.DateTime.ToLongTimeString());
                temp.Values.Add(tempModel);
                humidity.Values.Add(humidityModel);
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
            Formatter = value => new DateTime((long)value).ToShortDateString() + " " +new DateTime((long)value).ToLongTimeString();
            graph.Series = sc;
            DataContext = this;
            capteurSN.Text = mainWindow.captor.Serial_number.ToString();
            nbrEntries.Text = lms.Count().ToString();
 /*                       debut.SelectedDate = start;
                        fin.SelectedDate = end;
                        debut.DisplayDate = start;
                        fin.DisplayDate = end;
                        debut.DisplayDateStart = start;
                        debut.DisplayDateEnd = end;
                        fin.DisplayDateStart = start;
                        fin.DisplayDateEnd = end;
                        debut.Text = start.ToString();
                        fin.Text = end.ToString();
            */
        }
        public Graphique(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
            man = new Manager();
            run = true;
            getAllData();
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

        public void GoToData(object sender, RoutedEventArgs e)
        {
            mainWindow._mainwindows.Content = new Data(mainWindow);
        }

    }
    public class DateModel
    {
        public System.DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}
