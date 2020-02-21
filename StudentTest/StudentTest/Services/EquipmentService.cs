using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using StudentTest.Models;

namespace StudentTest.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRestClient _restClient;

        public EquipmentService()
        {
            _restClient = RestService.For<IRestClient>("http://etestapi.test.eminenttechnology.com");
        }

        public async Task<Equipment> Add(Equipment equipment)
        {
            try
            {
                var equipments = await _restClient.AddEquipment(equipment);
                return equipments;
            }
            catch (ValidationApiException validationException)
            {
                Console.WriteLine("m-validation error " + validationException.Message);
                return new Equipment();
            }
            catch (ApiException exception)
            {
                Console.WriteLine("m-exception error " + exception.Message);
                return new Equipment();
            }
        }

        public async void Delete(string id)
        {
            try
            {
                var equipments = await _restClient.DeleteEquipment(id);
            }
            catch (ValidationApiException validationException)
            {
                Console.WriteLine("m-validation error " + validationException.Message);
            }
            catch (ApiException exception)
            {
                Console.WriteLine("m-exception error " + exception.Message);
            }
        }

        public async Task<List<Equipment>> GetAll()
        {
            try
            {
                var equipments = await _restClient.GetEquipments();
                return equipments;
            }
            catch (ValidationApiException validationException)
            {
                Console.WriteLine("m-validation error " + validationException.Message);
                return new List<Equipment>();
            }
            catch (ApiException exception)
            {
                Console.WriteLine("m-exception error " + exception.Message);
                return new List<Equipment>();
            }
           
        }

        public Task<Equipment> GetEquipmentById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Equipment>> Search(string name, int status, int type)
        {
            try
            {
                var equipments = await _restClient.SearchEquipments(name, status, type);
                return equipments;
            }
            catch (ValidationApiException validationException)
            {
                Console.WriteLine("S-validation error " + validationException.Message);
                return new List<Equipment>();
            }
            catch (ApiException exception)
            {
                Console.WriteLine("S-exception error " + exception.Message);
                return new List<Equipment>();
            }
            
        }

        public async Task<Equipment> Update(Equipment equipment)
        {
            try
            {
                var equipments = await _restClient.UpdateEquipment(equipment.Id, equipment);
                return equipments;
            }
            catch (ValidationApiException validationException)
            {
                Console.WriteLine("m-validation error " + validationException.Message);
                return new Equipment();
            }
            catch (ApiException exception)
            {
                Console.WriteLine("m-exception error " + exception.Message);
                return new Equipment();
            }
        }
    }
}
