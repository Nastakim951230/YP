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

        string text = String.Empty;
       
        DispatcherTimer disTimer = new DispatcherTimer();
        int sec = 10;//переменная для времени
        Sotrudnik sotrudnik;
        public Avtorizatsia()
        {
            InitializeComponent();

            disTimer.Interval = TimeSpan.FromSeconds(1);
            disTimer.Tick += dtTicker;

        }
        //Таймер
        private void dtTicker(object sender, EventArgs e)
        {
            if(sec==0)
            {
                otpravka.IsEnabled = true;
                kod.IsEnabled = false;
                Vhod.IsEnabled = false;
                disTimer.Stop();
                sec = 10;
                time.Text = "Время закончилось";
                

            }
            else
            {
                sec=sec-1;
                time.Text = "Время закончиться через:" + sec;
            }
        }
        //Проверка номера
        private void Nomer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Sotrudnik sotrudnik = Class.ClassBase.DB.Sotrudnik.FirstOrDefault(x => x.NomerTelefon == Nomer.Text);
            
                if(sotrudnik!=null)
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
        //Создание кода
        public void KOD()
        {
            otpravka.IsEnabled=false;
            kod.IsEnabled = true;
            Vhod.IsEnabled = true;
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
              
                disTimer.Start();
            }

        }
        //Проверка пароля 
        private void Password_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Sotrudnik sotrudnik = Class.ClassBase.DB.Sotrudnik.FirstOrDefault(x => x.NomerTelefon == Nomer.Text &&x.Password==Password.Text);
                if (sotrudnik!=null)
                {
                    
                    KOD();
                   
                }
                else
                {
                    MessageBox.Show("Пароль не правильный.");
                }
            }
        }
        //Обновление кода
        private void otpravka_Click(object sender, RoutedEventArgs e)
        {
            time.Text = "Время закончиться через: 10";
            text = "";
            kod.Text = "";
            KOD();

        }
        //Вход
        private void Vhod_Click(object sender, RoutedEventArgs e)
        {
            if(kod.Text ==text)
                {
                disTimer.Stop();
                sec = 10;
                time.Visibility = Visibility.Collapsed;
                MessageBox.Show("Вы зашли");
            }
            else
            {
                MessageBox.Show("Вы неверно ввели код");
                disTimer.Stop();
                sec = 10;
                time.Text = "Время закончиться через: 10";
                text = "";
                kod.Text = "";
                KOD();
            }
        }

        //Удаление данных из TextBlock
        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            disTimer.Stop();
            time.Text = "Время закончиться через: 10";
            sec = 10;
            time.Visibility = Visibility.Collapsed;
            kod.IsEnabled=false;
            kod.Text = "";
            Password.IsEnabled = false;
            otpravka.IsEnabled = false;
            Vhod.IsEnabled = false;
            Password.Text = "";
            Nomer.Text = "";
        }
    }
}
