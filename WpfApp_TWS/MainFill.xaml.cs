using System;
using System.Collections.Generic;
using System.Globalization;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Controls.Primitives;

namespace WpfApp_TWS
{
    public partial class MainFill : Window
    {
        public bool networkFail = false;
        public int[] user_ID;
        public String[] user_Name;
        public String sql;
        public String sql_Qr;
        public String w_machine_id = "!= -99";
        public String w_standardSize = "!= -99";
        public String w_status = "!= -99";
        public String w_user_id = "!= -99";
        public Decimal sum_kg;
        public Decimal sum_lite;
        public Decimal sum_drif;
        public bool combobox_select_first = false;
        public bool comboBox_machineID_Changed = false;
        public bool comboBox_standardSize_Changed = false;
        public bool comboBox_User_Changed = false;
        public String name;
        public String id;
        public DateTime startDate;
        public DateTime stopDate;
        public String start;
        public String stop;
        public String start_stop;
        public bool select = false;

        public MainFill()
        {
            InitializeComponent();
            
            comboBox_Select.Items.Add("รายวัน");
            comboBox_Select.Items.Add("รายสัปดาห์");
            comboBox_Select.Items.Add("รายเดือน");
            comboBox_Select.Items.Add("รายปี");
            comboBox_Select.Items.Add("ย้อนหลัง 1 วัน");
            comboBox_Select.Items.Add("ย้อนหลัง 3 วัน");
            comboBox_Select.Items.Add("ย้อนหลัง 7 วัน");
            comboBox_Select.Items.Add("ย้อนหลัง 15 วัน");
            comboBox_Select.Items.Add("ย้อนหลัง 90 วัน");
            comboBox_Select.Items.Add("ย้อนหลัง 1 เดือน");
            comboBox_Select.Items.Add("ย้อนหลัง 3 เดือน");
            comboBox_Select.Items.Add("ย้อนหลัง 6 เดือน");
            comboBox_Select.Items.Add("ย้อนหลัง 1 ปี");
            comboBox_Select.Items.Add("ย้อนหลัง 3 ปี");
            comboBox_Select.Items.Add("ย้อนหลัง 6 ปี");
            comboBox_Select.Items.Add("ทั้งหมด");
            comboBox_Select.Items.Add("กำหนดเอง");
        }
        
        private void mainFill_Data_Loaded(object sender, RoutedEventArgs e)
        {
            this.comboBox_Select.SelectedIndex = 0;
        }
        private void comboBox_Select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(select == true)
            {
                comboBox_Select.Items.Remove(start_stop);
                select = false;
            }

