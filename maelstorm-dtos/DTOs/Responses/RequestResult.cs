using System.Collections.Generic;

namespace maelstorm_dtos.DTOs.Responses
{
    public class RequestResult<T>
    {
        public bool Ok { get; set; }
        public List<string> ErrorMessages { get; set; }
        public T Data { get; set; }

        public RequestResult()
        {
            ErrorMessages = new List<string>();
        }
    }
}