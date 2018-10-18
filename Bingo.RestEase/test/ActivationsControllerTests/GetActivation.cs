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
    [Trait("Activations", "Get Activation")]
    public class GetActivation : TestBase
    {
        private readonly TestContext _context;
        private readonly Activation _activationEntity;

        public GetActivation(TestContext context)
        {
            _context = context;

            // Test Setup
            _activationEntity = Activations.RandomContractActivation;
            _context.ActivationsCollection.InsertOne(_activationEntity);

            // Test Cleanup
            _context.AfterTests.Add(() => _context.ActivationsCollection.DeleteEntityById(_activationEntity.Id));
        }

        [Fact]
        public async Task ShouldReturnExpectedActivationWith200_WhenActivationExists()
        {
            // Act
            var response = await _context.ActivationsService.GetActivationById(_activationEntity.Id);
            var returnedActivation = response.GetContent();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
                () => returnedActivation.ShouldBe(_activationEntity)
            );
        }

        [Fact]
        public async Task ShouldReturnNoData404_WhenActivationDoesNotExist()
        {
            // Act
            var response = await _context.ActivationsService.GetActivationById(Utilities.GetRandomHexString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }

        [Fact]
        public async Task ShouldReturnNoData404_WhenNon24BitHexIsUsed()
        {
            // Act
            var response = await _context.ActivationsService.GetActivationById(Utilities.GetRandomString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }
    }
}
