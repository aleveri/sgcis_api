using Azure;
using Azure.Storage.Blobs.Models;
using SGCIS_API.Entities.Business;
using SGCIS_API.Enumerations;
using SGCIS_API.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGCIS_API.Services
{
    public class PersonBlobService : BaseBlobService<Person>, IPersonBlobService
    {
        private readonly ICollection<Person> _persons;

        public PersonBlobService() => _containerClient = _blobServiceClient.GetBlobContainerClient(ServicesEnum.PersonsContainer);

        public async Task<IList> GetList()
        {
            IList persons = new List<Person>();
            Pageable<BlobItem> blobs = _containerClient.GetBlobs();

            foreach (BlobItem blob in blobs)
                persons.Add(await Get(blob.Name));

            return persons;
        }

        public async Task DeletePerson(string id) => await Delete(id);

        public async Task AddPerson(Person person) => await Add(person.Id.ToString(), person);

        public async Task<bool> UpdatePerson(Person person)
        {
            try
            {
                await Add(person.Id.ToString(), person);
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}
