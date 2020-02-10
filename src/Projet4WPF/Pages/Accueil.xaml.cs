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
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Page
    {
        MainWindow mainWindow;
        Manager man;
        public Accueil(MainWindow mw)
        { 
            InitializeComponent();
            mainWindow = mw;
            man = new Manager();
            Titre.Text = "Bienvenue dans votre espace de consultation ! \n Veuillez sélectionnez un capteur.";
            List<MCaptor> mcaptorList = man.GetCaptorList();
            ComboCaptorList.ItemsSource = mcaptorList;
            ComboCaptorList.DisplayMemberPath = "Serial_number";
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.captor = (MCaptor)ComboCaptorList.SelectedItem;
            mainWindow._mainwindows.Content = new Data(mainWindow);
        }
    }
}
