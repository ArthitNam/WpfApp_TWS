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
using MySql.Data.MySqlClient;
using System.Data;


namespace WpfApp_TWS
{
    /// <summary>
    /// Interaction logic for UserControl_User.xaml
    /// </summary>
    public partial class UserControl_User : UserControl
    {
        public bool networkFail = false;
        public bool fineStatus = false;
        public UserControl_User()
        {
            InitializeComponent();
        }

        private void UserControl_User1_Loaded(object sender, RoutedEventArgs e)
        {
            showData();
        }
        private void showData()
        {
            try
            {
                String sql1 = "SELECT * FROM user";
                MySqlConnection conn1 = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn1);
                conn1.Open();
                MySqlDataReader read1 = cmd1.ExecuteReader();
                int n = 0;
                dataG_User.Items.Clear();

                DataColumn id = new DataColumn("รหัส", typeof(String));
                DataColumn name = new DataColumn("ชื่อ - นามสกุล", typeof(String));
                DataColumn department = new DataColumn("สถานะ", typeof(String));
                DataColumn user_login = new DataColumn("ชื่อผู้ใช้", typeof(String));
                DataColumn password_login = new DataColumn("รหัสผ่าน", typeof(String));
                DataColumn telephone = new DataColumn("โทรศัพท์", typeof(String));
                DataColumn email = new DataColumn("อีเมล", typeof(String));

                User user = new User();
                
                while (read1.Read())
                {                   
                    String userid = (read1["id"].ToString());
                    Int32 useridInt = Convert.ToInt32(userid);  //  แปลงเป็น int 

                    var data = new User
                    {
                        Id = String.Format("{0:000}", useridInt),
                        Name = read1["name"].ToString(),
                        Department = read1["department"].ToString(),
                        User_login = read1["user_login"].ToString(),
                        Password_login = read1["password_login"].ToString(),
                        Telephone = read1["telephone"].ToString(),
                        Email = read1["email"].ToString()
                    };
                    dataG_User.Items.Add(data);
                    n++;
                }
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
                networkFail = true;
            }
        }
        public class User
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public String Department { get; set; }
            public String User_login { get; set; }
            public String Password_login { get; set; }
            public String Telephone { get; set; }
            public String Email { get; set; }           
        }
        private void msgError(String msg)
        {
            MessageBox.Show(msg, "พบปัญหา");
        }

        private void save()
        {
            bool error = false;
            String user_status = "user";

            bool emailValid = IsValidEmail(txt_Mail.Text);
            //MessageBox.Show(emailValid.ToString());
            if(emailValid == false && checkBox1.IsChecked == true)
            {
                error = true;
            }

            if (checkBox1.IsChecked == true)
            {
                user_status = "Administrator";
            }
            if (checkBox1.IsChecked == false)
            {
                user_status = "User";
            }
            if (checkBox1.IsChecked == false && (txt_Name.Text == "" || txt_PassUser.Text == "" || txt_UserName.Text == ""))
            {
                MessageBox.Show("โปรดกรอกรายละเอียดให้ครบถ้วน", "ไม่สำเร็จ");
                error = true;
            }
            if (checkBox1.IsChecked == true && (txt_Name.Text == "" || txt_PassUser.Text == "" || txt_UserName.Text == "" || txt_Mail.Text == ""))
            {
                MessageBox.Show("โปรดกรอกรายละเอียดให้ครบถ้วน", "ไม่สำเร็จ");
                error = true;
            }
            else
            {
                if (fineStatus == false && error == false)
                {
                    try
                    {
                        String sql = "INSERT INTO user (id,name,department,user_login,password_login,telephone,email) VALUES('" + txt_Id.Text + "' , '" + txt_Name.Text + "' , '" + user_status + "' , '" + txt_UserName.Text + "' , '" + txt_PassUser.Text + "' , '" + txt_Tel.Text + "' , '" + txt_Mail.Text + "')";
                        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        resetForm();
                        showData();
                        MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "สำเร็จ");
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
                        networkFail = true;
                    }
                }
                if (fineStatus == true && error == false)
                {
                    try
                    {
                        String sql = "UPDATE user Set name='" + txt_Name.Text + "', department='" + user_status + "', user_login='" + txt_UserName.Text + "', password_login='" + txt_PassUser.Text + "', telephone='" + txt_Tel.Text + "', email='" + this.txt_Mail.Text + "' WHERE id = " + txt_Id.Text;
                        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        resetForm();
                        showData();
                        MessageBox.Show("แก้ไขข้อมูลเรียบร้อย", "สำเร็จ");
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
                        networkFail = true;
                    }
                }
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            save();
        }
        private void resetForm()
        {
            fineStatus = false;
            btnUpdate();
            txt_Name.Clear();
            txt_UserName.Clear();
            txt_PassUser.Clear();
            txt_Tel.Clear();
            txt_Mail.Clear();
            txt_Fine.Clear();
            txt_Id.Clear();
            checkBox1.IsChecked = false;           
        }
        private void btnUpdate()
        {
            if (fineStatus == true)
            {
                btn_Save.Content = "อัพเดท";
            }
            else
            {
                btn_Save.Content = "บันทึก";
            }
        }
        private void fine()
        {
            if (dataG_User.SelectedCells.Count > 0)
            {
                var cellInfo = dataG_User.SelectedCells[0];
                var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
                txt_Fine.Text = content;
                //txt_Fine.Text = ((DataRowView)dataG_User.SelectedCells).Row[1].ToString();
                //MessageBox.Show(((DataRowView)dataG_User.CurrentItem).Row[1].ToString());
            }
            if (txt_Fine.Text != "")
            {
                txt_Name.Clear();
                txt_UserName.Clear();
                txt_PassUser.Clear();
                txt_Tel.Clear();
                txt_Mail.Clear();
                txt_Id.Clear();
                checkBox1.IsChecked = false;

                int a;
                if (int.TryParse(txt_Fine.Text, out a))
                {
                    try
                    {
                        //MessageBox.Show("ตัวเลข");
                        String txtSelect = txt_Fine.Text;
                        String sql = "SELECT * FROM user where id =" + (txtSelect);
                        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();
                        MySqlDataReader read = cmd.ExecuteReader();

                        while (read.Read())
                        {
                            txt_Id.Text = (read["id"].ToString());     //  อ่านจาก SQL
                            int idxxx = Convert.ToInt32(txt_Id.Text);  //  แปลงเป็น int 
                            txt_Id.Text = idxxx.ToString("D" + 3); //("D" + length)  แปลงกลับเป็นสามตำแหน่ง

                            txt_Name.Text = (read["name"].ToString());
                            txt_UserName.Text = (read["user_login"].ToString());
                            txt_PassUser.Text = (read["password_login"].ToString());
                            txt_Tel.Text = (read["telephone"].ToString());
                            txt_Mail.Text = (read["email"].ToString());
                            String user_status = (read["department"].ToString());
                            if (user_status == "User")
                            {
                                checkBox1.IsChecked = false;
                            }
                            if (user_status == "Administrator")
                            {
                                checkBox1.IsChecked = true;
                            }
                            fineStatus = true;
                            btnUpdate();
                            txt_Fine.Focus();
                            txt_Fine.SelectAll();
                        }
                        conn.Close();
                        if (txt_Id.Text == "")
                        {
                            MessageBox.Show("ไม่พบข้อมูลผู้ใช้");
                            resetForm();
                            txt_Fine.Focus();
                            txt_Fine.SelectAll();
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
                        networkFail = true;
                    }
                }
                else
                {
                    try
                    {
                        //MessageBox.Show("ตัวอักษร");
                        String txtSelect = txt_Fine.Text;
                        String sql = "SELECT * FROM user where name like '%" + txt_Fine.Text + "%'";
                        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();
                        MySqlDataReader read = cmd.ExecuteReader();

                        while (read.Read())
                        {
                            txt_Id.Text = (read["id"].ToString());     //  อ่านจาก SQL
                            int idxxx = Convert.ToInt32(txt_Id.Text);  //  แปลงเป็น int 
                            txt_Id.Text = idxxx.ToString("D" + 3);     //("D" + length)  แปลงกลับเป็นสามตำแหน่ง

                            txt_Name.Text = (read["name"].ToString());
                            txt_UserName.Text = (read["user_login"].ToString());
                            txt_PassUser.Text = (read["password_login"].ToString());
                            txt_Tel.Text = (read["telephone"].ToString());
                            txt_Mail.Text = (read["email"].ToString());
                            String user_status = (read["department"].ToString());
                            if (user_status == "User")
                            {
                                checkBox1.IsChecked = false;
                            }
                            if (user_status == "Administrator")
                            {
                                checkBox1.IsChecked = true;
                            }
                            fineStatus = true;
                            btnUpdate();
                            txt_Fine.Focus();
                            txt_Fine.SelectAll();
                        }
                        conn.Close();

                        if (txt_Id.Text == "")
                        {
                            MessageBox.Show("ไม่พบข้อมูลผู้ใช้");
                            resetForm();
                            txt_Fine.Focus();
                            txt_Fine.SelectAll();
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
                        networkFail = true;
                    }
                }
            }
        }
       
        private void btn_Fine_Click(object sender, RoutedEventArgs e)
        {
            fine();
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            resetForm();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            fineStatus = false;
            btnUpdate();
            if (txt_Name.Text == "")
            {
                MessageBox.Show("ข้อมูลผู้ใช้ไม่ถูกต้อง", "ไม่สำเร็จ");
            }
            else
            {
                if (MessageBox.Show("คุณต้องการลบผู้ใช้ " + txt_Name.Text + " ใช่หรือไม่?", "คำยืนยัน", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    try
                    {
                        String txtSelect = txt_Fine.Text;
                        String sql = "DELETE FROM user WHERE id =" + (txtSelect);
                        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        resetForm();
                        showData();
                        MessageBox.Show("ลบข้อมูลเรียบร้อย", "สำเร็จ");
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
                        networkFail = true;
                    }
                }
            }
        }

        private void txt_Fine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                fine();
            }
        }

        private void txt_Id_KeyDown(object sender, KeyEventArgs e)
        {
            
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Return)
            {
                if (e.Key == Key.Return && txt_Id.Text != "")
                {
                    e.Handled = true;
                    txt_Name.Focus();
                    txt_Name.SelectAll();
                }
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("รหัสพนักงาน ต้องเป็นตัวเลข 000 - 999 เท่านั้น");
                txt_Id.Focus();
                txt_Id.Text = "";
            }    
        }

        private void txt_Id_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_Id.Text != "")
            {
                try
                {
                    Int32 txt_ID_int = Convert.ToInt32(txt_Id.Text);
                    if (txt_ID_int < 0 || txt_ID_int > 999)
                    {
                        MessageBox.Show("รหัสพนักงาน ต้องเป็นตัวเลข 000 - 999 เท่านั้น");
                        txt_Id.Focus();
                        txt_Id.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    msgError(ex.Message);
                }
            }
        }

        private void txt_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                checkBox1.Focus();
            }
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                txt_UserName.Focus();
                txt_UserName.SelectAll();
            }
        }

        private void txt_UserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                txt_PassUser.Focus();
                txt_PassUser.SelectAll();
            }
        }

        private void txt_PassUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                txt_Tel.Focus();
                txt_Tel.SelectAll();
            }
        }

        private void txt_Tel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                txt_Mail.Focus();
                txt_Mail.SelectAll();
            }
        }

        private void txt_Mail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                btn_Save.Focus();
            }
           
        }

        private void btn_Save_KeyDown(object sender, KeyEventArgs e)
        {
            save();
        }

        private void dataG_User_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataG_User.SelectedCells.Count > 0)
            {
                var cellInfo = dataG_User.SelectedCells[0];
                var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
                txt_Fine.Text = content;
                fineStatus = false;
            }
        }
        bool IsValidEmail(string email)
        {
            string emailTrimed = email.Trim();

            if (!string.IsNullOrEmpty(emailTrimed))
            {
                bool hasWhitespace = emailTrimed.Contains(" ");

                int indexOfAtSign = emailTrimed.LastIndexOf('@');

                if (indexOfAtSign > 0 && !hasWhitespace)
                {
                    string afterAtSign = emailTrimed.Substring(indexOfAtSign + 1);

                    int indexOfDotAfterAtSign = afterAtSign.LastIndexOf('.');

                    if (indexOfDotAfterAtSign > 0 && afterAtSign.Substring(indexOfDotAfterAtSign).Length > 1)
                        return true;
                }
            }
            return false;
        }
    }
}
