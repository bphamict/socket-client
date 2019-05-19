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
    /// Interaction logic for GetInfoPaymentWindow.xaml
    /// </summary>
    public partial class GetInfoPaymentWindow : Window
    {
        public GetInfoPaymentWindow()
        {
            InitializeComponent();
        }

        public string bankID;
        public string cardID;

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            bankID = BankID_TextBox.Text;
            cardID = CardID_TextBox.Text;

            if (bankID == "")
            {
                MessageBox.Show("Bank ID is empty");
            }
            else if (!Regex.IsMatch(bankID, @"^[A-Z]+$") || bankID.Length != 3)
            {
                MessageBox.Show("Bank ID is not correct\nBank ID must have 3 characters uppercase");
            }
            else if (cardID == "")
            {
                MessageBox.Show("Card ID is empty");
            }
            else if (!Regex.IsMatch(cardID, @"^\d+$") || cardID.Length != 10)
            {
                MessageBox.Show("Card ID is not correct\nCard ID must have 10 numbers");
            }
            else
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
