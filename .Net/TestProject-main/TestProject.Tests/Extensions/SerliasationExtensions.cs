
using System.Net.Http.Json;

namespace TestProject.Tests.Extensions
{
    public static class SerliasationExtensions
    {
        public static JsonContent ToJsonContent(this object objecToSerialize) => JsonContent.Create(objecToSerialize);
    }
}
