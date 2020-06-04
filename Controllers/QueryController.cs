using System;
using System.Collections.Generic;
using System.Linq;
using Couchbase.Extensions.Caching;
using DistributedCaching.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace DistributedCaching.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryController : ControllerBase
    {
        private IDistributedCache cache;
        public QueryController(IDistributedCache cache)
        {
            this.cache = cache;
        }

        [HttpGet]
        public IEnumerable<Blob> Get()
        {
            Blob[] blobs;
            // check if blob list is already in cache
            blobs = cache.Get<Blob[]>("cachedQuery1");
            if (blobs == null)
            {
                // we couldn't find these blobs, so let's create new ones 
                var rng = new Random();
                blobs = Enumerable.Range(1, 100).Select(index => new Blob
                {
                    Id = index,
                    Data = string.Format("{0} | DATA, DATA, DATA, | {1}", DateTimeOffset.Now.ToString(), index)
                }).ToArray();

                // here we just simulate that we're working hard! :)
                System.Threading.Thread.Sleep(5000);

                // put these blobs into the cache and keep them there for up to five minutes
                cache.Set<Blob[]>("cachedQuery1", blobs, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) });   
            }
            return blobs;
        }
    }
}
