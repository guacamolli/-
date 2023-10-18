using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace АвторизацияБД
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private User _currentUser = new User();
        avtorizationEntities database = new avtorizationEntities();
        public Registration(User selectedUser)
        {
            InitializeComponent();

            if (selectedUser != null)
                _currentUser = selectedUser;

            DataContext = _currentUser;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUser.mail))
                errors.AppendLine("Укажите почту");
            if (string.IsNullOrWhiteSpace(_currentUser.login))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrWhiteSpace(_currentUser.pass))
                errors.AppendLine("Укажите пароль");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (database.User.Any(u => u.login == tb_reglogin.Text))
            {
                MessageBox.Show("Данный логин уже используется");
            }
            else
            {
                _currentUser.idType = 1;

                if (_currentUser.id == 0)
                    avtorizationEntities.GetContext().User.Add(_currentUser);

                try
                {
                    avtorizationEntities.GetContext().SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрировались!");

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    Hide();
                    mainWindow.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }   
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Hide();
            mainWindow.Show();
        }
    }
}
