using Bingo.RestEase.Models.Response;
using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using System.Net;
using Xunit;

namespace Bingo.RestEase.Test
{
    [Trait("Exercises", "Post Activation to Exercise")]
    public class PostActivationToExercise : TestBase
    {
        private readonly TestContext _context;
        private readonly Exercise _parentExercise;

        public PostActivationToExercise(TestContext context)
        {
            _context = context;

            // Test Setup
            _parentExercise = Exercises.RandomContractExercise;
            _context.ExercisesCollection.InsertOne(_parentExercise);

            // Test Teardown
            _context.AfterTests.Add(() => _context.ExercisesCollection.DeleteEntityById(_parentExercise.Id));
        }
        
        [Fact]
        public async void ShouldReturnCreatedActivation201_WhenActivationPostsSuccessfully()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;

            // Act
            var response = await _context.ExercisesService.PostActivationToExercise(_parentExercise.Id, postDto);
            var returnedActivation = response.GetContent();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created),
                () => returnedActivation.ShouldBe(postDto.ToActivation()),
                () => returnedActivation.ExerciseId.ShouldBe(_parentExercise.Id),
                () => _context.ActivationsCollection.ShouldContain(returnedActivation),
                () => _context.AfterTests.Add(() => _context.ActivationsCollection.DeleteEntityById(returnedActivation.Id))
            );
        }

        [Fact]
        public async void ShouldReturnNoData404_WhenExerciseDoesNotExist()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;

            // Act
            var response = await _context.ExercisesService.PostActivationToExercise(Utilities.GetRandomHexString(), postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }
    }
}
