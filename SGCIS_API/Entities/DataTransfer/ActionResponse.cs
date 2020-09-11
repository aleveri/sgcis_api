using Newtonsoft.Json.Linq;

namespace SGCIS_API.Entities.DataTransfer
{
    public class ActionResponse
    {
        public bool Error { get; set; }
        public int Code { get; set; }
        public dynamic Result { get; set; }
        public string Message { get; set; }
    }
}
