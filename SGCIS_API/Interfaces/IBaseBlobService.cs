using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGCIS_API.Interfaces
{
    public interface IBaseBlobService<T>
    {
        Task Add(string id, T model);
        Task<T> Get(string id);
        Task Delete(string id);
        Task<ICollection<T>> GetList(string blobName);
    }
}
