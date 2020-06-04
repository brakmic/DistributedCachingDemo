# DistributedCachingDemo


### Prerequisites

You will need a running [Couchbase](https://www.couchbase.com/) instance (or any other database that supports .NET Core's [IDistributedCache](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.caching.distributed.idistributedcache?view=dotnet-plat-ext-3.1) interface)

Create a `bucket` in Couchbase and a user with access to it. Then add this data to [Setup.cs](https://github.com/brakmic/DistributedCachingDemo/blob/master/Startup.cs#L31) of this project.

