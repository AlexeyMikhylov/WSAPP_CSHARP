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
        string toprows;
        string wherelike;
        string orderby;
        string gender = "where Gender.Name like '%%'";
        string birthday;
        int totalCount;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btn_editclient.Visibility = Visibility.Hidden;
            btn_deleteclient.Visibility = Visibility.Hidden;

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

            totalCount = grid_clients.Items.Count;
            lbl_currentrows.Content = grid_clients.Items.Count;
            lbl_totalrows.Content = totalCount;
        }

        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
            adapter.Update(clients);
        }

        //function
        private void TableFilterSql()
        {
            SqlConnection searchCon = new SqlConnection(App.MainConnection);
            searchCon.Open();
            string SearchQuery = "Select "+ toprows + " Client.id, Gender.Name as gender, FirstName, LastName, " +
                "Patronymic, Birthday, Phone, Email, RegistrationDate, max(ClientService.StartTime) as LastTime, " +
                "count(ClientService.ID) as NumberOfRequests, Tag.Title " +
                "From Client left JOIN Gender ON Client.GenderCode = Gender.Code " +
                "left join ClientService on Client.ID = ClientService.ClientID " +
                "left join TagOfClient on Client.ID = TagOfClient.ClientID " +
                "left join Tag on TagOfClient.TagID = Tag.ID " +
                gender + wherelike + birthday +
                "group by FirstName, Client.ID, Gender.Name, LastName, Patronymic, Birthday, Phone, Email, RegistrationDate, Tag.Title " +
                ""+ orderby + "";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SearchQuery, searchCon);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            DataSet a = new DataSet();
            dataAdapter.Fill(a);
            grid_clients.ItemsSource = a.Tables[0].DefaultView;

            lbl_currentrows.Content = grid_clients.Items.Count;
            lbl_totalrows.Content = totalCount;
        }        

        private void txtbx_search_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbx_searchselection.SelectedIndex == 1)
            {
                wherelike = "and FirstName like '%" + txtbx_search.Text + "%' ";
                TableFilterSql();
            }
            else if (cmbx_searchselection.SelectedIndex == 2)
            {
                wherelike = "and LastName like '%" + txtbx_search.Text + "%' ";
                TableFilterSql();
            }
            else if (cmbx_searchselection.SelectedIndex == 3)
            {
                wherelike = "and Patronymic like '%" + txtbx_search.Text + "%' ";
                TableFilterSql();
            }
            else if (cmbx_searchselection.SelectedIndex == 4)
            {
                wherelike = "and Phone like '%" + txtbx_search.Text + "%' ";
                TableFilterSql();
            }
            else if (cmbx_searchselection.SelectedIndex == 5)
            {
                wherelike = "and Email like '%" + txtbx_search.Text + "%' ";
                TableFilterSql();
            }
        }

        private void cmbx_rowsammount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_rowsammount.SelectedIndex == 1)
            {
                toprows = "top 10";
                TableFilterSql();
            }
            else if(cmbx_rowsammount.SelectedIndex == 2)
            {
                toprows = "top 50";
                TableFilterSql();
            }
            else if (cmbx_rowsammount.SelectedIndex == 3)
            {
                toprows = "top 200";
                TableFilterSql();
            }
        }    

        private void cmbx_genderfilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_genderfilter.SelectedIndex == 2)
            {
                gender = "Where Gender.Name like '%м%'";
                TableFilterSql();
            }
            else if (cmbx_genderfilter.SelectedIndex == 3)
            {
                gender = "Where Gender.Name like '%ж%'";
                TableFilterSql();
            }
            else if (cmbx_genderfilter.SelectedIndex == 1)
            {
                gender = "Where Gender.Name like '%%'";
                TableFilterSql();
            }
        }
        
        private void cmbx_order_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_order.SelectedIndex == 1)
            {
                orderby = "order by LastName";
                TableFilterSql();
            }
            else if (cmbx_order.SelectedIndex == 2)
            {
                orderby = "order by max(ClientService.StartTime)";
                TableFilterSql();
            }
            else if (cmbx_order.SelectedIndex == 3)
            {
                orderby = "order by count(ClientService.ID) desc";
                TableFilterSql();
            }
        }

        //strings
        string firstname;
        string clientid;
        string requests;

        private void grid_clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid pdr = (DataGrid)sender;
            DataRowView row_selected = pdr.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                firstname = row_selected["FirstName"].ToString();
                clientid = row_selected["id"].ToString();
                requests = row_selected["NumberOfRequests"].ToString();
                btn_editclient.Visibility = Visibility.Visible;
                btn_deleteclient.Visibility = Visibility.Visible;
            }
        }

        //edit
        private void btn_editclient_Click(object sender, RoutedEventArgs e)
        {
            string message = "Are you sure you want to edit client with name:\n" + firstname + " with id: "+clientid+"";
            string caption = "exit";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            string result;

            result = Convert.ToString(MessageBox.Show(message, caption, buttons,
            MessageBoxImage.Question));

            if (result == Convert.ToString(MessageBoxResult.Yes))
            {
                EditWindow EW = new EditWindow(clientid);
                EW.ShowDialog();
            }
        }

        //add
        private void btn_addclient_Click(object sender, RoutedEventArgs e)
        {
            AddWindow AW = new AddWindow();
            AW.ShowDialog();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        private void btn_showbirthday_Click(object sender, RoutedEventArgs e)
        {
            birthday = "and MONTH(Birthday) = MONTH(GETDATE()) ";
            TableFilterSql();
        }

        private void btn_deleteclient_Click(object sender, RoutedEventArgs e)
        {
            if (requests == "0")
            {

                if (grid_clients.SelectedItems != null)
                {
                    for (int i = 0; i < grid_clients.SelectedItems.Count; i++)
                    {
                        DataRowView datarowView = grid_clients.SelectedItems[i] as DataRowView;
                        if (datarowView != null)
                        {
                            DataRow dataRow = (DataRow)datarowView.Row;
                            dataRow.Delete();
                        }
                    }
                }
                UpdateDB();
            }
            else
            {
                MessageBox.Show("You can't delete this user, because hi has more than 0 requests");
            }
        }
    }
}
