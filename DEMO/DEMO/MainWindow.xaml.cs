using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace DEMO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            if (textBoxUserName.Text.Length == 0)
            {
                textBoxUserName.Focus();
            }
            //if (!Regex.IsMatch(textBoxUserName.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            //{

            //    textBoxUserName.Select(0, textBoxUserName.Text.Length);
            //    textBoxUserName.Focus();
            //    return;
            //}

            string userName = textBoxUserName.Text;
            string password = passwordBox.Password;

            string sql = "Select * from TB_USER where UserName='" + userName + "'  and PassWord='" + password + "'";

            using (var connection = new SqlConnection("Data Source=.;Initial Catalog=DEMO;Integrated Security=True"))
            {
                connection.Open();
                var result = connection.Query(sql);

                if (result.Count() > 0)
                {
                    Welcome welcome = new Welcome();
                    //this will open your child window
                    welcome.Show();
                    //this will close parent window. windowOne in this case
                    this.Close();
                }
                else
                {
                    errormessage.Content = "Sorry! Please enter existing email/password.";
                }
                connection.Close();
            }
        }
    }
}
