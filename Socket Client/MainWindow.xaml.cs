using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        private Socket _socket;

        Int32 port = 3000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        // Buffer for reading data
        byte[] bytes = new Byte[1024];
        byte[] msg;
        string data = null;
        int i;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ListProducts = new List<product>();

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(new IPEndPoint(localAddr, port));

            var screen = new GetNameWindow();

            if (screen.ShowDialog() == false) { this.Close(); }

            // Send name to server
            msg = Encoding.ASCII.GetBytes(screen.name);
            _socket.Send(msg);

            // Receive product data & add to list
            while (true)
            {
                bool flag = false;

                if ((i = _socket.Receive(bytes)) != 0)
                {
                    data = Encoding.ASCII.GetString(bytes, 0, i);

                    if (data == "NAME EXIST")
                    {
                        MessageBox.Show("Name exist");

                        var screen2 = new GetNameWindow();
                        if (screen2.ShowDialog() == false) { this.Close(); }

                        msg = Encoding.ASCII.GetBytes(screen2.name);
                        _socket.Send(msg);

                        continue;
                    }

                    string[] words = data.Split('*');

                    foreach (string s in words)
                    {
                        if (s == "EOF") { flag = true; break; }
                        else if (s != "") { addProductToList(s); }
                    }
                }

                if (flag) { break; }
            }

            Products_DataGrid.ItemsSource = _ListProducts;
            Products_ComboBox.ItemsSource = _ListProducts;

            // Wait until receive result of auction session

            // If client winner
            var screen1 = new GetInfoPaymentWindow();
            if (screen1.ShowDialog() == false) { MessageBox.Show("You not fill in form"); }

            //Good luck to you next time
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
                // Send product chosen to server
                //msg = Encoding.ASCII.GetBytes();
                //_socket.Send(msg);
            }
        }
    }
}
