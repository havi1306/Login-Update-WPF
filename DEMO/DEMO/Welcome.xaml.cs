using Dapper;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DEMO
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        //  public List<User> ListUser { get; set; }
        private User user;
        private int page = 1;
        public Welcome()
        {
            InitializeComponent();
            var lists = Get_Page(page, 10);
            this.gridControl1.ItemsSource = lists;
        }
      
        private IEnumerable<User> Get_Page(int page = 1, int pagesize = 10)
        {
            IEnumerable<User> result;

            using (var db = new SqlConnection("Data Source=.;Initial Catalog=DEMO;Integrated Security=True"))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                var sql =
                    "SELECT * FROM TB_USER " +
                    "ORDER BY IdUser " +
                    "OFFSET @Offset ROWS " +
                    "FETCH NEXT @PageSize ROWS ONLY;";

                result = db.Query<User>(sql, new { Offset = (page - 1) * pagesize, PageSize = pagesize });
            }
            return result;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            page--;
            var lists = Get_Page(page, 10);
            this.gridControl1.ItemsSource = lists;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            page++;
            var lists = Get_Page(page, 10);
            this.gridControl1.ItemsSource = lists;
        }

        private int UpdateRecord(User user)
        {
            int updateError;
            using (var db = new SqlConnection("Data Source=.;Initial Catalog=DEMO;Integrated Security=True"))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                updateError = db.Execute("UPDATE TB_USER set FirstName = @FirstName ," +
                                                            " LastName = @LastName" +
                                                            " where IdUser = @IdUser", new
                                                            {
                                                                FirstName = user.FirstName,
                                                                LastName = user.LastName,
                                                                IdUser = user.IdUser
                                                            }, commandType: CommandType.Text);
            }
            return updateError;
        }

        private void TableView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if (e.Key != Key.Enter && e.Key != Key.Tab) return;
            //user = (User)gridControl1.GetFocusedRow();
        }

        private void TableView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //var currentUser = user;

            //var firstnameCellValue = gridControl1.GetFocusedRowCellValue("FirstName");
            //if (firstnameCellValue != null)
            //{
            //    var firstName = firstnameCellValue.ToString();
            //    if (firstName.Equals(user.FirstName))
            //    {
            //        currentUser.FirstName = firstName;
            //    }
            //}

            //var lastnameCellValue = gridControl1.GetFocusedRowCellValue("LastName");
            //if (firstnameCellValue != null)
            //{
            //    var lastName = lastnameCellValue.ToString();
            //    if (lastName.Equals(user.LastName))
            //    {
            //        currentUser.LastName = lastName;
            //    }
            //}
            //if (user == null) return;
            //if (!Utils.JSONEquals(currentUser, user)) return;
            //if (UpdateRecord(user) > 0)
            //{
            //    // upate susscess
            //}
        }

        private void TableView_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab) return;
            user = (User)gridControl1.GetFocusedRow();

            var currentUser = user;

            var firstnameCellValue = gridControl1.GetFocusedRowCellValue("FirstName");
            if (firstnameCellValue != null)
            {
                var firstName = firstnameCellValue.ToString();
                if (firstName.Equals(user.FirstName))
                {
                    currentUser.FirstName = firstName;
                }
            }

            var lastnameCellValue = gridControl1.GetFocusedRowCellValue("LastName");
            if (firstnameCellValue != null)
            {
                var lastName = lastnameCellValue.ToString();
                if (lastName.Equals(user.LastName))
                {
                    currentUser.LastName = lastName;
                }
            }
            if (user == null) return;
            if (!Utils.JSONEquals(currentUser, user)) return;
            if (UpdateRecord(user) > 0)
            {
                // upate susscess
            }
        }
    } 
}
