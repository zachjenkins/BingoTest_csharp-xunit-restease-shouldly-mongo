using System;
using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using System.Net;
using Bingo.RestEase.Models.Response;
using Xunit;

namespace Bingo.RestEase.Test
{
    public class GetExercise : TestBase
    {
        private readonly ServiceFixture _service;
        private readonly Exercise _exerciseEntity;

        public GetExercise(ServiceFixture service)
        {
            _service = service;

            // Arrange
            _exerciseEntity = Exercises.RandomContractExercise;
            _service.ExercisesCollection.InsertOne(_exerciseEntity);
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing");
        }

        [Fact]
        public async void ShouldReturnExpectedExerciseWith200_WhenExerciseExists()
        {
            // Act
            var response = await _service.ExercisesService.GetExerciseById(_exerciseEntity.Id);
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
            var response = await _service.ExercisesService.GetExerciseById(Utilities.GetRandomHexString());

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
            var response = await _service.ExercisesService.GetExerciseById(Utilities.GetRandomString());

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound),
                () => response.StringContent.ShouldBeEmpty()
            );
        }
    }
}
