using System;
using System.Collections.Generic;
using System.Linq;

namespace Ngx.Monorepo.Framework.Core.Utility
{
    public class MicroservicesHelper
    {
        public List<Microservice> Microservices { get; set; }
        public string GetPathByName(string microserviceName, string pathName)
        {
            var ms = Microservices?
                .FirstOrDefault(x => x.Name.Equals(microserviceName, StringComparison.OrdinalIgnoreCase));
            var path = ms.Paths.FirstOrDefault(x => x.Name.Equals(pathName, StringComparison.OrdinalIgnoreCase)).Path;
            return $"{ms.BasePath}/{path}";
        }
    }

    public class Microservice
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string BasePath { get; set; }
        public IList<MicroservicePath> Paths { get; set; }
    }

    public class MicroservicePath
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
