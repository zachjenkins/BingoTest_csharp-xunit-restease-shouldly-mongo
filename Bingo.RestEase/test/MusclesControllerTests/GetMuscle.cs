using System.Net;
using System.Threading.Tasks;
using Bingo.RestEase.Models.Response;
using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using Xunit;

namespace Bingo.RestEase.Test.MusclesControllerTests
{
    [Trait("Muscles", "Get Muscle")]
    public class GetMuscle : TestBase
    {
        private readonly TestContext _context;
        private readonly Muscle _muscleEntity;

        public GetMuscle(TestContext context)
        {
            _context = context;

            // Test Setup
            _muscleEntity = Muscles.RandomContractMuscle;
            _context.MusclesCollection.InsertOne(_muscleEntity);

            // Test Cleanup
            _context.AfterTests.Add(() => _context.MusclesCollection.DeleteEntityById(_muscleEntity.Id));
        }

        [Fact]
        public async Task ShouldReturnExpectedMuscleWith200_WhenMuscleExists()
        {
            // Act
            var response = await _context.MusclesService.GetMuscleById(_muscleEntity.Id);
            var returnedMuscle = response.GetContent();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
                () => returnedMuscle.ShouldBe(_muscleEntity)
            );
        }

        [Fact]
        public async Task ShouldReturnNoData404_WhenMuscleDoesNotExist()
        {
            // Act
            var response = await _context.MusclesService.GetMuscleById(Utilities.GetRandomHexString());

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
            var response = await _context.MusclesService.GetMuscleById(Utilities.GetRandomString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }
    }
}
