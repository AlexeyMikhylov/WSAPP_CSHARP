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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace app_XX
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        SqlDataAdapter adapter;
        DataTable client;
        public EditWindow(string clientid)
        {
            InitializeComponent();
            lbl_clientid.Content = clientid;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_clientid.Visibility = Visibility.Hidden;

            SqlConnection connection = new SqlConnection(App.MainConnection);
            connection.Open();
            string SqlQuery = "Select * From Client Where id = "+lbl_clientid.Content+"";
            client = new DataTable();
            SqlCommand com = new SqlCommand(SqlQuery, connection);
            adapter = new SqlDataAdapter(com);
            adapter.Fill(client);
            grid_client.ItemsSource = client.DefaultView;
        }
        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
            adapter.Update(client);
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            UpdateDB();
            MessageBox.Show("Success");
        }
    }
}
