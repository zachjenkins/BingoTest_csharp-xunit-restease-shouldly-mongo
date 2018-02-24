using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using System.Net;
using Xunit;

namespace RestEase.IntegrationTests
{
    [Trait("Integration", nameof(MusclesControllerTest))]
    public class MusclesControllerTest : TestBase
    {
        private readonly ServiceFixture _service;

        public MusclesControllerTest(ServiceFixture service)
        {
            _service = service;
        }
        /*
        [Fact]
        public async void GetMuscles_WhenDataExists_ReturnsListContainingExpectedMuscle200()
        {
            var expectedMuscles = Muscles.ContractMuscles;

            var response = await _service.MusclesService.GetMuscles();
            var actualMuscles = response.GetContent();

            this.ShouldSatisfyAllConditions(
                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
                    () => expectedMuscles.ForEach(muscle =>
                            actualMuscles.ShouldContain(muscle))
                );
        }

        [Fact]
        public async void GetMuscle_ByMuscleId_ReturnsExpectedMuscle200()
        {
            var expectedMuscle = Muscles.ContractMuscle;

            var response = await _service.MusclesService.GetMuscleById(expectedMuscle.Id);
            var actualMuscle = response.GetContent();

            this.ShouldSatisfyAllConditions(
                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
                    () => actualMuscle.ShouldBe(expectedMuscle),
                    () => _service.MusclesCollection.ShouldContain(actualMuscle)
                );
        }

        [Fact]
        public async void PostMuscle_ByValidDto_ReturnsPostedMuscle201()
        {
            var postDto = Muscles.ContractMusclePostDto;

            var response = await _service.MusclesService.PostMuscle(postDto);
            var postedMuscle = response.GetContent();

            this.ShouldSatisfyAllConditions(
                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created),
                    () => postedMuscle.Id.ShouldNotBeNull(),
                    () => postedMuscle.ShouldBe(postDto.ToMuscle()),
                    () => _service.MusclesCollection.ShouldContain(postedMuscle)
                );
        }

        [Fact]
        public async void DeleteMuscle_ByMuscleId_ReturnsNoData204()
        {
            var muscleToDelete = Muscles.RandomizedMuscle;
            _service.MusclesCollection.InsertOne(muscleToDelete);

            var response = await _service.MusclesService.DeleteMuscleById(muscleToDelete.Id);

            this.ShouldSatisfyAllConditions(
                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                    () => response.StringContent.ShouldBeEmpty(),
                    () => _service.MusclesCollection.ShouldNotContain(muscleToDelete),
                    () => _service.MusclesCollection.ShouldNotBeEmpty()
                );
        }*/
    }
}
