namespace Interface_Пешин.Models
{
    public class Users
    {
        /// <summary> Код </summary>
        public int Id { get; set; }

        /// <summary> ФИО Пользователя </summary>
        public string FIO { get; set; }

        /// <summary> Пустой конструктор </summary>
        public Users() { }

        /// <summary> Конструктор для заполнения данных </summary>
        public Users(int id, string fio)
        {
            Id = id;
            FIO = fio;
        }
    }
}