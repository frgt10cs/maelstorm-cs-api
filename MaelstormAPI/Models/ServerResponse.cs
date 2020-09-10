using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaelstormApi.Models
{
    public class ServerResponse
    {
        public bool Ok
        {
            get { return ProblemDetails == null; }
        }

        public ProblemDetails ProblemDetails { get; internal set; }
        
        internal string Content;
        private object DeserializedContent;
        
        public T GetContent<T>()
        {
            if (DeserializedContent == null)
                DeserializedContent = JsonConvert.DeserializeObject<T>(Content);
            return (T)DeserializedContent;
        }
    }
}