using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MPI_Report.Infrastructure
{
    public static class JsonApiResult
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            NullValueHandling = NullValueHandling.Include,
            ContractResolver = new DefaultContractResolver()
        };

        public static ContentResult Ok(object data)
        {
            return Content(new
            {
                success = true,
                data,
                errors = new string[0]
            });
        }

        public static ContentResult Fail(IList<string> errors, object data = null)
        {
            return Content(new
            {
                success = false,
                data,
                errors = errors ?? new List<string>()
            });
        }

        public static ContentResult Fail(string error, object data = null)
        {
            return Fail(new[] { error }, data);
        }

        private static ContentResult Content(object payload)
        {
            return new ContentResult
            {
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(payload, Settings)
            };
        }
    }
}
