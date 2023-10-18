using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;


namespace АвторизацияБД
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        static int counter = 0;
        static int remainedtry = 4;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (tb_login.Text.Count()==0  )
            {
                lb_taskbar.Text = ("Введите логин!");
                return;
            }
            else if  (tb_pass.Password.Count() == 0)
            {
                lb_taskbar.Text = ("Введите пароль!");
                return;
            }
            var context = new avtorizationEntities();
            var name = context.User.FirstOrDefault(i => i.login == tb_login.Text && i.pass == tb_pass.Password);
            if (name != null)
            {
                lb_taskbar.Text = ("Успешная авторизация!");
                Successful_enter successful_wind = new Successful_enter();
                successful_wind.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                successful_wind.Show();
            }
            else
            {
                remainedtry--;
                if (remainedtry >= 2)
                {
                    lb_taskbar.Text = ($"Неверные данные!\nОсталось {remainedtry} попытки.");
                }
                else
                {
                    lb_taskbar.Text = ($"Неверные данные!\nОсталась {remainedtry} попытка!");
                }
                if (counter == 3)
                {
                    lb_taskbar.Text = ("Слишком много неверных попыток!\nАвторизация заблокирована!");
                    tb_login.IsEnabled = false;
                    tb_pass.IsEnabled = false;
                    but_enter.IsEnabled = false;
                }
                counter++;
                
                return;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Registration registwin = new Registration(null);
            registwin.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Hide();
            registwin.Show();
        }
    }
}
