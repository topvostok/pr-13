using System;

namespace Interface_Пешин.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Create { get; set; }
        public int IdUser { get; set; }
        public string ImagePath { get; set; } // Путь к изображению
        public bool HasImage => !string.IsNullOrEmpty(ImagePath);

        public Messages() { }

        public Messages(string message, DateTime create, int idUser, string imagePath = null)
        {
            Message = message;
            Create = create;
            IdUser = idUser;
            ImagePath = imagePath;
        }
    }
}