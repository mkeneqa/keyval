using System;
using System.Reflection;
using System.Resources;
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
using System.Data.SQLite;
using System.IO;
using System.Timers;

namespace KeyValWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CheckIfDatabaseExsists();
            //lblDebugResults.Content = "Current Directory: " + Common.DatabaseDirectory;
        }

        private void CheckIfDatabaseExsists()
        {
            if (File.Exists(Common.DBFileLocation))
            {
                //file exsits
                lblDebugResults.Content = "Database Located!";
            }
            else
            {
                lblDebugResults.Foreground = Brushes.Red;
                lblDebugResults.Content = "ERROR: Could NOT Locate Database!";
                txtKeyIn1.IsEnabled = false;
                txtKeyIn2.IsEnabled = false;
                EditMenu.IsEnabled = false;
            }
            
        }

        private Values GetValuesFromInputKeys(String firstKey, String secondKey)
        {   
            
            Values myValues = new Values();
            using (SQLiteConnection Conn = new SQLiteConnection(Common.DatabaseConnString))
            {
                Conn.Open();

                SQLiteDataReader rdr = null;

                SQLiteCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "SELECT kvValue FROM MyKeys WHERE (kvKey = @firstKey)";

                SQLiteParameter param = null;

                param = new SQLiteParameter("@firstKey", firstKey);
                cmd.Parameters.Add(param);
                cmd.Prepare();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //var myvals = rdr.GetString(0);
                    myValues.FirstValue = rdr.GetString(0);
                }
                
                //get second value
                SQLiteCommand cmd2 = Conn.CreateCommand();
                cmd2.CommandText = "SELECT kvValue FROM MyKeys WHERE (kvKey = @secondKey)";

                param = new SQLiteParameter("@secondKey", secondKey);
                cmd2.Parameters.Add(param);

                cmd2.Prepare();
                rdr = cmd2.ExecuteReader();
                
                while (rdr.Read())
                {
                    myValues.SecondValue = rdr.GetString(0);
                }
                
                Conn.Close();
                rdr.Dispose();
            }

               return myValues;
        }


        private void btnClick_GetValues(object sender, RoutedEventArgs e)
        {
            string keyText1;
            string keyText2;

            keyText1 = txtKeyIn1.Text;
            keyText2 = txtKeyIn2.Text;

            Values resultValues = new Values();

            resultValues = GetValuesFromInputKeys(keyText1, keyText2);

            blockText.Text = resultValues.FirstValue.ToString() + resultValues.SecondValue.ToString();
            Clipboard.SetText(blockText.Text.ToString());
            lblDebugResults.Content = "Combination Value Copied to Clipboard!";
        }

        private void MenuEdit_KeyVal(object sender, RoutedEventArgs e)
        {
            //when edit button is pressed load new window
            KeyValEditWindow editWindow = new KeyValEditWindow();
            //editWindow.Show();
            editWindow.ShowDialog();
            ResetWindow();
            lblDebugResults.Content = "";
                
        }

        private void ResetWindow()
        {
            txtKeyIn1.Text = "";
            txtKeyIn2.Text = "";
            blockText.Text = "";
            btnGetValues.IsEnabled = false;
        }

        void DeactivateMainWindow(object sender, EventArgs e)
        { 
            //MainWindow.IsEnabledProperty
        }

        private void MenuFile_Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(-1);
        }

        private void getDatabaseDirectory_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Common.DBFileLocation);
            lblDebugResults.Content = "Database File Location Copied to Clipboard!";
        }

        private void btnClick_ClearAll(object sender, RoutedEventArgs e)
        {
            ResetWindow();
        }

        private void keyup_checkForNulls(object sender, KeyEventArgs e)
        {
            if (txtKeyIn1.Text != "" && txtKeyIn2.Text != "")
                btnGetValues.IsEnabled = true;

        }


    }
}
