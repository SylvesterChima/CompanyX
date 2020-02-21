using Refit;
using StudentTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentTest.Services
{
    public interface IRestClient
    {
        //Equpments
        [Get("/api/Equipment")]
        Task<List<Equipment>> GetEquipments();

        [Post("/api/Equipment")]
        Task<Equipment> AddEquipment([Body] Equipment equipment);

        [Get("/api/Equipment/search")]
        Task<List<Equipment>> SearchEquipments(string name, int status, int type);

        [Put("/api/Equipment/{id}")]
        Task<Equipment> UpdateEquipment(string id, [Body] Equipment equipment);

        [Delete("/api/Equipment/{id}")]
        Task<Equipment> DeleteEquipment(string id);


        //Students
        [Get("/api/Student")]
        Task<List<Student>> GetStudents();

        [Post("/api/Student")]
        Task<Student> AddStudent([Body] Student student);

        [Put("/api/Student/{id}")]
        Task<Student> UpdateStudent(string id, [Body] Student student);

        [Delete("/api/Student/{id}")]
        Task<Student> DeleteStudent(string id);
    }
}
