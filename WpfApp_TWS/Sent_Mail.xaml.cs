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
using System.Runtime.InteropServices;
using System.Net.Mail;
using MySql.Data.MySqlClient;


namespace WpfApp_TWS
{
    /// <summary>
    /// Interaction logic for Sent_Mail.xaml
    /// </summary>
    public partial class Sent_Mail : Window
    {
        public String txt_Name;
        public String txt_Mail;
        public String userNow;
        public String userStatus;
        public String txt_UserName;
        public String txt_PassUser;
        public Sent_Mail()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Sent_Email_Click(object sender, RoutedEventArgs e)
        {
            checkStatusLogin();
        }
        private void sent_email()
        {
            using (new WaitCursor())
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(txt_Mail_Fill.Text);
                    mail.From = new MailAddress("tawansmartdata@gmail.com");
                    mail.Subject = "ข้อมูลสำหรับลงชื่อเข้าใช้โปรแกรม tawanSmartData ของคุณ" + txt_Name;
                    mail.Body = "<body> <b>ใช้ชื่อผู้ใช้ และรหัสผ่านนี้ เพื่อลงชื่อเข้าใช้</b><br> ชื่อผู้ใช้: &emsp;" + txt_UserName + "<br>" + "รหัสผ่าน: &emsp;" + txt_PassUser + "<body>";
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential("tawansmartdata@gmail.com", "Panda8133"); // ***use valid credentials***
                    smtp.Port = 587;
                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    //Cursor.Current = Cursors.Default;
                    Mouse.OverrideCursor = null;
                    //Application.UseWaitCursor = false;
                    MessageBox.Show("ชื่อผู้ใช้และรหัสผ่าน ถูกส่งเรียบร้อย กรุณาตรวจสอบอีเมลของคุณ และลงชื่อเข้าใช้อีกครั้ง", "สำเร็จ");
                }
                catch (Exception ex)
                {
                    //Cursor.Current = Cursors.Default;
                    Mouse.OverrideCursor = null;
                    //Application.UseWaitCursor = false;
                    MessageBox.Show("Exception in sendEmail:" + ex.Message);
                }
            }
        }

        private void checkStatusLogin()
        {
            try
            {
                String sql = "SELECT * FROM user where email = '" + txt_Mail_Fill.Text + "'";
                //MessageBox.Show(sql);
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;

                while (read.Read())
                {
                    //String id = (read["id"].ToString());
                    txt_Name = (read["name"].ToString());
                    txt_UserName = (read["user_login"].ToString());
                    txt_PassUser = (read["password_login"].ToString());
                    //String txt_Tel = (read["telephone"].ToString());
                    txt_Mail = (read["email"].ToString());
                    userStatus = (read["department"].ToString());
                    n = 1;
                    //MessageBox.Show(txt_Name);
                    //MessageBox.Show(txt_PassUser);
                }
                conn.Close();
                if(n==0)
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ใช้");
                }
                if(n>0)
                {
                    sent_email();
                    this.Close();
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

        private void msgError(String msg)
        {
            MessageBox.Show(msg);
            //l_Noti.Text = " " + msg;
            //l_Noti.Visibility = Visibility.Visible;
        }

        public class WaitCursor : IDisposable
        {
            private Cursor _previousCursor;

            public WaitCursor()
            {
                _previousCursor = Mouse.OverrideCursor;

                Mouse.OverrideCursor = Cursors.Wait;
            }

            #region IDisposable Members

            public void Dispose()
            {
                Mouse.OverrideCursor = _previousCursor;
            }

            #endregion
        }

        private void SentWindows_Loaded(object sender, RoutedEventArgs e)
        {
            txt_Mail_Fill.Focus();
        }

        private void txt_Mail_Fill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                checkStatusLogin();
            }
        }
    }
}
