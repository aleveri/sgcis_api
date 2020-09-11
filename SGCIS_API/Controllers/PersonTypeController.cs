using Microsoft.AspNetCore.Mvc;
using SGCIS_API.Entities.Business;
using SGCIS_API.Entities.DataTransfer;
using SGCIS_API.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SGCIS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonTypeController : ControllerBase
    {
        private readonly IPersonTypeBlobService _personTypeBlobService;

        public PersonTypeController(IPersonTypeBlobService personTypeBlobService) => _personTypeBlobService = personTypeBlobService;

        [HttpGet]
        public async Task<ActionResponse> GetPersonTypes()
        {
            try
            {
                return new ActionResponse()
                {
                    Code = (int)HttpStatusCode.OK,
                    Result = await _personTypeBlobService.GetList()
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse()
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Error = true,
                    Message = $"Message: {ex.Message} - {ex.InnerException}"
                };
            }
        }

        [HttpPost]
        public async Task<ActionResponse> CreatePersonType([FromBody] PersonType personType)
        {
            try
            {
                if (personType.Type < 0 || string.IsNullOrWhiteSpace(personType.Description))
                    return new ActionResponse()
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Error = true,
                        Message = "PersonType invalid data"
                    };

                bool sucess = await _personTypeBlobService.AddPersonType(personType);

                return new ActionResponse()
                {
                    Code = (int)HttpStatusCode.OK,
                    Result = personType
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse()
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Error = true,
                    Message = $"Message: {ex.Message} - {ex.InnerException}"
                };
            }
        }
    }
}
