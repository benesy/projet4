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
        public Data(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
            man = new Manager();
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now; ;
            List<MStatement> lms = man.GetAllStatement(mainWindow.captor);

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
            capteurSN.Text = mw.captor.Serial_number.ToString();
            nbrEntries.Text = lms.Count().ToString();
            debut.SelectedDate = start;
            fin.SelectedDate = end;
            debut.DisplayDateStart = start;
            debut.DisplayDateEnd = end;
            fin.DisplayDateStart = start;
            fin.DisplayDateEnd = end;
            debut.Text = start.ToString();
            fin.Text = end.ToString();
        }
    }
}
