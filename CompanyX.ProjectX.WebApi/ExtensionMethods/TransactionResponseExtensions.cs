using CompanyX.ProjectX.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyX.ProjectX.WebApi.ExtensionMethods
{
    internal static class TransactionResponseExtensions
    {
        public static ProblemDetails GetProblemDetails(this TransactionResponse response)
        {
            return new ProblemDetails
            {
                Title = response.Status.ToString(),
                Detail = response.StatusMessage
            };
        }
    }
}
