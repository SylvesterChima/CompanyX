using StudentTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentTest.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAll();
        Task<Student> Add(Student student);
        Task<Student> GetStudentById(string id);
        Task<Student> Update(Student student);
        void Delete(string id);
    }
}
