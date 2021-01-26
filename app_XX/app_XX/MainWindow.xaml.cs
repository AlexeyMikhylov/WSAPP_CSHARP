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
using System.Data;
using System.Data.SqlClient;

namespace app_XX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlDataAdapter adapter;
        DataTable clients;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(App.MainConnection);
            connection.Open();
            string SqlQuery = "Select Client.id, Gender.Name as gender, FirstName, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, max(ClientService.StartTime) as LastTime, count(ClientService.ID) as NumberOfRequests, Tag.Title " +
                "From Client left JOIN Gender ON Client.GenderCode = Gender.Code left join ClientService on Client.ID = ClientService.ClientID left join TagOfClient on Client.ID = TagOfClient.ClientID left join Tag on TagOfClient.TagID = Tag.ID " +
                "group by  FirstName, Client.ID, Gender.Name, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, Tag.Title order by Client.ID";

            clients = new DataTable();
            SqlCommand com = new SqlCommand(SqlQuery, connection);
            adapter = new SqlDataAdapter(com);
            adapter.Fill(clients);
            grid_clients.ItemsSource = clients.DefaultView;            
        }

        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
            adapter.Update(clients);
        }

        private void SearchSql(string category)
        {
            SqlConnection searchCon = new SqlConnection(App.MainConnection);
            searchCon.Open();
            string SearchQuery = "Select Client.id, Gender.Name as gender, FirstName, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, max(ClientService.StartTime) as LastTime, count(ClientService.ID) as NumberOfRequests, Tag.Title " +
                "From Client left JOIN Gender ON Client.GenderCode = Gender.Code left join ClientService on Client.ID = ClientService.ClientID left join TagOfClient on Client.ID = TagOfClient.ClientID left join Tag on TagOfClient.TagID = Tag.ID " +
                "where "+ category + " like '%" + txtbx_search.Text + "%'" +
                "group by FirstName, Client.ID, Gender.Name, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, Tag.Title order by Client.ID";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SearchQuery, searchCon);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            DataSet a = new DataSet();
            dataAdapter.Fill(a);
            grid_clients.ItemsSource = a.Tables[0].DefaultView;
        }

        private void txtbx_search_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbx_searchselection.SelectedIndex == 1)
            {               
                SearchSql("FirstName");
            }
            else if (cmbx_searchselection.SelectedIndex == 2)
            {
                SearchSql("LastName");
            }
            else if (cmbx_searchselection.SelectedIndex == 3)
            {
                SearchSql("Patronymic");
            }
            else if (cmbx_searchselection.SelectedIndex == 4)
            {
                SearchSql("Phone");
            }
            else if (cmbx_searchselection.SelectedIndex == 5)
            {
                SearchSql("Email");
            }
        }

        private void Rowselection (string rowsammount)
        {
            SqlConnection searchCon = new SqlConnection(App.MainConnection);
            searchCon.Open();
            string SearchQuery = "Select top "+rowsammount+" Client.id, Gender.Name as gender, FirstName, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, max(ClientService.StartTime) as LastTime, count(ClientService.ID) as NumberOfRequests, Tag.Title " +
                "From Client left JOIN Gender ON Client.GenderCode = Gender.Code left join ClientService on Client.ID = ClientService.ClientID left join TagOfClient on Client.ID = TagOfClient.ClientID left join Tag on TagOfClient.TagID = Tag.ID " +
                "group by  FirstName, Client.ID, Gender.Name, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, Tag.Title order by Client.ID";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SearchQuery, searchCon);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            DataSet a = new DataSet();
            dataAdapter.Fill(a);
            grid_clients.ItemsSource = a.Tables[0].DefaultView;
        }

        private void cmbx_rowsammount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_rowsammount.SelectedIndex == 1)
            {
                Rowselection("10");
            }
            else if(cmbx_rowsammount.SelectedIndex == 2)
            {
                Rowselection("50");
            }
            else if (cmbx_rowsammount.SelectedIndex == 3)
            {
                Rowselection("200");
            }
        }

        private void Genderfilter(string gender)
        {
            SqlConnection searchCon = new SqlConnection(App.MainConnection);
            searchCon.Open();
            string SearchQuery = "Select Client.id, Gender.Name as gender, FirstName, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, max(ClientService.StartTime) as LastTime, count(ClientService.ID) as NumberOfRequests, Tag.Title " +
                "From Client left JOIN Gender ON Client.GenderCode = Gender.Code left join ClientService on Client.ID = ClientService.ClientID left join TagOfClient on Client.ID = TagOfClient.ClientID left join Tag on TagOfClient.TagID = Tag.ID " +
                "where Gender.Name like '"+gender+"'" +
                "group by  FirstName, Client.ID, Gender.Name, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, Tag.Title order by Client.ID";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SearchQuery, searchCon);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            DataSet a = new DataSet();
            dataAdapter.Fill(a);
            grid_clients.ItemsSource = a.Tables[0].DefaultView;
        }

        private void cmbx_genderfilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_genderfilter.SelectedIndex == 2)
            {
                Genderfilter("м");
            }
            else if (cmbx_genderfilter.SelectedIndex == 3)
            {
                Genderfilter("ж");
            }
            else if (cmbx_genderfilter.SelectedIndex == 1)
            {
                Window_Loaded(sender, e);
            }
        }

        private void Orderfilter(string field)
        {
            SqlConnection searchCon = new SqlConnection(App.MainConnection);
            searchCon.Open();
            string SearchQuery = "Select Client.id, Gender.Name as gender, FirstName, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, max(ClientService.StartTime) as LastTime, count(ClientService.ID) as NumberOfRequests, Tag.Title " +
                "From Client left JOIN Gender ON Client.GenderCode = Gender.Code left join ClientService on Client.ID = ClientService.ClientID left join TagOfClient on Client.ID = TagOfClient.ClientID left join Tag on TagOfClient.TagID = Tag.ID " +
                "group by  FirstName, Client.ID, Gender.Name, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, Tag.Title order by "+field+"";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SearchQuery, searchCon);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            DataSet a = new DataSet();
            dataAdapter.Fill(a);
            grid_clients.ItemsSource = a.Tables[0].DefaultView;
        }

        private void cmbx_order_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_order.SelectedIndex == 1)
            {
                Orderfilter("LastName");
            }
            else if (cmbx_order.SelectedIndex == 2)
            {
                Orderfilter("max(ClientService.StartTime)");
            }
            else if (cmbx_order.SelectedIndex == 3)
            {
                Orderfilter("count(ClientService.ID) desc");
            }
        }
    }
}
