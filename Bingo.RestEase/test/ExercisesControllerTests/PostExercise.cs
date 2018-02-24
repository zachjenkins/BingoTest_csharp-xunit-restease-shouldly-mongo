using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using System.Net;
using Xunit;

namespace Bingo.RestEase.Test
{
    [Trait("Exercises", "Post Exercise")]
    public class PostExercise : TestBase
    {
        private readonly TestContext _context;

        public PostExercise(TestContext context)
        {
            _context = context;
        }
        
        [Fact]
        public async void ShouldReturnCreatedExercise201_WhenExercisePostsSuccessfully()
        {
            // Arrange
            var postDto = Exercises.RandomPostExerciseDto;

            // Act
            var response = await _context.ExercisesService.PostExercise(postDto);
            var returnedExercise = response.GetContent();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created),
                () => returnedExercise.ShouldBe(postDto.ToExercise()),
                () => _context.ExercisesCollection.ShouldContain(returnedExercise),
                () => _context.AfterTests.Add(() => _context.ExercisesCollection.DeleteEntityById(returnedExercise.Id))
            );
        }

        #region Required Fields

        [Fact]
        public async void ShouldReturnRequiredFieldsError400_WhenNameIsExcluded()
        {
            // Arrange
            var postDto = Exercises.RandomPostExerciseDto;
            postDto.Name = null;

            // Act
            var response = await _context.ExercisesService.PostExercise(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.Name)} field is required")
            );
        }

        [Fact]
        public async void ShouldReturnRequiredFieldsError400_WhenShortNameIsExcluded()
        {
            // Arrange
            var postDto = Exercises.RandomPostExerciseDto;
            postDto.ShortName = null;

            // Act
            var response = await _context.ExercisesService.PostExercise(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.ShortName)} field is required")
            );
        }

        [Fact]
        public async void ShouldReturnRequiredFieldsError400_WhenLongNameIsExcluded()
        {
            // Arrange
            var postDto = Exercises.RandomPostExerciseDto;
            postDto.LongName = null;

            // Act
            var response = await _context.ExercisesService.PostExercise(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.LongName)} field is required")
            );
        }

        #endregion

        #region Field Length

        [Fact]
        public async void ShouldReturnFieldLengthError400_WhenNameIsGreaterThan30()
        {
            // Arrange
            var postDto = Exercises.RandomPostExerciseDto;
            postDto.Name = new string('a', 31);

            // Act
            var response = await _context.ExercisesService.PostExercise(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain(
                    $"The field {nameof(postDto.Name)} must be a string or array type with a maximum length of '30'.")
            );
        }

        [Fact]
        public async void ShouldReturnFieldLengthError400_WhenShortNameIsGreaterThan20()
        {
            // Arrange
            var postDto = Exercises.RandomPostExerciseDto;
            postDto.ShortName = new string('a', 21);

            // Act
            var response = await _context.ExercisesService.PostExercise(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain(
                    $"The field {nameof(postDto.ShortName)} must be a string or array type with a maximum length of '20'.")
            );
        }

        [Fact]
        public async void ShouldReturnFieldLengthError400_WhenLongNameIsGreaterThan60()
        {
            // Arrange
            var postDto = Exercises.RandomPostExerciseDto;
            postDto.LongName = new string('a', 61);

            // Act
            var response = await _context.ExercisesService.PostExercise(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain(
                    $"The field {nameof(postDto.LongName)} must be a string or array type with a maximum length of '60'.")
            );
        }

        #endregion
    }
}
