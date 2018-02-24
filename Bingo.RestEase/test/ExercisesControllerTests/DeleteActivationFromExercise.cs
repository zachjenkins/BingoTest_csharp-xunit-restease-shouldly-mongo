using Bingo.RestEase.Models.Response;
using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using System.Net;
using Xunit;

namespace Bingo.RestEase.Test
{
    [Trait("Exercises", "Get Activation From Exercise")]
    public class GetActivationFromExercise : TestBase
    {
        private readonly TestContext _context;
        private readonly Exercise _exerciseEntity;
        private readonly Activation _activationEntity;

        public GetActivationFromExercise(TestContext context)
        {
            _context = context;

            // Test Setup
            _exerciseEntity = Exercises.RandomContractExercise;
            _activationEntity = Activations.RandomContractActivation;
            _activationEntity.ExerciseId = _exerciseEntity.Id;
            _context.ExercisesCollection.InsertOne(_exerciseEntity);
            _context.ActivationsCollection.InsertOne(_activationEntity);

            // Test Cleanup
            _context.AfterTests.Add(() => _context.ExercisesCollection.DeleteEntityById(_exerciseEntity.Id));
            _context.AfterTests.Add(() => _context.ActivationsCollection.DeleteEntityById(_activationEntity.Id));
        }

        [Fact]
        public async void ShouldReturnExpectedActivation200_WhenExerciseAndActivationExist()
        {
            // Act
            var response = await _context.ExercisesService.GetActivationForExercise(_exerciseEntity.Id, _activationEntity.Id);
            var returnedActivation = response.GetContent();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
                () => returnedActivation.ShouldBe(_activationEntity)
            );
        }

        [Fact]
        public async void ShouldReturnNoData404_WhenEntitiesAreNotLinked_ByExerciseId()
        {
            // Arrange
            var exercise = Exercises.RandomContractExercise;
            var activation = Activations.RandomContractActivation;
            _context.ExercisesCollection.InsertOne(exercise);
            _context.ActivationsCollection.InsertOne(activation);

            // Act
            var response = await _context.ExercisesService.GetActivationForExercise(exercise.Id, activation.Id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty(),
                () => _context.AfterTests.Add(() => _context.ExercisesCollection.DeleteEntityById(exercise.Id)),
                () => _context.AfterTests.Add(() => _context.ActivationsCollection.DeleteEntityById(activation.Id))
            );
        }

        [Fact]
        public async void ShouldReturnNoData404_WhenExerciseDoesNotExist()
        {
            // Act
            var response = await _context.ExercisesService.GetActivationForExercise(Utilities.GetRandomHexString(), _activationEntity.Id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }

        [Fact]
        public async void ShouldReturnNoData404_WhenActivationDoesNotExist()
        {
            // Act
            var response = await _context.ExercisesService.GetActivationForExercise(_exerciseEntity.Id, Utilities.GetRandomHexString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }

        [Fact]
        public async void ShouldReturnNoData404_WhenExerciseIdIsNon24BitHex()
        {
            // Act
            var response = await _context.ExercisesService.GetActivationForExercise(Utilities.GetRandomString(24), _activationEntity.Id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }

        [Fact]
        public async void ShouldReturnNoData404_WhenActivationIdIsNon24BitHex()
        {
            // Act
            var response = await _context.ExercisesService.GetActivationForExercise(_exerciseEntity.Id, Utilities.GetRandomString(24));

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }
    }
}
