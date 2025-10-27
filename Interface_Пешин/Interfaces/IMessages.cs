using Interface_Пешин.Models;
using System.Collections.Generic;

namespace Interface_Пешин.Interfaces
{
    public interface IMessages
    {
        void All(out List<Messages> messages);
        void Save(bool update);
        void Delete();
    }
}