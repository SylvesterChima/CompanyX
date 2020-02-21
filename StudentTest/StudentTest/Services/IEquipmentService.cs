using StudentTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentTest.Services
{
    public interface IEquipmentService
    {
        Task<List<Equipment>> GetAll();
        Task<Equipment> Add(Equipment equipment); 
        Task<List<Equipment>> Search(string name, int status, int type);
        Task<Equipment> GetEquipmentById(string id);
        Task<Equipment> Update(Equipment equipment);
        void Delete(string id);
    }
}
