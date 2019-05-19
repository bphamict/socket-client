using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Socket_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private class product
        {
            public string index { get; set; }
            public string name { get; set; }
            public string price { get; set; }
        }

        private List<product> _ListProducts = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ListProducts = new List<product>();

            //Int32 port = 3000;
            //TcpClient client = new TcpClient("127.0.0.1", port);

            var screen = new GetNameWindow();

            if (screen.ShowDialog() == false) { this.Close(); }
            else
            {
                // Send name to server

                // Receive product data & add to list
                addProductToList("1-abc-1000");
                addProductToList("2-def-1000");
                addProductToList("3-acc-1000");
                addProductToList("4-qqq-1000");

                Products_DataGrid.ItemsSource = _ListProducts;
                Products_ComboBox.ItemsSource = _ListProducts;

                // Wait until receive result of auction session
                var screen1 = new GetInfoPaymentWindow();
                screen1.ShowDialog();

                //Good luck to you next time
            }
        }

        private void addProductToList(string s)
        {
            string[] words = s.Split('-');

            _ListProducts.Add(new product()
            {
                index = words[0],
                name = words[1],
                price = words[2]
            });
        }

        private void Send_Button(object sender, RoutedEventArgs e)
        {
            string index;

            try { index = ((product)Products_ComboBox.SelectedItem).index; }
            catch { MessageBox.Show("Choose a product please"); return; }

            string price = Price_TextBox.Text;

            if (price == "")
            {
                MessageBox.Show("Price is empty");
            }
            else if (!Regex.IsMatch(price, @"^\d+$"))
            {
                MessageBox.Show("Price is not correct");
            }
            else
            {
                MessageBox.Show(index);
            }
        }
    }
}
