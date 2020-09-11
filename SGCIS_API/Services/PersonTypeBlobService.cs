using SGCIS_API.Entities.Business;
using SGCIS_API.Enumerations;
using SGCIS_API.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGCIS_API.Services
{
    public class PersonTypeBlobService : BaseBlobService<PersonType>, IPersonTypeBlobService
    {
        public PersonTypeBlobService() => _containerClient = _blobServiceClient.GetBlobContainerClient(ServicesEnum.ConfigurationContainer);

        public async Task<IEnumerable<PersonType>> GetList()
        {
            ICollection<PersonType> res = await GetList(ServicesEnum.PersonTypeBlob);
            return res;
        }

        public async Task<bool> TypeExists(int type) => (await GetList()).Any(pt => pt.Type == type);

        public async Task<bool> AddPersonType(PersonType entity)
        {
            ICollection<PersonType> personsType = (await GetList()).ToList();

            if (personsType.Any(pt => pt.Type == entity.Type))
                return false;
            else
                personsType.Add(entity);

            await AddList(ServicesEnum.PersonTypeBlob, personsType);
            return true;
        }
    }
}
