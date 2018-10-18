using System.Net;
using System.Threading.Tasks;
using Bingo.RestEase.Models.Response;
using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using Xunit;

namespace Bingo.RestEase.Test.ExercisesControllerTests
{
    [Trait("Exercises", "Delete Exercise")]
    public class DeleteExercise : TestBase
    {
        private readonly TestContext _context;
        private readonly Exercise _exerciseEntity;

        public DeleteExercise(TestContext context)
        {
            _context = context;

            // Test Setup
            _exerciseEntity = Exercises.RandomContractExercise;
            _context.ExercisesCollection.InsertOne(_exerciseEntity);

            // Test Cleanup
            _context.AfterTests.Add(() => _context.ExercisesCollection.DeleteEntityById(_exerciseEntity.Id));
        }

        [Fact]
        public async Task ShouldDeleteObjectAndReturnNoData204_WhenExerciseExists()
        {
            // Act
            var response = await _context.ExercisesService.DeleteExerciseById(_exerciseEntity.Id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                () => response.StringContent.ShouldBeEmpty(),
                () => _context.ExercisesCollection.ShouldNotContain(_exerciseEntity),
                () => _context.ExercisesCollection.ShouldNotBeEmpty()
            );
        }

        [Fact]
        public async Task ShouldReturnNoData204_WhenExerciseDoesNotExist()
        {
            // Act
            var response = await _context.ExercisesService.DeleteExerciseById(Utilities.GetRandomHexString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                () => response.StringContent.ShouldBeEmpty(),
                () => _context.ExercisesCollection.ShouldNotBeEmpty()
            );
        }
        
        [Fact]
        public async Task ShouldReturnNoData204_WhenNon24BitHexIdIsUsed()
        {
            // Act
            var response = await _context.ExercisesService.DeleteExerciseById(Utilities.GetRandomString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                () => response.StringContent.ShouldBeEmpty(),
                () => _context.ExercisesCollection.ShouldNotBeEmpty()
            );
        }
    }
}
