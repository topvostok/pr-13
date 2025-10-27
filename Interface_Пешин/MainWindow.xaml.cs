using System.Windows;
using System.Windows.Input;
using Interface_Пешин.Models;
using Interface_Пешин.Elements;
using Interface_Пешин.Classes;
using System.Windows.Controls;
using System;
using System.Linq;
using Microsoft.Win32;

namespace Interface_Пешин
{
    public partial class MainWindow : Window
    {
        public UsersContext usersContext = new UsersContext();
        public MessagesContext messagesContext = new MessagesContext();
        public int IdSelectUser = -1;
        public static MainWindow mainWindow;

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            LoadUsers();
        }

        public void LoadUsers()
        {
            ParentUser.Children.Clear();
            foreach (Models.Users user in usersContext.AllUsers)
            {
                ParentUser.Children.Add(new Elements.Users(user));
            }
        }

        public void SelectUser(Models.Users user)
        {
            if (user != null)
            {
                IdSelectUser = user.Id;
                SelectedUserName.Text = user.FIO;
                parentMessage.Children.Clear();
                LoadMessagesForUser(IdSelectUser);
                BlockMessage.IsEnabled = true;
                MessageText.Focus();
            }
        }

        private void LoadMessagesForUser(int userId)
        {
            parentMessage.Children.Clear();
            messagesContext.All(out var allMessages);
            var userMessages = allMessages.Where(m => m.IdUser == userId).ToList();

            foreach (var message in userMessages)
            {
                var messageContext = new MessagesContext(message.Message, message.Create, message.IdUser, message.ImagePath)
                {
                    Id = message.Id
                };
                parentMessage.Children.Add(new Elements.Messages(messageContext));
            }

            ScrollToBottom();
        }

        private void SendMessage(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendTextMessage();
                e.Handled = true;
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendTextMessage();
        }

        private void SendTextMessage()
        {
            if (IdSelectUser == -1)
            {
                MessageBox.Show("Сначала выберите пользователя!");
                return;
            }

            string messageText = MessageText.Text.Trim();
            if (!string.IsNullOrEmpty(messageText))
            {
                MessagesContext newMessage = new MessagesContext(messageText, DateTime.Now, IdSelectUser);
                newMessage.Save();
                parentMessage.Children.Add(new Elements.Messages(newMessage));
                MessageText.Text = "";
                ScrollToBottom();
            }
        }

        private void SendImage_Click(object sender, RoutedEventArgs e)
        {
            if (IdSelectUser == -1)
            {
                MessageBox.Show("Сначала выберите пользователя!");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp",
                Title = "Выберите изображение"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string savedImagePath = messagesContext.SaveImage(openFileDialog.FileName);
                    if (!string.IsNullOrEmpty(savedImagePath))
                    {
                        MessagesContext newMessage = new MessagesContext("", DateTime.Now, IdSelectUser, savedImagePath);
                        newMessage.Save();
                        parentMessage.Children.Add(new Elements.Messages(newMessage));
                        ScrollToBottom();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при отправке изображения: {ex.Message}");
                }
            }
        }

        private void ScrollToBottom()
        {
            MessagesScrollViewer.ScrollToEnd();
        }
    }
}