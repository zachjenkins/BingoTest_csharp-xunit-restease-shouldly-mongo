using Bingo.RestEase.Models.Response;
using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using System.Net;
using Xunit;

namespace Bingo.RestEase.Test
{
    [Trait("Muscles", "Delete Muscle")]
    public class DeleteMuscle: TestBase
    {
        private readonly TestContext _context;
        private readonly Muscle _muscleEntity;

        public DeleteMuscle(TestContext context)
        {
            _context = context;

            // Test Setup
            _muscleEntity = Muscles.RandomContractMuscle;
            _context.MusclesCollection.InsertOne(_muscleEntity);

            // Test Cleanup
            _context.AfterTests.Add(() => _context.MusclesCollection.DeleteEntityById(_muscleEntity.Id));
        }

        [Fact]
        public async void ShouldDeleteObjectAndReturnNoData204_WhenMuscleExists()
        {
            // Act
            var response = await _context.MusclesService.DeleteMuscleById(_muscleEntity.Id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                () => response.StringContent.ShouldBeEmpty(),
                () => _context.MusclesCollection.ShouldNotContain(_muscleEntity),
                () => _context.MusclesCollection.ShouldNotBeEmpty()
            );
        }

        [Fact]
        public async void ShouldReturnNoData204_WhenMuscleDoesNotExist()
        {
            // Act
            var response = await _context.MusclesService.DeleteMuscleById(Utilities.GetRandomHexString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                () => response.StringContent.ShouldBeEmpty(),
                () => _context.MusclesCollection.ShouldNotBeEmpty()
            );
        }
        
        [Fact]
        public async void ShouldReturnNoData204_WhenNon24BitHexIdIsUsed()
        {
            // Act
            var response = await _context.MusclesService.DeleteMuscleById(Utilities.GetRandomString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                () => response.StringContent.ShouldBeEmpty(),
                () => _context.MusclesCollection.ShouldNotBeEmpty()
            );
        }
    }
}
