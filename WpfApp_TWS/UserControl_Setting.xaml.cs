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

namespace WpfApp_TWS
{
    /// <summary>
    /// Interaction logic for UserControl_Setting.xaml
    /// </summary>
    public partial class UserControl_Setting : UserControl
    {
        public UserControl_Setting()
        {
            InitializeComponent();
            Panal_Setting_Container.Children.Clear();
            Panal_Setting_Container.Children.Add(new UserControl_Setting_Company());
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            Panal_Setting_Container.Children.Clear();
            Panal_Setting_Container.Children.Add(new UserControl_Setting_Program());
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            Panal_Setting_Container.Children.Clear();
            Panal_Setting_Container.Children.Add(new UserControl_Setting_Company());
        }
    }
}
