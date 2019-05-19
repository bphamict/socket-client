using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace Socket_Client
{
    /// <summary>
    /// Interaction logic for GetNameWindow.xaml
    /// </summary>
    public partial class GetNameWindow : Window
    {
        public GetNameWindow()
        {
            InitializeComponent();
        }

        public string name;

        private bool IsValidEmail(string email)
        {
            Regex rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return rx.IsMatch(email);
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            name = Name_TextBox.Text;

            if (name == "")
            {
                MessageBox.Show("Name is empty");
            }
            else if (!IsValidEmail(name))
            {
                MessageBox.Show("Name is not correct\nName is a email");
            }
            else
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
