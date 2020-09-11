using SGCIS_API.Entities.Business;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGCIS_API.Interfaces
{
    public interface IPersonBlobService
    {
        Task<IList> GetList();
        Task AddPerson(Person person);
        Task DeletePerson(string id);
        Task<bool> UpdatePerson(Person person);
    }
}
