using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Domain.Models;
using CompanyX.ProjectX.WebApi.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.WebApi.Controllers
{
    /// <summary>
    /// Controller for processing transactions.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TransactionController : Controller
    {
        private readonly ITransactionProcessor _processor;
        private readonly ITransactionReader _reader;

        /// <summary>
        /// Initialises the controller with the supplied parameters.
        /// </summary>
        /// <param name="processor">The processor with which to process transactions.</param>
        /// <param name="reader">The reader with which to read transactions.</param>
        public TransactionController(ITransactionProcessor processor, ITransactionReader reader)
        {
            _processor = processor;
            _reader = reader;
        }

        /// <summary>
        /// Retrieves a transaction by its id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /transaction?id=123456
        ///
        /// </remarks>
        /// <param name="id">The id of the transaction to retrieve.</param>
        /// <returns>The request transaction, if found.</returns>
        /// <response code="200">Transaction was found.</response>
        /// <response code="404">Transaction was not found.</response>
        /// <response code="500">An error occurred during the retrieval of the transaction.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<Transaction>> GetTransactionById(string id)
        {
            Transaction result = await _reader.GetTransactionAsync(id);

            if (result == null)
            {
                return NotFound(id);
            }

            return result;
        }

        /// <summary>
        /// Processes a transaction request.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /transaction
        ///     {
        ///       "shopper": {
        ///         "contact": {
        ///           "firstName": "John",
        ///           "lastName": "Smith",
        ///           "addressLine1": "Sunshine Cottage",
        ///           "addressLine2": "4, Long Road",
        ///           "postcode": "EN33 4EE",
        ///           "county": "Somewhere",
        ///           "country": "Anywhere",
        ///           "email": "someone@somewhere.com"
        ///         },
        ///         "card": {
        ///           "type": "MasterCard",
        ///           "number": "1111-1111-1111-1111",
        ///           "securityCode": 234,
        ///           "name": "MR JOHN SMITH",
        ///           "expiryMonth": 10,
        ///           "expiryYear": 2022,
        ///           "issueNumber": 0
        ///         }
        ///       },
        ///       "merchant": {
        ///         "contact": {
        ///           "companyName": "Burt's Books",
        ///           "addressLine1": "Flat 13",
        ///           "addressLine2": "5, Short Road",
        ///           "postcode": "EN33 4FF",
        ///           "county": "Somewhere",
        ///           "country": "Anywhere",
        ///           "email": "burt@burtsbooks.co.uk"
        ///         },
        ///         "account": {
        ///           "name": "BURTS BOOKS",
        ///           "number": "12345678",
        ///           "sortCode": "12-34-56"
        ///         }
        ///       },
        ///       "payment": {
        ///         "currency": {
        ///           "name": "GBP",
        ///           "symbol": "£"
        ///         },
        ///         "value": 4.95
        ///       },
        ///       "date": "2021-04-30T12:40:11.836Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">The transaction request to process.</param>
        /// <returns>The result of the transaction request.</returns>
        /// <response code="200">Transaction processed.</response>
        /// <response code="400">Transaction request is invalid.</response>
        /// <response code="500">An error occurred during the processing of the transaction.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<TransactionResponse>> ProcessTransactionRequest(TransactionRequest request)
        {
            TransactionResponse result = await _processor.ProcessTransactionAsync(request);

            if (result.Status == TransactionStatus.Successful)
            {
                return result;
            }

            return BadRequest(result.GetProblemDetails());
        }
    }
}
