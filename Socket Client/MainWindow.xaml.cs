using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Threading;

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

            Instance = this;
        }

        // Instance to dispatcher invoke mainwindow
        private MainWindow Instance { get; set; }

        private class product
        {
            public string index { get; set; }
            public string name { get; set; }
            public string price { get; set; }
        }

        private List<product> _ListProducts = null;

        private string _name_customer;

        private Socket _socket;

        Int32 port = 3000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        // Buffer for reading data
        byte[] bytes = new Byte[1024];
        byte[] msg;
        string data = null;
        int i;

        // Flag to check sent
        private bool flag = false;

        // Create countdown timer
        private DateTime time = new DateTime(1, 1, 1, 0, 1, 0);

        // Call func interval = 1s
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            CountDown.Text = time.ToString("mm:ss");
            time = time.AddSeconds(-1);
        }

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            _ListProducts = new List<product>();

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(new IPEndPoint(localAddr, port));

            var screen = new GetNameWindow();

            if (screen.ShowDialog() == false) { this.Close(); }

            // Send customer name to server
            _name_customer = screen.name;
            msg = Encoding.ASCII.GetBytes(_name_customer);
            _socket.Send(msg);

            // Receive product data & add to list
            while (true)
            {
                // flag to check if receive EOF products list
                bool flag = false;

                if ((i = _socket.Receive(bytes)) != 0)
                {
                    data = Encoding.ASCII.GetString(bytes, 0, i);

                    // Check if receive full customer
                    if (data == "FULL CUSTOMER")
                    {
                        MessageBox.Show("Full customer, come back later");
                        return;
                    }
                    // Check if receive existed name
                    else if (data == "NAME EXIST")
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

            new Thread(() => { CheckResult(); }).Start();
        }

        private void CheckResult()
        {
            // Start countdown
            dispatcherTimer.Start();

            while (true)
            {
                if (time.Second == 1 && time.Minute != 1)
                {
                    dispatcherTimer.Stop();
                    break;
                }
            }

            // Check customer win or lose
            while ((i = _socket.Receive(bytes)) != 0)
            {
                data = Encoding.ASCII.GetString(bytes, 0, i);
                break;
            };

            if (data == "WIN")
            {
                Instance.Dispatcher.Invoke(() =>
                {
                    var screen1 = new GetInfoPaymentWindow();
                    if (screen1.ShowDialog() == false) { MessageBox.Show("You not fill in form"); }
                });
            }
            else if (data == "LOSE")
            {
                MessageBox.Show("You Lose! Good luck to you next time");
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
            if (flag == false) flag = true;
            else { MessageBox.Show("You has sent"); return; }

            product p;

            try { p = ((product)Products_ComboBox.SelectedItem); }
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
            else if (int.Parse(price) < int.Parse(p.price))
            {
                MessageBox.Show("Price is less than origin price");
            }
            else if (time.Second == 0)
            {
                MessageBox.Show("Time was up");
            }
            else
            {
                // Send product chosen to server
                msg = Encoding.ASCII.GetBytes(p.index + "-" + price + "-" + _name_customer);
                _socket.Send(msg);
                MessageBox.Show("Sent");
            }
        }
    }
}
