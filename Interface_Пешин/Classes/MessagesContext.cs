using Interface_Пешин.Interfaces;
using Interface_Пешин.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Interface_Пешин.Classes
{
    public class MessagesContext : Messages, IMessages
    {
        private static string DataFilePath = "messages.xml";
        public static List<MessagesContext> AllMessages = new List<MessagesContext>();

        public MessagesContext() { }

        public MessagesContext(string message, DateTime create, int idUser, string imagePath = null)
            : base(message, create, idUser, imagePath) { }

        public void All(out List<Models.Messages> messages)
        {
            LoadMessages();
            messages = AllMessages.Cast<Models.Messages>().ToList();
        }

        public void Save(bool update = false)
        {
            if (!update)
            {
                this.Id = AllMessages.Count > 0 ? AllMessages.Max(m => m.Id) + 1 : 1;
                AllMessages.Add(this);
            }
            SaveMessages();
        }

        public void Delete()
        {
            var messageToDelete = AllMessages.FirstOrDefault(m => m.Id == this.Id);
            if (messageToDelete != null)
            {
                // Удаляем файл изображения если он существует
                if (!string.IsNullOrEmpty(messageToDelete.ImagePath) && File.Exists(messageToDelete.ImagePath))
                {
                    File.Delete(messageToDelete.ImagePath);
                }
                AllMessages.Remove(messageToDelete);
                SaveMessages();
            }
        }

        private void LoadMessages()
        {
            if (File.Exists(DataFilePath))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(List<MessagesContext>));
                    using (var reader = new StreamReader(DataFilePath))
                    {
                        AllMessages = (List<MessagesContext>)serializer.Deserialize(reader);
                    }
                }
                catch
                {
                    AllMessages = new List<MessagesContext>();
                }
            }
            else
            {
                AllMessages = new List<MessagesContext>();
            }
        }

        private void SaveMessages()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<MessagesContext>));
                using (var writer = new StreamWriter(DataFilePath))
                {
                    serializer.Serialize(writer, AllMessages);
                }
            }
            catch { }
        }

        public string SaveImage(string sourceImagePath)
        {
            try
            {
                string imagesFolder = "MessageImages";
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                string fileName = $"img_{DateTime.Now:yyyyMMddHHmmssfff}_{Path.GetFileName(sourceImagePath)}";
                string destinationPath = Path.Combine(imagesFolder, fileName);

                File.Copy(sourceImagePath, destinationPath, true);
                return destinationPath;
            }
            catch
            {
                return null;
            }
        }
    }
}