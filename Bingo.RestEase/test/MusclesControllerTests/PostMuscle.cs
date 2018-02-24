using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using System.Net;
using Xunit;

namespace Bingo.RestEase.Test
{
    [Trait("Muscles", "Post Muscle")]
    public class PostMuscle: TestBase
    {
        private readonly TestContext _context;

        public PostMuscle(TestContext context)
        {
            _context = context;
        }
        
        [Fact]
        public async void ShouldReturnCreatedMuscle201_WhenMusclePostsSuccessfully()
        {
            // Arrange
            var postDto = Muscles.RandomPostMuscleDto;

            // Act
            var response = await _context.MusclesService.PostMuscle(postDto);
            var returnedMuscle = response.GetContent();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created),
                () => returnedMuscle.ShouldBe(postDto.ToMuscle()),
                () => _context.MusclesCollection.ShouldContain(returnedMuscle),
                () => _context.AfterTests.Add(() => _context.MusclesCollection.DeleteEntityById(returnedMuscle.Id))
            );
        }

        #region Required Fields

        [Fact]
        public async void ShouldReturnRequiredFieldsError400_WhenNameIsExcluded()
        {
            // Arrange
            var postDto = Muscles.RandomPostMuscleDto;
            postDto.Name = null;

            // Act
            var response = await _context.MusclesService.PostMuscle(postDto);

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
            var postDto = Muscles.RandomPostMuscleDto;
            postDto.ShortName = null;

            // Act
            var response = await _context.MusclesService.PostMuscle(postDto);

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
            var postDto = Muscles.RandomPostMuscleDto;
            postDto.LongName = null;

            // Act
            var response = await _context.MusclesService.PostMuscle(postDto);

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
            var postDto = Muscles.RandomPostMuscleDto;
            postDto.Name = new string('a', 31);

            // Act
            var response = await _context.MusclesService.PostMuscle(postDto);

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
            var postDto = Muscles.RandomPostMuscleDto;
            postDto.ShortName = new string('a', 21);

            // Act
            var response = await _context.MusclesService.PostMuscle(postDto);

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
            var postDto = Muscles.RandomPostMuscleDto;
            postDto.LongName = new string('a', 61);

            // Act
            var response = await _context.MusclesService.PostMuscle(postDto);

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
