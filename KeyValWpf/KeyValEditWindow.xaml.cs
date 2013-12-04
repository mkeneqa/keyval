using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SQLite;

namespace KeyValWpf
{
    /// <summary>
    /// Interaction logic for KeyValEditWindow.xaml
    /// </summary>
    public partial class KeyValEditWindow : Window
    {

        private int RowID = 0;

        public KeyValEditWindow()
        {
            InitializeComponent();
            DataBindEditWindow();
            
        }

        private void DataBindEditWindow()
        {
            using (SQLiteConnection Conn = new SQLiteConnection(Common.DatabaseConnString))
            {
                Conn.Open();
                string cmdString = "Select ID, kvValue, kvKey FROM MyKeys";
                SQLiteCommand cmd = new SQLiteCommand(cmdString, Conn);

                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable("MyKeys");
                sda.Fill(dt);                
                lstvw_KeyVal.ItemsSource = dt.DefaultView;

                Conn.Close();
                sda.Dispose();
            }

            
        }        

        //when a row is selected show values in the two textboxes
        private void lstvw_KeyVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            DataRowView keyValRow;            
            
            keyValRow = lstvw_KeyVal.SelectedItem as DataRowView;
            
            if (lstvw_KeyVal.SelectedItem != null)
            {
                txtKeyEdit.Text = keyValRow["kvKey"].ToString();
                txtValEdit.Text = keyValRow["kvValue"].ToString();
                RowID = Convert.ToInt32(keyValRow["ID"]);
                btnRemove.IsEnabled = true;
                btnUpdate.IsEnabled = true;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Values val = new Values(txtKeyEdit.Text.ToString(),txtValEdit.Text.ToString());
            
            //txtKeyEdit.Text.ToString();
            //txtValEdit.Text.ToString();
            insertValuesInToDatabase(val);
            DataBindEditWindow();

        }

        private void insertValuesInToDatabase(Values NewVal)
        {
            using (SQLiteConnection Conn = new SQLiteConnection(Common.DatabaseConnString))
            {
                Conn.Open();                

                SQLiteCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "INSERT INTO MyKeys(kvKey,kvValue) VALUES(@Key,@Value)";

                SQLiteParameter param = null;

                param = new SQLiteParameter("@Key", NewVal.FirstValue);
                cmd.Parameters.Add(param);
                param = new SQLiteParameter("@Value", NewVal.SecondValue);
                cmd.Parameters.Add(param);
                cmd.Prepare();
                cmd.ExecuteNonQuery();                
                Conn.Close();
                
            }

            txtKeyEdit.Text = "";
            txtValEdit.Text = "";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            using (SQLiteConnection Conn = new SQLiteConnection(Common.DatabaseConnString))
            {
                Conn.Open();

                SQLiteCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "UPDATE MyKeys SET kvKey=@Key,kvValue=@Value WHERE ID=@ID";

                SQLiteParameter param = null;

                param = new SQLiteParameter("@Key", txtKeyEdit.Text.ToString());
                cmd.Parameters.Add(param);
                param = new SQLiteParameter("@Value", txtValEdit.Text.ToString());
                cmd.Parameters.Add(param);
                param = new SQLiteParameter("@ID", RowID);
                cmd.Parameters.Add(param);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Conn.Close();

            }

            DataBindEditWindow();

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //delete from table where id = id in

            using (SQLiteConnection Conn = new SQLiteConnection(Common.DatabaseConnString))
            {
                Conn.Open();

                SQLiteCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "DELETE FROM MyKeys WHERE ID = @RowID";

                SQLiteParameter param = null;

                param = new SQLiteParameter("@RowID", RowID);
                cmd.Parameters.Add(param);
            
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Conn.Close();

            }

            txtKeyEdit.Text = "";
            txtValEdit.Text = "";

            DataBindEditWindow();

        }
    }
}
