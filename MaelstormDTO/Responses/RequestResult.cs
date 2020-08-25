using System.Collections.Generic;

namespace MaelstormDTO.Responses
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