using System.Collections.Generic;

namespace MaelstormApi.Models
{
    public class ProblemDetails
    {
        public string Detail { get; set; }
        public IDictionary<string, object> Extensions { get; }
        public string Instance { get; set; }
        public int? Status { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public ProblemDetails()
        {
                
        }

        public ProblemDetails(string detail)
        {
            Detail = detail;
        }
    }
}