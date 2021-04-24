using CompanyX.ProjectX.Domain;
using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Services;
using System.Threading.Tasks;
using Xunit;

namespace CompanyX.ProjectX.Tests.Unit
{
    public class ValueServiceTests
    {
        [Fact]
        public async Task ExpectedValueIsReturnedAsync()
        {
            Value testInput = new() { Text = "testing!" };

            Value result = await CreateService().GetValueAsync(testInput);

            Assert.Equal(testInput, result);
        }

        private static IValueService CreateService()
        {
            return new ValueService();
        }
    }
}
