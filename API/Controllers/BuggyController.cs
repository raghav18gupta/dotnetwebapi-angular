using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext storeContext;

        public BuggyController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        [HttpGet("notFound")]
        public ActionResult MyNotFound()
        {
            var thing = storeContext.Products.Find(2);
            if (thing == null)
                return NotFound(new ApiResponse(404));
            return Ok(thing);
        }

        [HttpGet("serverError")]
        public ActionResult ServerError()
        {
            var thing = storeContext.Products.Find(1000);
            var thingToReturn = thing.ToString();
            return Ok(thing);
        }

        [HttpGet("badRequest")]
        public ActionResult MyBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badRequest/{id}")]
        public ActionResult MyBadRequest(int id)
        {
            return BadRequest();
        }
    }
}
