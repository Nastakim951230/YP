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
        public Avtorizatsia()
        {
            InitializeComponent();
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

        private void Password_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (password == Password.Text)
                {
                    kod.IsEnabled = true;
                    otpravka.IsEnabled = true;
                    kod.Focus();
                    Random random = new Random();
                    text = String.Empty;
                    string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm/*-+., ";
                    for (int i = 0; i < 8; ++i)
                        text += ALF[random.Next(ALF.Length)];
                   
                }
                else
                {
                    MessageBox.Show("Пароль не правильный.");
                }
            }
        }
    }
}
