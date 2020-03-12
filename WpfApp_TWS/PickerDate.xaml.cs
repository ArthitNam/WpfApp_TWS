using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Controls.Primitives;

namespace WpfApp_TWS
{
    /// <summary>
    /// Interaction logic for PickerDate.xaml
    /// </summary>
    public partial class PickerDate : Window
    {
        public String startDate;
        public String stopDate;
        public bool cancel = false;

        public MainFill MdiParent { get; internal set; }

        public PickerDate()
        {
            InitializeComponent();           
        }
        

        private void Calendar_Start_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {           
            DateTime datetimeNew = Convert.ToDateTime(Calendar_Start.SelectedDate.ToString());           
            txt_Start_Date.Text = datetimeNew.ToString("dd/MM/yyyy");  
            startDate = datetimeNew.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
            Calendar_Stop.DisplayDateStart = Calendar_Start.SelectedDate;
        }

        private void Calendar_Stop_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime datetimeNew = Convert.ToDateTime(Calendar_Stop.SelectedDate.ToString());           
            txt_Stop_Date.Text = datetimeNew.ToString("dd/MM/yyyy");
            stopDate = datetimeNew.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
            
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            cancel = true;
            this.Close();
        }

        private void btn_Enter_Click_1(object sender, RoutedEventArgs e)
        {
            cancel = false;
            this.Close();
        }

        private void Calendar_Start_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        private void Calendar_Stop_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is CalendarItem)
            {
                Mouse.Capture(null);
            }
        }
    }
}
