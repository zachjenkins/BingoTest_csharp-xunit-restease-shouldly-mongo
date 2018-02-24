using Bingo.RestEase.Models.Response;
using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using System.Net;
using Xunit;

namespace Bingo.RestEase.Test
{
    [Trait("Exercises", "Get Exercise")]
    public class GetExercise : TestBase
    {
        private readonly TestContext _context;
        private readonly Exercise _exerciseEntity;

        public GetExercise(TestContext context)
        {
            _context = context;

            // Test Setup
            _exerciseEntity = Exercises.RandomContractExercise;
            _context.ExercisesCollection.InsertOne(_exerciseEntity);

            // Test Cleanup
            _context.AfterTests.Add(() => _context.ExercisesCollection.DeleteEntityById(_exerciseEntity.Id));
        }

        [Fact]
        public async void ShouldReturnExpectedExerciseWith200_WhenExerciseExists()
        {
            // Act
            var response = await _context.ExercisesService.GetExerciseById(_exerciseEntity.Id);
            var returnedExercise = response.GetContent();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
                () => returnedExercise.ShouldBe(_exerciseEntity)
            );
        }

        [Fact]
        public async void ShouldReturnNoData404_WhenExerciseDoesNotExist()
        {
            // Act
            var response = await _context.ExercisesService.GetExerciseById(Utilities.GetRandomHexString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }

        [Fact]
        public async void ShouldReturnNoData404_WhenNon24BitHexIsUsed()
        {
            // Act
            var response = await _context.ExercisesService.GetExerciseById(Utilities.GetRandomString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }
    }
}
