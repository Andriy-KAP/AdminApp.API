using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace CallCenter.API.CasheServices
{
    public class CacheableEntity
    {
        public string ResourceKey { get; private set; }
        public EntityTagHeaderValue EntityTag { get; set; }
        public DateTimeOffset LastModified { get; set; }

        public CacheableEntity(string resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public bool IsValid(DateTimeOffset modifiedSince)
        {
            var lastModified = LastModified.UtcDateTime;
            return (lastModified.AddSeconds(-1) < modifiedSince.UtcDateTime);
        }
    }
}