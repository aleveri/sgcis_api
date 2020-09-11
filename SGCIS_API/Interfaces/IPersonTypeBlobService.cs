using SGCIS_API.Entities.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGCIS_API.Interfaces
{
    public interface IPersonTypeBlobService
    {
        Task<bool> AddPersonType(PersonType entity);
        Task<IEnumerable<PersonType>> GetList();
        Task<bool> TypeExists(int type);
    }
}
