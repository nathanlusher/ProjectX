using CompanyX.ProjectX.Domain;
using CompanyX.ProjectX.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.WebApi.Controllers
{
    /// <summary>
    /// Controller for returning a string value.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ValueController : Controller
    {
        private readonly IValueService _service;

        /// <summary>
        /// Initialises the controller with the supplied parameters.
        /// </summary>
        /// <param name="service">The <see cref="IValueService"/> to use.</param>
        public ValueController(IValueService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns a string value representing the string value passed as a parameter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        "value": "some-string",
        ///     }
        ///
        /// </remarks>
        /// <param name="input">The input value.</param>
        /// <returns>A value based on the supplied input.</returns>
        /// <response code="202">Value accepted.</response>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPost]
        public async Task<ActionResult<Value>> GetValue(Value input)
        {
            return Accepted(await _service.GetValueAsync(input));
        }
    }
}
