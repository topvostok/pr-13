using Interface_Пешин.Models;
using System.Collections.Generic;

namespace Interface_Пешин.Interfaces
{
    public interface IUsers
    {
        void All(out List<Users> users);
    }
}