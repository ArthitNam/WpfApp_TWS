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
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Net.Mail;



namespace WpfApp_TWS
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public String txt_Name;
        public String txt_Mail;
        public String userNow;
        public String userStatus;
        public String txt_UserName;
        public String txt_PassUser;
        public String remember_checked;

        public Login()
        {
            InitializeComponent();
        }
        private void Login_Form_Loaded(object sender, RoutedEventArgs e)
        {          
            remember();
            txt_User.Focus();
        }

        private void remember()
        {
            try
            {
                String sql1 = "SELECT * FROM remember";
                MySqlConnection conn1 = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn1);
                conn1.Open();
                MySqlDataReader read1 = cmd1.ExecuteReader();

                while (read1.Read())
                {
                    remember_checked = (read1["remember_checked"].ToString());
                    txt_UserName = (read1["username"].ToString());
                    txt_PassUser = (read1["password"].ToString());
                }
                conn1.Close();
                if(remember_checked == "yes")
                {
                    check_Remember.IsChecked = true;
                    txt_User.Text = txt_UserName;
                    txt_Password.Password = txt_PassUser;
                    Login_Click();
                }
                if (remember_checked == "no")
                {
                    check_Remember.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unable to connect to any of the specified MySQL hosts.")
                {
                    msgError("ไม่สามารถเชื่อมต่อฐานข้อมูลได้ กรุณาตรวจสอบการเชื่อมต่ออินเทอร์เน็ต");
                }
                else
                {
                    msgError(ex.Message);
                }
            }
        }

        private void Login_Click()
        {
            String sql = "SELECT * FROM user WHERE user_login = '" + txt_User.Text + "'AND password_login='" + txt_Password.Password + "'";
            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userNow = txt_User.Text;
                    checkStatusLogin();
                    if(check_Remember.IsChecked==true)
                    {
                        try
                        {
                            String sql1 = "UPDATE remember Set remember_checked = 'yes', username ='" + txt_User.Text + "', password ='" + txt_Password.Password + "'";
                            //MessageBox.Show(sql1);
                            MySqlConnection conn1 = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                            MySqlCommand cmd1 = new MySqlCommand(sql1, conn1);
                            conn1.Open();
                            cmd1.ExecuteNonQuery();
                            conn1.Close();
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "Unable to connect to any of the specified MySQL hosts.")
                            {
                                msgError("ไม่สามารถเชื่อมต่อฐานข้อมูลได้ กรุณาตรวจสอบการเชื่อมต่ออินเทอร์เน็ต");
                            }
                            else
                            {
                                msgError(ex.Message);
                            }
                        }
                    }
                    if (check_Remember.IsChecked == false)
                    {
                        try
                        {
                            String sql1 = "UPDATE remember Set remember_checked = 'no', username ='" + txt_User.Text + "', password ='" + txt_Password.Password + "'";
                            //MessageBox.Show(sql1);
                            MySqlConnection conn1 = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                            MySqlCommand cmd1 = new MySqlCommand(sql1, conn1);
                            conn1.Open();
                            cmd1.ExecuteNonQuery();
                            conn1.Close();
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "Unable to connect to any of the specified MySQL hosts.")
                            {
                                msgError("ไม่สามารถเชื่อมต่อฐานข้อมูลได้ กรุณาตรวจสอบการเชื่อมต่ออินเทอร์เน็ต");
                            }
                            else
                            {
                                msgError(ex.Message);
                            }
                        }
                    }
                    Contaimer f2 = new Contaimer(userNow, userStatus);
                    this.Hide();
                    f2.Show();
                }
                else
                {
                    userNow = txt_User.Text;
                    checkStatusLogin();
                    msgError(" ชื่อผู้ใช้ หรือรหัสผ่าน ไม่ถูกต้อง !");
                    //label_resetPass.Visible = true;
                    //txtPass.Clear();
                    //txtUser.SelectAll();
                    //txtUser.Focus();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unable to connect to any of the specified MySQL hosts.")
                {
                    msgError("ไม่สามารถเชื่อมต่อฐานข้อมูลได้ กรุณาตรวจสอบการเชื่อมต่ออินเทอร์เน็ต");
                }
                else
                {
                    msgError(ex.Message);
                }
            }

        }

        private void checkStatusLogin()
        {
            try
            {
                String sql = "SELECT * FROM user where user_login = '" + userNow + "'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    //String id = (read["id"].ToString());
                    txt_Name = (read["name"].ToString());
                    txt_UserName = (read["user_login"].ToString());
                    txt_PassUser = (read["password_login"].ToString());
                    //String txt_Tel = (read["telephone"].ToString());
                    txt_Mail = (read["email"].ToString());
                    userStatus = (read["department"].ToString());
                    //MessageBox.Show(txt_Name);
                    //MessageBox.Show(txt_Mail);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unable to connect to any of the specified MySQL hosts.")
                {
                    msgError("ไม่สามารถเชื่อมต่อฐานข้อมูลได้ กรุณาตรวจสอบการเชื่อมต่ออินเทอร์เน็ต");
                }
                else
                {
                    msgError(ex.Message);
                }
            }
        }

        private void msgError(String msg)
        {
            l_Noti.Text = " " + msg;
            l_Noti.Visibility = Visibility.Visible;
            txt_Password.Focus();
            txt_Password.SelectAll();
        }
       
        private void Login_Main_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void txt_User_KeyDown(object sender, KeyEventArgs e)
        {
            l_Noti.Visibility = Visibility.Hidden;
            if (e.Key == Key.Return)
            {
                txt_Password.Focus();
                txt_Password.SelectAll();
            }
        }

        private void txt_Password_KeyDown(object sender, KeyEventArgs e)
        {
            l_Noti.Visibility = Visibility.Hidden;

            if (e.Key == Key.Return)
            {
                Login_Click();
            }
        }

        private void btn_Login_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            Login_Click();
        }

        private void btn_Forget_Pass_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            var pk = new Sent_Mail();
            pk.ShowDialog();
            this.Opacity = 100;
        }

        private void check_Remember_Checked(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
