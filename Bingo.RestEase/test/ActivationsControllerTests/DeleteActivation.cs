using System.Net;
using System.Threading.Tasks;
using Bingo.RestEase.Models.Response;
using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using Xunit;

namespace Bingo.RestEase.Test.ActivationsControllerTests
{
    [Trait("Activations", "Delete Activation")]
    public class DeleteActivation: TestBase
    {
        private readonly TestContext _context;
        private readonly Activation _activationEntity;

        public DeleteActivation(TestContext context)
        {
            _context = context;

            // Test Setup
            _activationEntity = Activations.RandomContractActivation;
            _context.ActivationsCollection.InsertOne(_activationEntity);

            // Test Cleanup
            _context.AfterTests.Add(() => _context.ActivationsCollection.DeleteEntityById(_activationEntity.Id));
        }

        [Fact]
        public async Task ShouldDeleteObjectAndReturnNoData204_WhenActivationExists()
        {
            // Act
            var response = await _context.ActivationsService.DeleteActivationById(_activationEntity.Id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                () => response.StringContent.ShouldBeEmpty(),
                () => _context.ActivationsCollection.ShouldNotContain(_activationEntity),
                () => _context.ActivationsCollection.ShouldNotBeEmpty()
            );
        }

        [Fact]
        public async Task ShouldReturnNoData204_WhenActivationDoesNotExist()
        {
            // Act
            var response = await _context.ActivationsService.DeleteActivationById(Utilities.GetRandomHexString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                () => response.StringContent.ShouldBeEmpty(),
                () => _context.ActivationsCollection.ShouldNotBeEmpty()
            );
        }
        
        [Fact]
        public async Task ShouldReturnNoData204_WhenNon24BitHexIdIsUsed()
        {
            // Act
            var response = await _context.ActivationsService.DeleteActivationById(Utilities.GetRandomString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                () => response.StringContent.ShouldBeEmpty(),
                () => _context.ActivationsCollection.ShouldNotBeEmpty()
            );
        }
    }
}
