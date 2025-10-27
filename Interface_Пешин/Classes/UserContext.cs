using Interface_Пешин.Interfaces;
using Interface_Пешин.Models;
using System.Collections.Generic;

namespace Interface_Пешин.Classes
{
    public class UsersContext : Users, IUsers
    {
        /// <summary> Список всех пользователей </summary>
        public List<Users> AllUsers;

        /// <summary> Конструктор </summary>
        public UsersContext()
        {
            // Получаем список всех пользователей
            this.All(out AllUsers);
        }

        /// <summary> Метод получения пользователей </summary>
        public void All(out List<Users> users)
        {
            // Инициализируем список
            users = new List<Users>();
            // Вносим данные
            users.Add(new Users(1, "Аликина Ольга"));
            users.Add(new Users(2, "Бояркин Данил"));
            users.Add(new Users(3, "Бурмантов Владислав"));
            users.Add(new Users(4, "Дылдин Максим"));
            users.Add(new Users(5, "Евдокимов Даниил"));
            users.Add(new Users(6, "Костюнин Никита"));
            users.Add(new Users(7, "Кучин Данил"));
            users.Add(new Users(8, "Мотырев Александр"));
            users.Add(new Users(10, "Мухридинов Далер"));
            users.Add(new Users(11, "Олейник Владимир"));
            users.Add(new Users(12, "Саблин Константин"));
            users.Add(new Users(13, "Субботин Валерий"));
            users.Add(new Users(14, "Сукрушев Егор"));
            users.Add(new Users(15, "Торсунов Даниил"));
            users.Add(new Users(16, "Хабибрахманов Никита"));
            users.Add(new Users(17, "Хикматулин Григорий"));
            users.Add(new Users(18, "Черенев Сергей"));
            users.Add(new Users(19, "Чупин Дмитрий"));
            users.Add(new Users(20, "Шилов Дмитрий"));

            // Сохраняем также в поле AllUsers
            AllUsers = users;
        }
    }
}