using iTextSharp.text.pdf;
using Projet4Metier;
using Projet4Metier.Service;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
        public void CSVExport(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV files (*.csv)|*.csv";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                string csv = "ID; Heure; Temperature; Hygrometrie\n";
                if (debut.SelectedDate.HasValue && fin.SelectedDate.HasValue && run)
                {
                    List<MStatement> lms = man.GetAllStatementByDate(mainWindow.captor, debut.SelectedDate.Value, fin.SelectedDate.Value.AddDays(1));
                    foreach (MStatement s in lms)
                    {
                        csv += string.Format("{0};{1};{2};{3}\n", s.StatementId, s.DateTime, s.Temperature, s.Humidity);
                    }
                }
                else
                {
                    List<MStatement> lms = man.GetAllStatement(mainWindow.captor);
                    foreach (MStatement s in lms)
                    {
                        csv += string.Format("{0};{1};{2};{3}\n", s.StatementId, s.DateTime, s.Temperature, s.Humidity);
                    }

                }
                File.WriteAllText(filename, csv);
            }
        }
        public void PDFExport(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF files (*.pdf)|*.pdf";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string data = "";
                int lmsCount = 0;
                double tmpMax = 0;
                double tmpMin = 0;
                double tmpAvg = 0;
                double humMax = 0;
                double humMin = 0;
                double humAvg = 0;
                int ct = 0;
                bool first = true;

                Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
                PdfPageBase page = doc.Pages.Add();
                page.Canvas.DrawString("DATA REPORT",
                                        new Spire.Pdf.Graphics.PdfFont(PdfFontFamily.Helvetica, 20f),
                                        new PdfSolidBrush(new PdfRGBColor()),
                                        300, 10);
                if (debut.SelectedDate.HasValue && fin.SelectedDate.HasValue && run)
                {
                    List<MStatement> lms = man.GetAllStatementByDate(mainWindow.captor, debut.SelectedDate.Value, fin.SelectedDate.Value.AddDays(1));
                    foreach (MStatement s in lms)
                    {
                        data += string.Format("{0}.\t{1}\t{2}\t{3}\r\n", s.StatementId, s.DateTime, s.Temperature, s.Humidity);
                        if (first)
                        {
                            tmpMin = s.Temperature;
                            tmpMax = s.Temperature;
                            tmpAvg = s.Temperature;
                            humMin = s.Humidity;
                            humMax = s.Humidity;
                            humAvg = s.Humidity;
                            first = false;
                        }
                        else
                        {
                            if (tmpMin > s.Temperature)
                                tmpMin = s.Temperature;
                            if (tmpMax < s.Temperature)
                                tmpMax = s.Temperature;
                            tmpAvg += s.Temperature;
                            if (humMin > s.Humidity)
                                humMin = s.Humidity;
                            if (humMax < s.Humidity)
                                humMax = s.Humidity;
                            humAvg += s.Humidity;
                        }
                        ct++;
                    }
                    tmpAvg = tmpAvg/ct;
                    humAvg = humAvg/ct;
                    lmsCount = lms.Count;
                }
                else
                {
                    List<MStatement> lms = man.GetAllStatement(mainWindow.captor);
                    foreach (MStatement s in lms)
                    {
                        data += string.Format("{0}.\t{1}\t{2}\t{3}\r\n", s.StatementId, s.DateTime, s.Temperature, s.Humidity);
                        if (first)
                        {
                            tmpMin = s.Temperature;
                            tmpMax = s.Temperature;
                            tmpAvg = s.Temperature;
                            humMin = s.Humidity;
                            humMax = s.Humidity;
                            humAvg = s.Humidity;
                            first = false;
                        }
                        else
                        {
                            if (tmpMin > s.Temperature)
                                tmpMin = s.Temperature;
                            if (tmpMax < s.Temperature)
                                tmpMax = s.Temperature;
                            tmpAvg += s.Temperature;
                            if (humMin > s.Humidity)
                                humMin = s.Humidity;
                            if (humMax < s.Humidity)
                                humMax = s.Humidity;
                            humAvg += s.Humidity;
                        }
                        ct++;
                    }
                    tmpAvg = Math.Round(tmpAvg/ct, 1);
                    humAvg = Math.Round(humAvg/ct, 1);
                    lmsCount = lms.Count;
                }
                string info = string.Format("SN: {0}\nTotal Records: {1}\nTemp Max/Min/Avg: {2}/{3}/{4}\nHumidity Max/Min/Avg: {5}/{6}/{7}", mainWindow.captor.Serial_number, lmsCount, tmpMax, tmpMin, tmpAvg, humMax, humMin, humAvg);
                page.Canvas.DrawImage();
                page.Canvas.DrawString(info,
                        new Spire.Pdf.Graphics.PdfFont(PdfFontFamily.Helvetica, 12f),
                        new PdfSolidBrush(new PdfRGBColor()),
                        10, 60);

                page.Canvas.DrawString("Temperature and humidity data",
                        new Spire.Pdf.Graphics.PdfFont(PdfFontFamily.Helvetica, 18f),
                        new PdfSolidBrush(new PdfRGBColor()),
                        10, 140);

                page.Canvas.DrawString(data,
                                        new Spire.Pdf.Graphics.PdfFont(PdfFontFamily.Helvetica, 12f),
                                        new PdfSolidBrush(new PdfRGBColor()),
                                        10, 160);
                FileStream to_stream = new FileStream(dlg.FileName, FileMode.Create);
                doc.SaveToStream(to_stream);
                to_stream.Close();
                doc.Close();


            }
        }
    }
}