            if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("รายวัน"))
            {
                sql_Qr = "where datetime like '%" + System.DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "%' AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("รายสัปดาห์"))
            {
                sql_Qr = "where YEARWEEK (date(datetime),3) = YEARWEEK (CURRENT_DATE(),3) AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("รายเดือน"))
            {
                sql_Qr = "where datetime like '%" + System.DateTime.Now.Year.ToString("D" + 4) + "-" + (System.DateTime.Now.Month.ToString("D" + 2)) + "%' AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("รายปี"))
            {
                sql_Qr = "where datetime like '%" + (System.DateTime.Now.Year.ToString("D" + 4)) + "%' AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 1 วัน"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -1 DAY) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 3 วัน"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -3 DAY) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 7 วัน"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -7 DAY) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 15 วัน"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -15 DAY) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 90 วัน"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -90 DAY) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 1 เดือน"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -1 MONTH) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 3 เดือน"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -3 MONTH) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 6 เดือน"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -6 MONTH) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 1 ปี"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -1 YEAR) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 3 ปี"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -3 YEAR) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 6 ปี"))
            {
                sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -6 YEAR) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ทั้งหมด"))
            {
                sql_Qr = "WHERE YEAR(datetime) >1 AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                sql = "SELECT * FROM filler_log " + sql_Qr;
                show_Data();
                get_combobox_item();
            }
            else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("กำหนดเอง"))
            {
                this.Opacity = 0.5;
                var pk = new PickerDate();              
                pk.ShowDialog();
                this.Opacity = 100;
                if(pk.cancel==false)
                {
                    startDate = Convert.ToDateTime(pk.startDate); // start en
                    stopDate = Convert.ToDateTime(pk.stopDate);  // stop en  
                    start = startDate.ToString("yyyy-MM-dd");
                    stop = stopDate.ToString("yyyy-MM-dd");
                    start_stop = pk.txt_Start_Date.Text + "  ถึง  " + pk.txt_Stop_Date.Text;
                    comboBox_Select.Items.Add(start_stop);
                    comboBox_Select.SelectedIndex = comboBox_Select.Items.IndexOf(start_stop);
                    sql_Qr = "where datetime BETWEEN" + "'" + start + "'" + "AND" + "'" + stop + "' AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id; ;
                    sql = "SELECT * FROM filler_log " + sql_Qr;
                    show_Data();
                    get_combobox_item();
                    select = true;
                }                     
            }         
        }
        
        private void msgError(String msg)
        {
            MessageBox.Show(msg, "พบปัญหา");
        }

        private void show_Data()
        {
            try
            {
                if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("รายวัน"))
                {
                    sql_Qr = "where datetime like '%" + System.DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "%' AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("รายสัปดาห์"))
                {
                    sql_Qr = "where YEARWEEK (date(datetime),3) = YEARWEEK (CURRENT_DATE(),3) AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("รายเดือน"))
                {
                    sql_Qr = "where datetime like '%" + System.DateTime.Now.Year.ToString("D" + 4) + "-" + (System.DateTime.Now.Month.ToString("D" + 2)) + "%' AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("รายปี"))
                {
                    sql_Qr = "where datetime like '%" + (System.DateTime.Now.Year.ToString("D" + 4)) + "%' AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 1 วัน"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -1 DAY) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 3 วัน"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -3 DAY) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 7 วัน"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -7 DAY) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 15 วัน"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -15 DAY) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 90 วัน"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -90 DAY) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 1 เดือน"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -1 MONTH) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 3 เดือน"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -3 MONTH) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 6 เดือน"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -6 MONTH) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 1 ปี"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -1 YEAR) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 3 ปี"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -3 YEAR) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ย้อนหลัง 6 ปี"))
                {
                    sql_Qr = "where  DATE(datetime) BETWEEN DATE_ADD(CURRENT_DATE, INTERVAL -6 YEAR) AND CURRENT_DATE AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("ทั้งหมด"))
                {
                    sql_Qr = "WHERE YEAR(datetime) >1 AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id;
                }
                else if (comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf("กำหนดเอง") || comboBox_Select.SelectedIndex == comboBox_Select.Items.IndexOf(start_stop))
                {
                    sql_Qr = "where datetime BETWEEN" + "'" + start + "'" + "AND" + "'" + stop + "' AND machine_id " + w_machine_id + " AND standard_size " + w_standardSize + " AND status " + w_status + " AND user_id " + w_user_id; ;                    
                }

                sql = "SELECT * FROM filler_log " + sql_Qr;
                //MessageBox.Show(sql);
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();

                dataG_Fill.Items.Clear();

                DataColumn No = new DataColumn("ลำดับ", typeof(String));
                DataColumn DateTime = new DataColumn("วัน / เวลา", typeof(DateTime));
                DataColumn machine_id = new DataColumn("หัวจ่าย", typeof(String));
                DataColumn standard_size = new DataColumn("ขนาดถัง", typeof(String));
                DataColumn scale_label = new DataColumn("นน. ถังเปล่า", typeof(String));
                DataColumn scale_tank = new DataColumn("นน. ถังชั่ง", typeof(String));
                DataColumn scale_after = new DataColumn("นน. สุทธิ", typeof(String));
                DataColumn scale_fill = new DataColumn("จำนวนบรรจุ(กก.)", typeof(String));
                DataColumn scale_liter = new DataColumn("จำนวนบรรจุ(ลิตร)", typeof(String));
                DataColumn time_count = new DataColumn("เวลาบรรจุ", typeof(String));
                DataColumn scale_drif = new DataColumn("ผลต่าง(กก.)", typeof(String));
                DataColumn status = new DataColumn("สถานะ", typeof(String));
                DataColumn user = new DataColumn("พนักงาน", typeof(String));

                sum_kg = 0;
                sum_lite = 0;
                sum_drif = 0;
                int n = 0;
                int num = 1;
                String number;
                String status_Read;
                int number_int;
                Fill fill = new Fill();
                while (read.Read())
                {

                    String datetime = (read["datetime"].ToString());
                    DateTime datetimeNew = Convert.ToDateTime(datetime.ToString());

                    decimal temp_Sum_kg = decimal.Parse((read["scale_fill"].ToString()));
                    decimal temp_Sum_lite = decimal.Parse((read["scale_liter"].ToString()));
                    decimal temp_Sum_drif = decimal.Parse((read["scale_drif"].ToString()));
                    sum_kg = sum_kg + temp_Sum_kg;
                    sum_lite = sum_lite + temp_Sum_lite;
                    sum_drif = sum_drif + temp_Sum_drif;

                    status_Read = (read["status"].ToString());
                    if (status_Read == "0") status_Read = "เสร็จสมบูรณ์";
                    if (status_Read == "1") status_Read = "อ่านค่าปริมาตรไม่ได้";
                    if (status_Read == "2") status_Read = "ปริมาตรผิดปรกติ";
                    if (status_Read == "3") status_Read = "ยกเลิกโดยผู้ใช้";
                    if (status_Read == "4") status_Read = "ยกเลิกโดยผู้ใช้,อ่านค่าปริมาตรไม่ได้";
                    if (status_Read == "5") status_Read = "ยกเลิกโดยผู้ใช้,ปริมาตรผิดปรกติ";
                    if (status_Read == "6") status_Read = "เครื่องตัดการเติม";
                    if (status_Read == "7") status_Read = "เครื่องตัดการเติม,อ่านค่าปริมาตรไม่ได้";
                    if (status_Read == "8") status_Read = "เครื่องตัดการเติม,ปริมาตรผิดปรกติ";

                    String userid = (read["user_id"].ToString());
                    Int32 useridInt = Convert.ToInt32(userid);  //  แปลงเป็น int 
                    id = String.Format("{0:000}", useridInt);
                    number = (read["user_id"].ToString());
                    int.TryParse(number, out number_int);
                    ////////////////  อ่านข้อมูล ชื่อพนักงาน
                    try
                    {
                        String sql1 = "SELECT name FROM user WHERE id= " + (number_int);
                        MySqlConnection conn1 = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                        MySqlCommand cmd1 = new MySqlCommand(sql1, conn1);
                        conn1.Open();
                        MySqlDataReader read1 = cmd1.ExecuteReader();
                        while (read1.Read())
                        {
                            name = (read1["name"].ToString());
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
                    /////////////////////////////////////////////////// 


                    var data = new Fill
                    {
                        No = num.ToString(),
                        DateTime = datetimeNew.ToString("dd/MM/yyyy HH:mm:ss"),
                        machine_id = read["machine_id"].ToString(),
                        standard_size = read["standard_size"].ToString(),
                        scale_label = read["scale_label"].ToString(),
                        scale_tank = read["scale_tank"].ToString(),
                        scale_after = read["scale_after"].ToString(),
                        scale_fill = read["scale_fill"].ToString(),
                        scale_liter = read["scale_liter"].ToString(),
                        time_count = read["time_count"].ToString(),
                        scale_drif = read["scale_drif"].ToString(),
                        status = status_Read,
                        user = "(" + id + ") " + name
                    };

                    dataG_Fill.Items.Add(data);

                    name = "";
                    n++;
                    num++;

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
        public class Fill
        {
            public string No { get; set; }
            public string DateTime { get; set; }
            public String machine_id { get; set; }
            public String standard_size { get; set; }
            public String scale_label { get; set; }
            public String scale_tank { get; set; }
            public String scale_after { get; set; }
            public String scale_fill { get; set; }
            public String scale_liter { get; set; }
            public String time_count { get; set; }
            public String scale_drif { get; set; }
            public String status { get; set; }
            public String user { get; set; }


        }
        public void get_combobox_item()
        {
            comboBox_standardSize.Items.Clear();
            comboBox_machineID.Items.Clear();
            comboBox_User.Items.Clear();
            comboBox_status.Items.Clear();

            comboBox_status.Items.Add("เสร็จสมบูรณ์");
            comboBox_status.Items.Add("อ่านค่าปริมาตรไม่ได้");
            comboBox_status.Items.Add("ปริมาตรผิดปรกติ");
            comboBox_status.Items.Add("ยกเลิกโดยผู้ใช้");
            comboBox_status.Items.Add("ยกเลิกโดยผู้ใช้,อ่านค่าปริมาตรไม่ได้");
            comboBox_status.Items.Add("ยกเลิกโดยผู้ใช้,ปริมาตรผิดปรกติ");
            comboBox_status.Items.Add("เครื่องตัดการเติม");
            comboBox_status.Items.Add("เครื่องตัดการเติม,อ่านค่าปริมาตรไม่ได้");
            comboBox_status.Items.Add("เครื่องตัดการเติม,ปริมาตรผิดปรกติ");
            comboBox_status.Items.Add("ทั้งหมด");

            try
            {
                String sql1 = "SELECT Distinct user_id FROM filler_log " + sql_Qr + " order by user_id";
                MySqlConnection conn1 = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn1);
                conn1.Open();
                MySqlDataReader read1 = cmd1.ExecuteReader();

                while (read1.Read())
                {
                    //comboBox_User.Items.Add(read1.GetString("user_id"));
                    String userid = (read1["user_id"].ToString());
                    Int32 useridInt = Convert.ToInt32(userid);  //  แปลงเป็น int 
                                                                //dataG_Main.Rows[n].Cells[12].Value = String.Format("{0:000}", useridInt);

                    id = String.Format("{0:000}", useridInt);

                    ////////////////  อ่านข้อมูล ชื่อพนักงาน
                    try
                    {
                        String sql = "SELECT name FROM user WHERE id= " + (id);
                        //String sql = "SELECT COALESCE(name,0) FROM user WHERE id= " + (id);
                        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();
                        MySqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            //dataG_Main.Rows[n].Cells[13].Value = (read1["name"].ToString());
                            name = (read["name"].ToString());
                            //MessageBox.Show(name);

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
                    String id_user = id + name;
                    comboBox_User.Items.Add(id + " (" + name + ")");
                    name = "";
                    //MessageBox.Show(id + name);
                }
                comboBox_User.Items.Add("ทั้งหมด");
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
            try
            {
                String sql1 = "SELECT Distinct machine_id FROM filler_log " + sql_Qr + " order by machine_id";
                MySqlConnection conn1 = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn1);
                conn1.Open();
                MySqlDataReader read1 = cmd1.ExecuteReader();
                while (read1.Read())
                {
                    comboBox_machineID.Items.Add(read1.GetString("machine_id"));
                }
                comboBox_machineID.Items.Add("ทั้งหมด");
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
            try
            {
                String sql1 = "SELECT Distinct standard_size FROM filler_log " + sql_Qr + " order by standard_size";
                MySqlConnection conn1 = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn1);
                conn1.Open();
                MySqlDataReader read1 = cmd1.ExecuteReader();
                while (read1.Read())
                {
                    comboBox_standardSize.Items.Add(read1.GetString("standard_size"));
                }
                comboBox_standardSize.Items.Add("ทั้งหมด");
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_refesh_Click(object sender, EventArgs e)
        {
            show_Data();
        }

        private void Start_Picker_CloseUp(object sender, EventArgs e)
        {
            comboBox_Select.SelectedIndex = comboBox_Select.Items.IndexOf("กำหนดเอง");
        }

        //private void Stop_Picker_CloseUp(object sender, EventArgs e)
        //{
        //    comboBox_Select.SelectedIndex = comboBox_Select.Items.IndexOf("กำหนดเอง");
        //    sql = "SELECT * FROM filler_log where datetime BETWEEN" + "'" + this.Start_Picker.Value.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "'" + "AND" + "'" + this.Stop_Picker.Value.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "'";
        //    show_Data();
        //}


        private void comboBox_machineID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_machineID.SelectedIndex == comboBox_machineID.Items.IndexOf("ทั้งหมด") || comboBox_machineID.SelectedIndex == -1)
            {
                w_machine_id = "!= -99";
            }
            else
            {
                w_machine_id = "= " + (comboBox_machineID.SelectedItem.ToString());
            }
            show_Data();
        }

        private void comboBox_standardSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_standardSize.SelectedIndex == comboBox_standardSize.Items.IndexOf("ทั้งหมด") || comboBox_standardSize.SelectedIndex == -1)
            {
                w_standardSize = "!= -99";
            }
            else
            {
                w_standardSize = "= " + (comboBox_standardSize.SelectedItem.ToString());
            }
            show_Data();
        }

        private void comboBox_status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_status.SelectedIndex == comboBox_status.Items.IndexOf("ทั้งหมด") || comboBox_status.SelectedIndex == -1)
            {
                w_status = "!= -99";
            }
            else
            {
                w_status = "= " + (comboBox_status.SelectedIndex.ToString());
                //MessageBox.Show(w_status);
            }
            show_Data();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            w_machine_id = "!= -99";
            w_standardSize = "!= -99";
            w_status = "!= -99";
            w_user_id = "!= -99";

            this.comboBox_Select.SelectedIndex = 0;
            comboBox_machineID.SelectedIndex = -1;
            comboBox_standardSize.SelectedIndex = -1;
            comboBox_User.SelectedIndex = -1;
            comboBox_status.SelectedIndex = -1;
            show_Data();
        }
        private void comboBox_User_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_User.SelectedIndex == comboBox_User.Items.IndexOf("ทั้งหมด") || comboBox_User.SelectedIndex == -1)
            {
                w_user_id = "!= -99";
            }
            else
            {
                String id = (comboBox_User.SelectedItem.ToString());
                w_user_id = "= " + id.Substring(0, 3);
                //MessageBox.Show(w_user_id);
            }
            show_Data();
        }
    }
}
