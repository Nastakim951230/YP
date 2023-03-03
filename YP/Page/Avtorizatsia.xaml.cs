using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using System.Web;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace YP.Page
{
    /// <summary>
    /// Логика взаимодействия для Avtorizatsia.xaml
    /// </summary>
    public partial class Avtorizatsia
    {
        public static string nomer = "89063490993";
        public static string password = "01";
        string text = String.Empty;
       
        DispatcherTimer disTimer = new DispatcherTimer();
        int sec = 10;

        public Avtorizatsia()
        {
            InitializeComponent();
          
            
        }
        private void dtTicker(object sender, EventArgs e)
        {
            if(sec==0)
            {
                otpravka.IsEnabled = true;
                kod.IsEnabled = false;
                disTimer.Stop();
                sec = 10;
               
            }
            else
            {
                sec=sec-1;
                time.Text = "Время закончиться через:" + sec;
            }
        }
        private void Nomer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(nomer ==Nomer.Text)
                {
                    Password.IsEnabled=true;
                    Password.Focus();
                }
                else
                {
                    MessageBox.Show("Такого номера нету.");
                }
            }
        }

        public void KOD()
        {
            otpravka.IsEnabled=false;
            kod.IsEnabled = true;
            kod.Focus();
            Random random = new Random();
            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm !#$%&'()*+,-/:;<=>?^_";

            Regex regex = new Regex("(?=(.*?[A-Z]){2})(?=(.*?[a-z]){3})(?=(.*?[0-9]){2})(?=(.*?[ !#$%&'()*+,-/:;<=>?^_]){1}).{8}");
            while (regex.IsMatch(text) == false)
            {
                text = "";
                for (int i = 0; i < 8; ++i)
                    text += ALF[random.Next(ALF.Length)];
            }
            string caption = "Запомните код";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result;
            result = MessageBox.Show(text, caption, button, icon, MessageBoxResult.Yes);
            if (result == MessageBoxResult.OK)
            {
                time.Visibility = Visibility.Visible;
                disTimer.Interval = TimeSpan.FromSeconds(1);
                disTimer.Tick += dtTicker;
                disTimer.Start();
            }

        }
        private void Password_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (password == Password.Text)
                {
                    
                    KOD();
                   
                }
                else
                {
                    MessageBox.Show("Пароль не правильный.");
                }
            }
        }

        private void otpravka_Click(object sender, RoutedEventArgs e)
        {
            
            text = "";
            KOD();

        }
    }
}
