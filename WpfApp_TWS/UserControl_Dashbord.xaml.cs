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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace WpfApp_TWS
{
    /// <summary>
    /// Interaction logic for UserControl_Dashbord.xaml
    /// </summary>
    public partial class UserControl_Dashbord : UserControl
    {
        public bool networkFail = false;
        public double sum_4;
        public double sum_7;
        public double sum_11;
        public double sum_11_5;
        public double sum_13_5;
        public double sum_15;
        public double sum_48;
        public double sum_mix_1, sum_mix_2, sum_mix_3, sum_mix_4, sum_mix_5, sum_mix_6, sum_mix_7, sum_mix_8, sum_mix_9, sum_mix_10, sum_mix_11, sum_mix_12;
        public double[,] sum_scale_month = new double[,] { { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 },
                                                            { 0, 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0, 0 }};

        public UserControl_Dashbord()
        {
            InitializeComponent();
        }

        public SeriesCollection SeriesCollection { get; set; }

        private void Dashbord_Loaded(object sender, RoutedEventArgs e)
        {
            count_online();
            sum_kg_today();
            sum_lite_today();
            count_day();
            count_month();
            count_year();
            noti_count();
            sum_size();
            donutShow();
            sum_mouth_fill();
            graf_load();
        }

        private void sum_size()
        {
            try
            {
                String sql = "SELECT * FROM filler_log where standard_size = 4 AND datetime like '%" + (DateTime.Today.Year.ToString("D" + 4)) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                    sum_4 = n;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
            try
            {
                String sql = "SELECT * FROM filler_log where standard_size = 7 AND datetime like '%" + (DateTime.Today.Year.ToString("D" + 4)) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                    sum_7 = n;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
            try
            {
                String sql = "SELECT * FROM filler_log where standard_size = 11 AND datetime like '%" + (DateTime.Today.Year.ToString("D" + 4)) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                    sum_11 = n;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
            try
            {
                String sql = "SELECT * FROM filler_log where standard_size = 11.5 AND datetime like '%" + (DateTime.Today.Year.ToString("D" + 4)) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                    sum_11_5 = n;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
            try
            {
                String sql = "SELECT * FROM filler_log where standard_size = 13.5 AND datetime like '%" + (DateTime.Today.Year.ToString("D" + 4)) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                    sum_13_5 = n;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
            try
            {
                String sql = "SELECT * FROM filler_log where standard_size = 15 AND datetime like '%" + (DateTime.Today.Year.ToString("D" + 4)) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                    sum_15 = n;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
            try
            {
                String sql = "SELECT * FROM filler_log where standard_size = 48 AND datetime like '%" + (DateTime.Today.Year.ToString("D" + 4)) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                    sum_48 = n;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
        }

        private void donutShow()
        {
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            SeriesCollection = new SeriesCollection
            {
                new PieSeries
        {
            Title = "4 กก.",
            Values = new ChartValues<double> {sum_4},
            DataLabels = true,
            //LabelPoint = labelPoint
            
        },
        new PieSeries
        {
            Title = "7 กก.",
            Values = new ChartValues<double> {sum_7},
            DataLabels = true,
            //LabelPoint = labelPoint
        },
        new PieSeries
        {
            Title = "11 กก.",
            Values = new ChartValues<double> {sum_11},
            DataLabels = true,
            //LabelPoint = labelPoint
        },
        new PieSeries
        {
            Title = "11.5 กก.",
            Values = new ChartValues<double> {sum_11_5},
            DataLabels = true,
            //LabelPoint = labelPoint
        },
        new PieSeries
        {
            Title = "13.5 กก.",
            Values = new ChartValues<double> {sum_13_5},
            DataLabels = true,
            //LabelPoint = labelPoint
        },
        new PieSeries
        {
            Title = "15 กก.",
            Values = new ChartValues<double> {sum_15},
            DataLabels = true,
            //LabelPoint = labelPoint
        },
        new PieSeries
        {
            Title = "48 กก.",
            Values = new ChartValues<double> {sum_48},
            DataLabels = true,
            //LabelPoint = labelPoint
        }
        };

            // Define the collection of Values to display in the Pie Chart
            //Chart1.Series = piechartData;
            // Set the legend location to appear in the Right side of the chart
            Chart1.LegendLocation = LegendLocation.Right;

            DataContext = this;
        }

        private void graf_load()
        {
            sum_mix_1 = sum_scale_month[0, 0] + sum_scale_month[0, 1] + sum_scale_month[0, 2] + sum_scale_month[0, 3] + sum_scale_month[0, 4] + sum_scale_month[0, 5] + sum_scale_month[0, 6];
            sum_mix_2 = sum_scale_month[1, 0] + sum_scale_month[1, 1] + sum_scale_month[1, 2] + sum_scale_month[1, 3] + sum_scale_month[1, 4] + sum_scale_month[1, 5] + sum_scale_month[1, 6];
            sum_mix_3 = sum_scale_month[2, 0] + sum_scale_month[2, 1] + sum_scale_month[2, 2] + sum_scale_month[2, 3] + sum_scale_month[2, 4] + sum_scale_month[2, 5] + sum_scale_month[2, 6];
            sum_mix_4 = sum_scale_month[3, 0] + sum_scale_month[3, 1] + sum_scale_month[3, 2] + sum_scale_month[3, 3] + sum_scale_month[3, 4] + sum_scale_month[3, 5] + sum_scale_month[3, 6];
            sum_mix_5 = sum_scale_month[4, 0] + sum_scale_month[4, 1] + sum_scale_month[4, 2] + sum_scale_month[4, 3] + sum_scale_month[4, 4] + sum_scale_month[4, 5] + sum_scale_month[4, 6];
            sum_mix_6 = sum_scale_month[5, 0] + sum_scale_month[5, 1] + sum_scale_month[5, 2] + sum_scale_month[5, 3] + sum_scale_month[5, 4] + sum_scale_month[5, 5] + sum_scale_month[5, 6];
            sum_mix_7 = sum_scale_month[6, 0] + sum_scale_month[6, 1] + sum_scale_month[6, 2] + sum_scale_month[6, 3] + sum_scale_month[6, 4] + sum_scale_month[6, 5] + sum_scale_month[6, 6];
            sum_mix_8 = sum_scale_month[7, 0] + sum_scale_month[7, 1] + sum_scale_month[7, 2] + sum_scale_month[7, 3] + sum_scale_month[7, 4] + sum_scale_month[7, 5] + sum_scale_month[7, 6];
            sum_mix_9 = sum_scale_month[8, 0] + sum_scale_month[8, 1] + sum_scale_month[8, 2] + sum_scale_month[8, 3] + sum_scale_month[8, 4] + sum_scale_month[8, 5] + sum_scale_month[8, 6];
            sum_mix_10 = sum_scale_month[9, 0] + sum_scale_month[9, 1] + sum_scale_month[9, 2] + sum_scale_month[9, 3] + sum_scale_month[9, 4] + sum_scale_month[9, 5] + sum_scale_month[9, 6];
            sum_mix_11 = sum_scale_month[10, 0] + sum_scale_month[10, 1] + sum_scale_month[10, 2] + sum_scale_month[10, 3] + sum_scale_month[10, 4] + sum_scale_month[10, 5] + sum_scale_month[10, 6];
            sum_mix_12 = sum_scale_month[11, 0] + sum_scale_month[11, 1] + sum_scale_month[11, 2] + sum_scale_month[11, 3] + sum_scale_month[11, 4] + sum_scale_month[11, 5] + sum_scale_month[11, 6];

            //CartesianChart1.Series.Clear();
            //CartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            //{
            //    //Separator = new Separator
            //    //{
            //    //    Step = 1,
            //    //    IsEnabled = false                  
            //    //},
            //    //LabelsRotation = 15,
            //    //Title = "เดือน",

            //    Labels = new[] { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤาภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" }
            //});
            //CartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            //{
            //    LabelFormatter = value => value + "กก.",
            //    Title = "ยอดขาย",
            //}
            //);
            CartesianChart1.LegendLocation = LiveCharts.LegendLocation.Bottom;

            CartesianChart1.Series = new SeriesCollection
            //SeriesCollection1 = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "ขนาด 4 กก.",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,sum_scale_month[0, 0]),
                        new ObservablePoint(1,sum_scale_month[1, 0]),
                        new ObservablePoint(2,sum_scale_month[2, 0]),
                        new ObservablePoint(3,sum_scale_month[3, 0]),
                        new ObservablePoint(4,sum_scale_month[4, 0]),
                        new ObservablePoint(5,sum_scale_month[5, 0]),
                        new ObservablePoint(6,sum_scale_month[6, 0]),
                        new ObservablePoint(7,sum_scale_month[7, 0]),
                        new ObservablePoint(8,sum_scale_month[8, 0]),
                        new ObservablePoint(9,sum_scale_month[9, 0]),
                        new ObservablePoint(10,sum_scale_month[10, 0]),
                        new ObservablePoint(11,sum_scale_month[11, 0]),
                    },
                    PointGeometrySize = 8,
                },

                new LineSeries
                {
                    Title = "ขนาด 7 กก.",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,sum_scale_month[0, 1]),
                        new ObservablePoint(1,sum_scale_month[1, 1]),
                        new ObservablePoint(2,sum_scale_month[2, 1]),
                        new ObservablePoint(3,sum_scale_month[3, 1]),
                        new ObservablePoint(4,sum_scale_month[4, 1]),
                        new ObservablePoint(5,sum_scale_month[5, 1]),
                        new ObservablePoint(6,sum_scale_month[6, 1]),
                        new ObservablePoint(7,sum_scale_month[7, 1]),
                        new ObservablePoint(8,sum_scale_month[8, 1]),
                        new ObservablePoint(9,sum_scale_month[9, 1]),
                        new ObservablePoint(10,sum_scale_month[10, 1]),
                        new ObservablePoint(11,sum_scale_month[11, 1]),
                    },
                    PointGeometrySize = 8
                },
                new LineSeries
                {
                    Title = "ขนาด 11 กก.",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,sum_scale_month[0, 2]),
                        new ObservablePoint(1,sum_scale_month[1, 2]),
                        new ObservablePoint(2,sum_scale_month[2, 2]),
                        new ObservablePoint(3,sum_scale_month[3, 2]),
                        new ObservablePoint(4,sum_scale_month[4, 2]),
                        new ObservablePoint(5,sum_scale_month[5, 2]),
                        new ObservablePoint(6,sum_scale_month[6, 2]),
                        new ObservablePoint(7,sum_scale_month[7, 2]),
                        new ObservablePoint(8,sum_scale_month[8, 2]),
                        new ObservablePoint(9,sum_scale_month[9, 2]),
                        new ObservablePoint(10,sum_scale_month[10, 2]),
                        new ObservablePoint(11,sum_scale_month[11, 2]),
                    },
                    PointGeometrySize = 8
                },
                new LineSeries
                {
                    Title = "ขนาด 11.5 กก.",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,sum_scale_month[0, 3]),
                        new ObservablePoint(1,sum_scale_month[1, 3]),
                        new ObservablePoint(2,sum_scale_month[2, 3]),
                        new ObservablePoint(3,sum_scale_month[3, 3]),
                        new ObservablePoint(4,sum_scale_month[4, 3]),
                        new ObservablePoint(5,sum_scale_month[5, 3]),
                        new ObservablePoint(6,sum_scale_month[6, 3]),
                        new ObservablePoint(7,sum_scale_month[7, 3]),
                        new ObservablePoint(8,sum_scale_month[8, 3]),
                        new ObservablePoint(9,sum_scale_month[9, 3]),
                        new ObservablePoint(10,sum_scale_month[10, 3]),
                        new ObservablePoint(11,sum_scale_month[11, 3]),
                    },
                    PointGeometrySize = 8
                },
                new LineSeries
                {
                    Title = "ขนาด 13.5 กก.",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,sum_scale_month[0, 4]),
                        new ObservablePoint(1,sum_scale_month[1, 4]),
                        new ObservablePoint(2,sum_scale_month[2, 4]),
                        new ObservablePoint(3,sum_scale_month[3, 4]),
                        new ObservablePoint(4,sum_scale_month[4, 4]),
                        new ObservablePoint(5,sum_scale_month[5, 4]),
                        new ObservablePoint(6,sum_scale_month[6, 4]),
                        new ObservablePoint(7,sum_scale_month[7, 4]),
                        new ObservablePoint(8,sum_scale_month[8, 4]),
                        new ObservablePoint(9,sum_scale_month[9, 4]),
                        new ObservablePoint(10,sum_scale_month[10,4]),
                        new ObservablePoint(11,sum_scale_month[11,4]),
                    },
                    PointGeometrySize = 8
                },
                new LineSeries
                {
                    Title = "ขนาด 15 กก.",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,sum_scale_month[0, 5]),
                        new ObservablePoint(1,sum_scale_month[1, 5]),
                        new ObservablePoint(2,sum_scale_month[2, 5]),
                        new ObservablePoint(3,sum_scale_month[3, 5]),
                        new ObservablePoint(4,sum_scale_month[4, 5]),
                        new ObservablePoint(5,sum_scale_month[5, 5]),
                        new ObservablePoint(6,sum_scale_month[6, 5]),
                        new ObservablePoint(7,sum_scale_month[7, 5]),
                        new ObservablePoint(8,sum_scale_month[8, 5]),
                        new ObservablePoint(9,sum_scale_month[9, 5]),
                        new ObservablePoint(10,sum_scale_month[10, 5]),
                        new ObservablePoint(11,sum_scale_month[11, 5]),
                    },
                    PointGeometrySize = 8
                },
                new LineSeries
                {
                    Title = "ขนาด 48 กก.",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,sum_scale_month[0, 6]),
                        new ObservablePoint(1,sum_scale_month[1, 6]),
                        new ObservablePoint(2,sum_scale_month[2, 6]),
                        new ObservablePoint(3,sum_scale_month[3, 6]),
                        new ObservablePoint(4,sum_scale_month[4, 6]),
                        new ObservablePoint(5,sum_scale_month[5, 6]),
                        new ObservablePoint(6,sum_scale_month[6, 6]),
                        new ObservablePoint(7,sum_scale_month[7, 6]),
                        new ObservablePoint(8,sum_scale_month[8, 6]),
                        new ObservablePoint(9,sum_scale_month[9, 6]),
                        new ObservablePoint(10,sum_scale_month[10, 6]),
                        new ObservablePoint(11,sum_scale_month[11, 6]),
                    },
                    PointGeometrySize = 8
                },
                 new LineSeries
                {
                    Title = "รวม",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,sum_mix_1),
                        new ObservablePoint(1,sum_mix_2),
                        new ObservablePoint(2,sum_mix_3),
                        new ObservablePoint(3,sum_mix_4),
                        new ObservablePoint(4,sum_mix_5),
                        new ObservablePoint(5,sum_mix_6),
                        new ObservablePoint(6,sum_mix_7),
                        new ObservablePoint(7,sum_mix_8),
                        new ObservablePoint(8,sum_mix_9),
                        new ObservablePoint(9,sum_mix_10),
                        new ObservablePoint(10,sum_mix_11),
                        new ObservablePoint(11,sum_mix_12),
                    },
                    PointGeometrySize = 15,
                    //PointGeometry = DefaultGeometries.Square
                    //DataLabels = true,
                    //LabelPoint = point => point.Y + "กก."
                },
            };
            Labels = new[] { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤาภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
            DataContext = this;
        }


        public SeriesCollection SeriesCollection1 { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void msgError(String msg)
        {
            MessageBox.Show(msg, "พบปัญหา");
        }

        private void count_online()
        {
            try
            {
                String sql = "SELECT * FROM machine_online WHERE  online = '1' AND datetime like '%" + DateTime.Today.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                }
                conn.Close();
                l_machine_online_count.Text = n.ToString();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
            try
            {
                String sql = "SELECT DISTINCT user_id FROM machine_online WHERE  online = '1' AND datetime like '%" + DateTime.Today.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                }
                conn.Close();
                l_userID_cound_online.Text = n.ToString();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
        }

        private void sum_kg_today()
        {
            try
            {
                String sql = "SELECT IFNULL(SUM(scale_fill),0) FROM filler_log where datetime like '%" + DateTime.Today.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    l_kg_today.Text = (read["IFNULL(SUM(scale_fill),0)"].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
        }

        private void sum_lite_today()
        {
            try
            {
                String sql = "SELECT IFNULL(SUM(scale_liter),0) FROM filler_log where datetime like '%" + DateTime.Today.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    l_liter_today.Text = (read["IFNULL(SUM(scale_liter),0)"].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
        }

        private void count_day()
        {
            try
            {
                String sql = "SELECT * FROM filler_log where datetime like '%" + DateTime.Today.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                }
                conn.Close();
                l_day_count.Text = n.ToString();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
        }
        private void count_month()
        {
            try
            {
                String sql = "SELECT * FROM filler_log where datetime like '%" + DateTime.Today.Year.ToString("D" + 4) + "-" + (DateTime.Today.Month.ToString("D" + 2)) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                }
                conn.Close();
                l_month_count.Text = n.ToString();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
        }
        private void count_year()
        {
            try
            {
                String sql = "SELECT * FROM filler_log where datetime like '%" + (DateTime.Today.Year.ToString("D" + 4)) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                }
                conn.Close();
                l_year_count.Text = n.ToString();
            }
            catch (Exception ex)
            {
                msgError(ex.Message);
            }
        }

        private void noti_count()
        {
            try
            {
                String sql = "SELECT * FROM filler_log where status != 0 AND datetime like '%" + DateTime.Today.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "%'";
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                int n = 0;
                while (read.Read())
                {
                    n++;
                }
                conn.Close();
                l_noti_count.Text = n.ToString();
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

        private void sum_mouth_fill()
        {
            int i;
            int n = 0;
            String size = "";
            for (n = 0; n < 7; n++)
            {
                for (i = 1; i < 13; i++)
                {
                    try
                    {
                        if (n == 0)
                        {
                            size = "4";
                        }
                        if (n == 1)
                        {
                            size = "7";
                        }
                        if (n == 2)
                        {
                            size = "11";
                        }
                        if (n == 3)
                        {
                            size = "11.5";
                        }
                        if (n == 4)
                        {
                            size = "13.5";
                        }
                        if (n == 5)
                        {
                            size = "15";
                        }
                        if (n == 6)
                        {
                            size = "48";
                        }

                        String sql = "SELECT standard_size, IFNULL(SUM(scale_fill),0) FROM filler_log where standard_size =" + size + " AND  datetime like '%" + (DateTime.Today.Year.ToString("D" + 4) + "-" + String.Format("{0:00}", i)) + "%'";
                        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='panda1';username=vsApp;password=Panda8133;CharSet=UTF8");
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        conn.Open();
                        MySqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            String sum_month = (read["IFNULL(SUM(scale_fill),0)"].ToString());
                            sum_scale_month[i - 1, n] = Convert.ToDouble(sum_month);
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        msgError(ex.Message);
                    }
                }
            }
        }
    }
}
