using Microsoft.Win32;
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

namespace Razor2Rail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //C:\Program Files(x86)\Ultima Online Outlands\ClassicUO\Data\Plugins\Assistant\Macros
        string fileName = null;
        public static List<string> splitStrings = new();
        public static List<string> formattedRailStrings = new();

        public MainWindow()
        {
            InitializeComponent();


        }

        private void FormatRails(List<string> railLocationStrings)
        {
            foreach(string railLoc in railLocationStrings)
            {
                int finishedRailLoc = Int32.Parse(railLoc) + 1;

                formattedRailStrings.Add("pushlist moveList '"+finishedRailLoc.ToString()+"'");
            }
            SaveToFile();
        }
        private void ParseDataFile(string dataFile)
        {     
            var lines = File.ReadAllLines(dataFile);

            foreach (var line in lines)
            {
                string[] splitLine = line.Split("|");
                if (splitLine[0].ToString() == "Assistant.Macros.WalkAction")
                {
                    splitStrings.Add(splitLine[1]);
                }
                
            }
            FormatRails(splitStrings);
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Program Files(x86)\Ultima Online Outlands\ClassicUO\Data\Plugins\Assistant\Macros";
            openFileDialog.Filter = "All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
            }

            string[] fileNameSplit = fileName.Split("\\");
            Brush colorBrush = new SolidColorBrush(Color.FromArgb(255, 0, 100, 255));
            lbl_Instructions.Foreground = colorBrush;
            lbl_Instructions.Content = fileNameSplit.Last() + " Selected";
            lbl_Instructions.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void SaveToFile()
        {
            TextWriter writer = new StreamWriter("Converted Rails.txt");

            foreach (string line in formattedRailStrings)
            {
                writer.WriteLine(line);
            }
            Brush colorBrush = new SolidColorBrush(Color.FromArgb(255, 43, 255, 0));
            lbl_Instructions.Content = "Converted " + formattedRailStrings.Count.ToString() + " Lines";
            lbl_Instructions.Foreground = colorBrush;
            writer.Close();
        }

        private void btn_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
            btn_Convert.IsEnabled = true;
        }

        private void btn_Convert_Click(object sender, RoutedEventArgs e)
        {
            ParseDataFile(fileName);
            btn_Convert.IsEnabled = false;

        }
    }
}
