using Projet4DAO;
using Projet4Metier;
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
    /// Logique d'interaction pour ImportPage.xaml
    /// </summary>
    public partial class ImportPage : Page
    {
        MainWindow mainWindow;
        public ImportPage(MainWindow mw)
        {
            mainWindow = mw;
            InitializeComponent();
            projet4Entities ctx = new projet4Entities();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            Nullable<bool> result = dlg.ShowDialog();
            displayFile.Text = "";
            if (result == true)
            {
                string filename = dlg.FileName;
                string[] file = Import.ReadFile(filename);
                foreach (string s in file)
                    displayFile.Text += string.Format("{0}\n", s);
                List<MStatement> mStatementslist = Import.ConvertFile(file);
                numberRows.Text = mStatementslist.Count().ToString();
                foreach (MStatement m in mStatementslist)
                {
                    m.CaptorId =  mainWindow.captor.CaptorId;
                    ctx.statement.Add(m.ConvertToDao());
                }
                ctx.SaveChanges();
            }
        }

        public void Valider(object sender, RoutedEventArgs e)
        {
            mainWindow._mainwindows.Content = new Data(mainWindow);
        }
    }

}
