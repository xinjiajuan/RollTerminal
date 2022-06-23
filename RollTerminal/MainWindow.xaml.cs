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
using Renci.SshNet;

namespace RollTerminal
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
        PasswordConnectionInfo connectionInfo;
        StringBuilder result = new StringBuilder();
        SshClient client;
        bool connentSuccess = false;
        private void button_connect_Click(object sender, RoutedEventArgs e)
        {
            if (!connentSuccess)
            {
                connectionInfo = new PasswordConnectionInfo(textBox_host.Text, textBox_username.Text, passwordBox.Password);
                client = new SshClient(connectionInfo);
                try
                {
                    client.Connect();
                    connentSuccess = true;
                    button_connect.Content = "disconnect";
                    result.AppendLine("Connect Success!");
                }
                catch (Exception ex)
                {
                    result.AppendLine(ex.ToString());
                    textBox_resurlt.Text = result.ToString();
                }
            }
            else
            {
                client.Disconnect();
                textBox_resurlt.Text = "";
                textBox_command.Text = "";
                connentSuccess = false;
                button_connect.Content = "connect";
                result.AppendLine("Discnnected!");
            }
        }

        private void button_send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                result.AppendLine(client.RunCommand(textBox_command.Text).Execute());
                textBox_resurlt.Text = result.ToString();
            }
            catch (Exception ex)
            {
                result.AppendLine(ex.ToString());
                textBox_resurlt.Text = result.ToString();
            }
        }
    }
}
