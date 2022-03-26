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
                splitStrings.Add(splitLine[1]);             
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
                ParseDataFile(fileName);
            }
        }

        private static void SaveToFile()
        {
            TextWriter writer = new StreamWriter("Finished.txt");

            foreach (string line in formattedRailStrings)
            {
                writer.WriteLine(line);
            }
            writer.Close();
        }

        private void btn_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }
        private void btn_Format_Click(object sender, RoutedEventArgs e)
        {
            FormatRails(splitStrings);
        }
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile();
        }

    }
}
