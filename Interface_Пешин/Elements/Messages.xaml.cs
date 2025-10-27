using Interface_Пешин.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Interface_Пешин.Elements
{
    public partial class Messages : UserControl
    {
        public MessagesContext ThisMessage;

        public Messages(MessagesContext message)
        {
            InitializeComponent();
            ThisMessage = message;
            UpdateUI();
        }

        private void UpdateUI()
        {
            MessageText.Text = ThisMessage.Message;
            MessageDate.Text = ThisMessage.Create.ToString("dd.MM.yyyy HH:mm");

            // Отображаем изображение если есть
            if (ThisMessage.HasImage && System.IO.File.Exists(ThisMessage.ImagePath))
            {
                try
                {
                    var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new System.Uri(ThisMessage.ImagePath, System.UriKind.RelativeOrAbsolute);
                    bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    MessageImage.Source = bitmap;
                    MessageImage.Visibility = Visibility.Visible;
                }
                catch
                {
                    MessageImage.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageImage.Visibility = Visibility.Collapsed;
            }
        }

        private void DeleteMessage(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) // Двойной клик
            {
                var result = MessageBox.Show("Удалить сообщение?", "Подтверждение",
                                           MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ThisMessage.Delete();
                    if (this.Parent is Panel parentPanel)
                    {
                        parentPanel.Children.Remove(this);
                    }
                }
            }
        }
    }
}