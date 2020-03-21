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


namespace WpfApp_TWS
{
    /// <summary>
    /// Interaction logic for UserControl_Setting_Company.xaml
    /// </summary>
    public partial class UserControl_Setting_Company : UserControl
    {
        public String head;
        public bool editState = false;
        public UserControl_Setting_Company()
        {
            InitializeComponent();
            Radio_Main.IsChecked = true;
            load_Branch();
        }
        private void load_Branch()
        {
            try
            {
                String sql = "SELECT * FROM systems where id =" + ("1");
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    txt_Company.Text = (read["company"].ToString());
                    txt_Address.Text = (read["address"].ToString());
                    txt_Tel.Text = (read["tel"].ToString());
                    txt_Mail.Text = (read["email"].ToString());
                    txt_BranchName.Text = (read["branchName"].ToString());
                    txt_BranchID.Text = (read["branch_ID"].ToString());

                    head = (read["head"].ToString());

                    if (head == "yes")
                    {
                        Radio_Main.IsChecked = true;
                        txt_BranchName.Text = "";
                    }
                    if (head == "no")
                    {
                        radio_Branch.IsChecked = true;
                    }
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
        }
        
        private void msgError(String msg)
        {
            MessageBox.Show(msg, "พบปัญหา");
        }
        private void Radio_Main_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void radio_Branch_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Save_Click_1(object sender, RoutedEventArgs e)
        {
            if (Radio_Main.IsChecked == true)
            {
                head = "yes";
                txt_BranchName.Text = "";
            }
            if (radio_Branch.IsChecked == true)
            {
                head = "no";
            }
            try
            {
                String sql = "UPDATE systems Set company='" + txt_Company.Text + "', address='" + txt_Address.Text + "', tel='" + txt_Tel.Text + "', email='" + txt_Mail.Text + "', head='" + head + "', branchName='" + txt_BranchName.Text + "', branch_ID='" + txt_BranchID.Text + "' WHERE id = 1";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                load_Branch();
                MessageBox.Show("แก้ไขข้อมูลเรียบร้อย", "สำเร็จ");
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            load_Branch();
        }

        private void txt_Company_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txt_Address.Focus();
                txt_Address.SelectAll();
            }
        }

        private void txt_Address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txt_Tel.Focus();
                txt_Tel.SelectAll();
            }
        }

        private void txt_Tel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txt_Mail.Focus();
                txt_Mail.SelectAll();
            }
        }

        private void txt_Mail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Radio_Main.Focus();
            }
        }

        private void txt_BranchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txt_BranchID.Focus();
                txt_BranchID.SelectAll();
            }
        }

        private void txt_BranchID_KeyDown(object sender, KeyEventArgs e)
        {
            btn_Save.Focus();
        }

        private void Radio_Main_KeyDown(object sender, KeyEventArgs e)
        {
            radio_Branch.Focus();
        }

        private void radio_Branch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txt_BranchName.Focus();
                txt_BranchName.SelectAll();
            }
        }
    }
}
