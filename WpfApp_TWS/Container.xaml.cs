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


namespace WpfApp_TWS
{
    /// <summary>
    /// Interaction logic for Contaimer.xaml
    /// </summary>
    public partial class Contaimer : Window
    {
        public bool networkFail = false;
        public string loginUser;
        public string loginStatus;
        public bool admin = false;
        public bool first = false;
        public bool aboutForm = false;


        public Contaimer(String userNow, String userStatus)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.WorkArea.Height;
            loginUser = userNow;
            loginStatus = userStatus;
            if (loginStatus == "Administrator")
            {
                admin = true;
            }
            if (loginStatus == "User")
            {
                admin = false;
            }
        }
        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            btn_Normal.Visibility = Visibility.Visible;
            btn_Maximize.Visibility = Visibility.Hidden;

            Panal_Container.Children.Add(new UserControl_Dashbord());
            load_Branch();
            l_user.Text = loginUser;
            l_status.Text = loginStatus;
        }
        private void msgError(String msg)
        {
            MessageBox.Show(msg, "พบปัญหา");
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            btn_Normal.Visibility = Visibility.Visible;
            btn_Maximize.Visibility = Visibility.Hidden;
        }

        private void btn_Normal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            btn_Normal.Visibility = Visibility.Hidden;
            btn_Maximize.Visibility = Visibility.Visible;
        }
        private void btn_Minimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        
        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (MenuVertical.Width == 250)
            {
                MenuVertical.Width = 67;
                Panal_Container.Margin = Margin = new Thickness(67, 60, 0, 0);
                btn_Menu.Margin = Margin = new Thickness(67, 4, 0, 0);

            }
            else
            {
                MenuVertical.Width = 250;
                Panal_Container.Margin = Margin = new Thickness(250, 60, 0, 0);
                btn_Menu.Margin = Margin = new Thickness(250, 4, 0, 0);
            }
        }
        
        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            Panal_Container.Children.Clear();
            Panal_Container.Children.Add(new UserControl_Home());
        }

        private void ListViweMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);

            switch(index)
            {
                case 0:
                    Panal_Container.Children.Clear();
                    Panal_Container.Children.Add(new UserControl_Dashbord());
                    break;

                case 1:
                    MenuVertical.Width = 67;
                    Panal_Container.Margin = Margin = new Thickness(67, 60, 0, 0);
                    btn_Menu.Margin = Margin = new Thickness(67, 4, 0, 0);

                    Panal_Container.Children.Clear();
                    Panal_Container.Children.Add(new UserControl_Home());
                    break;

                case 2:

                    break;

                case 3:

                    break;
                case 4:
                    if (admin == true)
                    {
                        Panal_Container.Children.Clear();
                        Panal_Container.Children.Add(new UserControl_User());
                    }
                    if (admin == false)
                    {
                        MessageBox.Show("กรูณาลงชื่อเข้าใช้ ในฐานะ ผู้ดูแลระบบ", "สิทธิการเข้าถึง !");
                    }
                    
                    break;
                case 5:
                    if (admin == true)
                    {
                        Panal_Container.Children.Clear();
                        Panal_Container.Children.Add(new UserControl_Setting());
                    }
                    if (admin == false)
                    {
                        MessageBox.Show("กรูณาลงชื่อเข้าใช้ ในฐานะ ผู้ดูแลระบบ", "สิทธิการเข้าถึง !");
                    }
                    
                    break;
                case 6:
                    if (MessageBox.Show("ลงชื่อออกจาก " + loginUser + " ใช่หรือไม่?", "คำยืนยัน", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            String sql1 = "UPDATE remember Set remember_checked = 'no'";
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

                        this.Close();
                        Login f2 = new Login();
                        f2.Show();
                    }

                    break;
                default:
                    break;

            }
        }

        private void MoveCursorMenu(int index)
        {
            TransitioningContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0,(100+(50*index)),0,0);
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
                    String company, branch, branch_id;

                    company = (read["company"].ToString());
                    if (company == "")
                    {
                        first = true;
                    }
                    branch = (read["branchName"].ToString());
                    if (branch == "")
                    {
                        branch = "สำนักงานใหญ่";
                    }
                    branch_id = "รหัสสาขา : " + (read["branch_ID"].ToString());
                    l_Company.Text = company + " (" + branch + ") " + branch_id;
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
                networkFail = true;
            }
        }

    }
}
