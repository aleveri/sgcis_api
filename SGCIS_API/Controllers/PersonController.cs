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
    public class PersonController : ControllerBase
    {
        private readonly IPersonBlobService _personBlobService;
        private readonly IPersonTypeBlobService _personTypeBlobService;

        public PersonController(IPersonBlobService personBlobService, IPersonTypeBlobService personTypeBlobService)
        {
            _personBlobService = personBlobService;
            _personTypeBlobService = personTypeBlobService;
        }

        [HttpGet]
        public async Task<ActionResponse> GetPersons()
        {
            try
            {
                return new ActionResponse()
                {
                    Code = (int)HttpStatusCode.OK,
                    Result = await _personBlobService.GetList()
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

        [HttpDelete]
        public async Task<ActionResponse> DeletePerson(int id)
        {
            try
            {
                if (id < 0)
                    return new ActionResponse()
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Error = true,
                        Message = "id doesn't exist"
                    };

                await _personBlobService.DeletePerson(id.ToString());

                return new ActionResponse()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"The person with id: {id} was deleted"
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
        public async Task<ActionResponse> CreatePerson([FromBody] Person person)
        {
            try
            {
                if (person.Id < 0 || person.Age < 0 || string.IsNullOrWhiteSpace(person.Name) || person.PersonType?.Type == null)
                    return new ActionResponse()
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Error = true,
                        Message = "Person invalid data"
                    };

                if (!(await _personTypeBlobService.TypeExists(person.PersonType.Type)))
                    return new ActionResponse()
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Error = true,
                        Message = "PersonType invalid"
                    };

                await _personBlobService.AddPerson(person);

                return new ActionResponse()
                {
                    Code = (int)HttpStatusCode.OK,
                    Result = person
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

        [HttpPut]
        public async Task<ActionResponse> UpdatePerson([FromBody] Person person)
        {
            try
            {
                if (person.Id < 0 || person.Age < 0 || string.IsNullOrWhiteSpace(person.Name) || person.PersonType?.Type == null)
                    return new ActionResponse()
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Error = true,
                        Message = "Person invalid data"
                    };

                if (await _personBlobService.UpdatePerson(person))
                {
                    return new ActionResponse()
                    {
                        Code = (int)HttpStatusCode.OK,
                        Result = person
                    };
                }
                else
                {
                    return new ActionResponse()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Error = true
                    };
                }
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