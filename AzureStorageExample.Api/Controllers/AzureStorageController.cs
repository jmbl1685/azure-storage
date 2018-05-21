using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzureStorageExample.Api.Controllers
{
    [RoutePrefix("api")]
    public class AzureStorageController : ApiController
    {
        [HttpPost, Route("upload")]
        public IHttpActionResult UploadFile() => Ok(new { message = "Ok" });
    }
}
