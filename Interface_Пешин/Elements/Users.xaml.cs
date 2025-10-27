using System.Windows.Controls;
using Interface_Пешин.Models;
using System.Windows.Input;

namespace Interface_Пешин.Elements
{
    public partial class Users : UserControl
    {
        public Models.Users ThisUser;

        public Users(Models.Users user)
        {
            InitializeComponent();
            ThisUser = user;
            FIO.Text = user.FIO;
        }

        private void SelectUser(object sender, MouseButtonEventArgs e)
        {
            // Вызываем метод выбора пользователя на главном окне
            MainWindow.mainWindow.SelectUser(ThisUser);
        }
    }
}