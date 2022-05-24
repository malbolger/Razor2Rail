using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        //TODO: handle errors and bad input types
        static int x;
        static int y;
        static int directionMovedLast = 1;

        //Output ready lists.
        public static List<string> moveList = new();
        public static List<string> xCoord = new();
        public static List<string> yCoord = new();
        private bool lumberScript;
        private int startingX;
        private int startingY;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FormatRails(List<string> railLocationStrings)
        {
            //Create moveList
            foreach (string railLoc in railLocationStrings)
            {
                //Set the direction to be compatable with our script in game.
                int finishedRailLoc = Int32.Parse(railLoc) + 1;

                moveList.Add("pushlist moveList '" + finishedRailLoc.ToString() + "'");

            }


            directionMovedLast = Combo_Direction.SelectedIndex;

            //Create xCoord and yCoord Lists
            foreach (string nextStep in railLocationStrings)
            {
                //Set the direction to be compatable with our script in game.
                int railDirection = Int32.Parse(nextStep) + 1;

                switch (railDirection)
                {
                    //North (X + 0 and Y - 1)
                    case 1:
                        {
                            if (directionMovedLast == railDirection)
                            {
                                string xToHex = x.ToString("X");
                                string yToHex = (y - 1).ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x;
                                y = y - 1;
                                directionMovedLast = railDirection;

                            }
                            else
                            {
                                string xToHex = x.ToString("X");
                                string yToHex = y.ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x;
                                y = y;
                                directionMovedLast = railDirection;

                            }
                            break;
                        }
                    //Right (X + 1 and Y - 1)
                    case 2:
                        {
                            if (directionMovedLast == railDirection)
                            {
                                string xToHex = (x + 1).ToString("X");
                                string yToHex = (y - 1).ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x + 1;
                                y = y - 1;
                                directionMovedLast = railDirection;

                            }
                            else
                            {
                                string xToHex = x.ToString("X");
                                string yToHex = y.ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x;
                                y = y;
                                directionMovedLast = railDirection;

                            }

                            break;
                        }
                    //East (X + 1 and Y + 0) 
                    case 3:
                        {
                            if (directionMovedLast == railDirection)
                            {
                                string xToHex = (x + 1).ToString("X");
                                string yToHex = y.ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x + 1;
                                y = y;
                                directionMovedLast = railDirection;
                            }
                            else
                            {
                                string xToHex = x.ToString("X");
                                string yToHex = y.ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x;
                                y = y;
                                directionMovedLast = railDirection;

                            }

                            break;
                        }
                    //DOWN (X + 1 and Y + 1)
                    case 4:
                        {
                            if (directionMovedLast == railDirection)
                            {
                                string xToHex = (x + 1).ToString("X");
                                string yToHex = (y + 1).ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x + 1;
                                y = y + 1;
                                directionMovedLast = railDirection;
                            }
                            else
                            {
                                string xToHex = x.ToString("X");
                                string yToHex = y.ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x;
                                y = y;
                                directionMovedLast = railDirection;

                            }

                            break;
                        }
                    //South (X + 0 and Y + 1)
                    case 5:
                        {
                            if (directionMovedLast == railDirection)
                            {
                                string xToHex = x.ToString("X");
                                string yToHex = (y + 1).ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x;
                                y = y + 1;
                                directionMovedLast = railDirection;
                            }
                            else
                            {
                                string xToHex = x.ToString("X");
                                string yToHex = y.ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x;
                                y = y;
                                directionMovedLast = railDirection;

                            }

                            break;
                        }
                    //Left (X - 1 and Y + 1
                    case 6:
                        {
                            if (directionMovedLast == railDirection)
                            {
                                string xToHex = (x - 1).ToString("X");
                                string yToHex = (y + 1).ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x - 1;
                                y = y + 1;
                                directionMovedLast = railDirection;
                            }
                            else
                            {
                                string xToHex = x.ToString("X");
                                string yToHex = y.ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x;
                                y = y;
                                directionMovedLast = railDirection;

                            }

                            break;
                        }
                    //West (X -1 and Y + 0) 
                    case 7:
                        {
                            if (directionMovedLast == railDirection)
                            {
                                string xToHex = (x - 1).ToString("X");
                                string yToHex = y.ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x - 1;
                                y = y;
                                directionMovedLast = railDirection;
                            }
                            else
                            {
                                string xToHex = x.ToString("X");
                                string yToHex = y.ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x;
                                y = y;
                                directionMovedLast = railDirection;

                            }
                            break;
                        }
                    //Up (X - 1 and Y - 1)
                    case 8:
                        {
                            if (directionMovedLast == railDirection)
                            {
                                string xToHex = (x - 1).ToString("X");
                                string yToHex = (y - 1).ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x - 1;
                                y = y - 1;
                                directionMovedLast = railDirection;
                            }
                            else
                            {
                                string xToHex = x.ToString("X");
                                string yToHex = y.ToString("X");

                                xCoord.Add("pushlist xCoord '0x" + xToHex + "'");
                                yCoord.Add("pushlist yCoord '0x" + yToHex + "'");

                                x = x;
                                y = y;
                                directionMovedLast = railDirection;

                            }
                            break;
                        }
                    default:
                        break;
                }
            }

            //Save our lists to text to be added in game.
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
            lbl_Instructions2.Visibility = Visibility.Collapsed;
        }

        private void SaveToFile()
        {

            TextWriter writer = new StreamWriter(startingX.ToString()+"-"+startingY.ToString()+".txt");

            //moveList Section
            writer.WriteLine("# Starting Location " + startingX.ToString() + " - " + startingY.ToString());
            writer.WriteLine("overhead '::: rail loading :::'");
            writer.WriteLine("if not listexists moveList");
            writer.WriteLine("    " + "createlist moveList");
            foreach (string line in moveList)
            {
                writer.WriteLine("    " + line);
            }
            writer.WriteLine("endif");
            writer.WriteLine(String.Empty);

            //xCoord Section
            writer.WriteLine("if not listexists xCoord");
            writer.WriteLine("    " + "createlist xCoord");
            foreach (string line in xCoord)
            {
                writer.WriteLine("    " + line);
            }
            writer.WriteLine("endif");
            writer.WriteLine(String.Empty);

            //yCoordSection
            writer.WriteLine("if not listexists yCoord");
            writer.WriteLine("    " + "createlist yCoord");
            foreach (string line in yCoord)
            {
                writer.WriteLine("    " + line);
            }
            writer.WriteLine("endif");
            writer.WriteLine("overhead '::: rail loaded :::'");
            if (lumberScript == true)
            {
                writer.WriteLine("script 'Harvester'");
            }
            else
            {
                writer.WriteLine("script 'M_Harvester'");
            }
            writer.WriteLine(String.Empty);

            writer.Close();

            //Update UI
            Brush colorBrush = new SolidColorBrush(Color.FromArgb(255, 43, 255, 0));
            lbl_Instructions.Content = "Converted " + moveList.Count.ToString() + " Lines";
            lbl_Instructions.Foreground = colorBrush;
        }

        private void btn_Open_Click(object sender, RoutedEventArgs e)
        {
            //clear our lists and get ready to accept information
            splitStrings.Clear();
            formattedRailStrings.Clear();
            moveList.Clear();
            xCoord.Clear();
            yCoord.Clear();


            OpenFile();
            btn_Convert.IsEnabled = true;
        }

        private void btn_Convert_Click(object sender, RoutedEventArgs e)
        {
            //Quick and Dirty.
            x = Int32.Parse(txtBox_xCoord.Text);
            y = Int32.Parse(txtBox_yCoord.Text);

            startingX = x;
            startingY = y;

            ParseDataFile(fileName);
            btn_Convert.IsEnabled = false;

        }
        //Make sure that only intergers can be input into our text boxes
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        //Remove our default text and remove the handler so it wont work a second time.
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = String.Empty;
            textBox.GotFocus -= TextBox_GotFocus;
        }

        private void chkBox_Lumber_Checked(object sender, RoutedEventArgs e)
        {
            chkBox_Mining.IsChecked = false;

            lumberScript = true;
        }

        private void chkBox_Mining_Checked(object sender, RoutedEventArgs e)
        {
            chkBox_Lumber.IsChecked = false;
            lumberScript = false;
        }
    }
}



