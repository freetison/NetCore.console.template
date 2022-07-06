using System.Collections.Generic;

namespace ncapp.DependencyInjection
{
    public class HttpServiceOptions
    {
        public string BaseUrl { get; set; }

        public IEnumerable<string> ContentTypes { get; set; } = new List<string>()
        {
            "application/json",
            "text/json",
            "text/x-json"
        };

    }
}
