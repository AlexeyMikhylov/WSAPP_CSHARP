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
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace app_XX
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        int id;
        public AddWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(App.MainConnection);
            connection.Open();
            string sqlidstr = "Select top 1 id from Client order by id desc";
            SqlCommand com = new SqlCommand(sqlidstr, connection);
            SqlDataReader rd = com.ExecuteReader();
            while (rd.Read())//Чтение данных
            {
                id = rd.GetInt32(0);
            }
            txtbx_id.Text = Convert.ToString(id + 1);
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            //Маска для ФИО
            string fnameformat = txtbx_firstname.Text;
            string lnameformat = txtbx_lastname.Text;
            string pnameformat = txtbx_patronymic.Text;
            string fullnamepattern = @"^[A-Za-zА-Яа-я-\s@]*$";
            bool isFullnameValid()
            {
                if (Regex.IsMatch(fnameformat, fullnamepattern) && Regex.IsMatch(lnameformat, fullnamepattern) && Regex.IsMatch(pnameformat, fullnamepattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //Проверка на формат почты (@)
            string emailformat = txtbx_email.Text;
            string pattern = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            bool isEmailValid()
            {
                if (Regex.IsMatch(emailformat, pattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //Проверка на формат почты (@)
            string phoeformat = txtbx_phone.Text;
            string phonepattern = @"^[0-9-+()\s@]*$";
            bool isPhoneValid()
            {
                if (Regex.IsMatch(phoeformat, phonepattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (txtbx_lastname.Text == "" || txtbx_firstname.Text == "" || txtbx_patronymic.Text == "" || txtbx_email.Text == "" || txtbx_phone.Text == "")
            {
                MessageBox.Show("Fill all fields");
            }
            else if (datepicker_birthday.Text == "Select a date")
            {
                MessageBox.Show("Select a date");
            }
            else if (cmbbx_gender.SelectedIndex == 0)
            {
                MessageBox.Show("Select gender");
            }
            else if (isFullnameValid() == false)
            {
                MessageBox.Show("Incorrect first name, last name or patronymic format");
            }
            else if (isEmailValid() == false)
            {
                MessageBox.Show("Incorrect email format");
            }
            else if (isPhoneValid() == false)
            {
                MessageBox.Show("Incorrect phone format");
            }
            else if (lbl_Filename.Content.ToString() == "?")
            {
                MessageBox.Show("Select photo");
            }
            else
            {
                SqlConnection connection = new SqlConnection(App.MainConnection);
                connection.Open();
                string sqlInsertQuery = "Insert into Client([FirstName], [LastName], [Patronymic], [Birthday], [RegistrationDate], [Email], [Phone], [GenderCode], [PhotoPath]) " +
                    "Values('"+txtbx_firstname.Text+"', '"+txtbx_lastname.Text+"', '"+txtbx_patronymic.Text+"', '"+datepicker_birthday.SelectedDate+"', '"+DateTime.Now+"', '"+txtbx_email.Text+"', '"+txtbx_phone.Text+"', '"+cmbbx_gender.SelectedIndex+"', '"+lbl_Filename.Content.ToString()+"')";                

                //проверка на уникальность почты
                string SelSql2 = "Select email FROM Client WHERE email = ('{0}')";
                string sSelSotr2 = string.Format(SelSql2, txtbx_email.Text);
                SqlCommand cmd_Sel2 = new SqlCommand(sSelSotr2, connection);
                string result_email = Convert.ToString(cmd_Sel2.ExecuteScalar());
                bool checkemail()
                {
                    if (result_email == txtbx_email.Text)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                if (checkemail() == true)
                {
                    SqlCommand command = new SqlCommand(sqlInsertQuery, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Email is alredy using");
                }
            }
        }

        private void btn_photoOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                image_photo.Source = new BitmapImage(new Uri(op.FileName));
                lbl_Filename.Content = op.FileName;
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Close();
        }
    }
}
