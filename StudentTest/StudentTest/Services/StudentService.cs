using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using StudentTest.Models;

namespace StudentTest.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRestClient _restClient;

        public StudentService()
        {
            _restClient = RestService.For<IRestClient>("http://etestapi.test.eminenttechnology.com");
        }
        public Task<Student> Add(Student student)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Student>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
