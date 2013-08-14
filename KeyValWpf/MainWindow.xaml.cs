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
using System.Data.SqlServerCe;
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
            using (SqlCeConnection Conn = new SqlCeConnection(Common.DatabaseConnString))
            {
                Conn.Open();
                SqlCeDataReader rdr = null;

                SqlCeCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "SELECT kvValue FROM MyKeys WHERE (kvKey = @firstKey)";

                SqlCeParameter param = null;

                param = new SqlCeParameter("@firstKey", firstKey);
                cmd.Parameters.Add(param);
                cmd.Prepare();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //var myvals = rdr.GetString(0);
                    myValues.FirstValue = rdr.GetString(0);
                }
                
                //get second value
                SqlCeCommand cmd2 = Conn.CreateCommand();
                cmd2.CommandText = "SELECT kvValue FROM MyKeys WHERE (kvKey = @secondKey)";

                param = new SqlCeParameter("@secondKey",secondKey);
                cmd2.Parameters.Add(param);

                cmd2.Prepare();
                rdr = cmd2.ExecuteReader();
                
                while (rdr.Read())
                {
                    myValues.SecondValue = rdr.GetString(0);
                }
                
                Conn.Close();
                rdr.Close();
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
        }

        private void MenuEdit_KeyVal(object sender, RoutedEventArgs e)
        {
            //when edit button is pressed load new window
            KeyValEditWindow editWindow = new KeyValEditWindow();
            //editWindow.Show();
            editWindow.ShowDialog();
                
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
            Clipboard.SetText(Common.DatabaseDirectory);
        }


    }
}
