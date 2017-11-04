using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AuthAPI.Domain.Extensions;
using AuthAPI.Domain.ValueObjects;

namespace AuthAPI.WebApi.Controllers
{
    public abstract class AbstractController : ApiController
    {
        protected IHttpActionResult ProcessResult(MethodResult methodResult)
        {
            if (methodResult.Success)
            {
                if (methodResult.Data != null)
                    return Ok(methodResult.Data);

                return Ok();
            }

            var errorMessages = methodResult.Failures.Select(x => (x.PropertyName.IsEmpty() ? "" : ": " + x.PropertyName) + x.ErrorMessage);
            var messages = string.Join("<br>", errorMessages);

            return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = messages });
        }
    }
}