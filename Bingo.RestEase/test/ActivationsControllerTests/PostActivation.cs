using Bingo.RestEase.Support;
using Bingo.RestEase.Test.Common;
using Bingo.RestEase.Test.TestData;
using Shouldly;
using System.Net;
using Xunit;

namespace Bingo.RestEase.Test
{
    [Trait("Activations", "Post Activation")]
    public class PostActivation: TestBase
    {
        private readonly TestContext _context;

        public PostActivation(TestContext context)
        {
            _context = context;
        }
        
        [Fact]
        public async void ShouldReturnCreatedActivation201_WhenActivationPostsSuccessfully()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);
            var returnedActivation = response.GetContent();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created),
                () => returnedActivation.ShouldBe(postDto.ToActivation()),
                () => _context.ActivationsCollection.ShouldContain(returnedActivation),
                () => _context.AfterTests.Add(() => _context.ActivationsCollection.DeleteEntityById(returnedActivation.Id))
            );
        }

        // This is useful for testing that the ToEntity() functions on the DTOs that are not required are handled correctly (null ref)
        [Fact]
        public async void ShouldReturnCreatedActivation201_WhenOnlyRequiredFieldsAreUsed()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.Electromyography = null;
            postDto.LactateProduction = null;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);
            var returnedActivation = response.GetContent();

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created),
                () => returnedActivation.ShouldBe(postDto.ToActivation()),
                () => _context.ActivationsCollection.ShouldContain(returnedActivation),
                () => _context.AfterTests.Add(() => _context.ActivationsCollection.DeleteEntityById(returnedActivation.Id))
            );
        }

        #region Required Fields

        [Fact]
        public async void ShouldReturnRequiredFieldError400_WhenExerciseId_IsExcluded()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.ExerciseId = null;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.ExerciseId)} field is required.")
            );
        }

        [Fact]
        public async void ShouldReturnRequiredFieldError400_WhenMuscleId_IsExcluded()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.MuscleId = null;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.MuscleId)} field is required.")
            );
        }

        [Fact]
        public async void ShouldReturnRequiredFieldError400_WhenRepetitionTempo_IsExcluded()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.RepetitionTempo = null;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.RepetitionTempo)} field is required.")
            );
        }

        [Fact]
        public async void ShouldReturnRequiredFieldError400_WhenRepetitionTempoType_IsExcluded()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.RepetitionTempo.Type = null;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.RepetitionTempo.Type)} field is required.")
            );
        }

        [Fact]
        public async void ShouldReturnRequiredFieldError400_WhenElectromyographyMeanEmg_IsExcluded()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.Electromyography.MeanEmg = null;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.Electromyography.MeanEmg)} field is required.")
            );
        }

        [Fact]
        public async void ShouldReturnRequiredFieldError400_WhenElectromyographyPeakEmg_IsExcluded()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.Electromyography.PeakEmg= null;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.Electromyography.PeakEmg)} field is required.")
            );
        }

        #endregion

        #region Field Length

        [Fact]
        [InlineData(23)]
        [InlineData(25)]
        public async void ShouldReturnFieldLengthError400_WhenExerciseId_IsNot24BitHex()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.ExerciseId = "Non24BitHex";

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.ExerciseId)} must be a valid 24-bit hex string.")
            );
        }

        [Fact]
        [InlineData(23)]
        [InlineData(25)]
        public async void ShouldReturnFieldLengthError400_WhenMuscleId_IsNot24BitHex()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.MuscleId = "Non24BitHex";

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.MuscleId)} must be a valid 24-bit hex string.")
            );
        }

        #endregion

        #region Field Range

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public async void ShouldReturnFieldRangeError400_WhenRangeOfMotion_IsNotZeroToOneHundred(double value)
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.RangeOfMotion = value;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.RangeOfMotion)} must be between 0 and 100")
            );
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public async void ShouldReturnFieldRangeError400_WhenForcePercentageOutput_IsNotZeroToOneHundred(double value)
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.ForceOutputPercentage = value;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.ForceOutputPercentage)} must be between 0 and 100")
            );
        }

        [Fact]
        public async void ShouldReturnFieldRangeError400_WhenRepetitionTempoDuration_IsLessThanZero()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.RepetitionTempo.Duration = -1;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.RepetitionTempo.Duration)} must be between 0 and ")
            );
        }

        [Fact]
        public async void ShouldReturnFieldRangeError400_WhenRepetitionTempoConcentricDuration_IsLessThanZero()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.RepetitionTempo.ConcentricDuration = -1;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.RepetitionTempo.ConcentricDuration)} must be between 0 and ")
            );
        }

        [Fact]
        public async void ShouldReturnFieldRangeError400_WhenRepetitionTempoEccentricDuration_IsLessThanZero()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.RepetitionTempo.EccentricDuration = -1;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.RepetitionTempo.EccentricDuration)} must be between 0 and ")
            );
        }

        [Fact]
        public async void ShouldReturnFieldRangeError400_WhenRepetitionTempoIsometricDuration_IsLessThanZero()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.RepetitionTempo.IsometricDuration = -1;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.RepetitionTempo.IsometricDuration)} must be between 0 and ")
            );
        }

        [Fact]
        public async void ShouldReturnFieldRangeError400_WhenElectromyographyMeanEmg_IsLessThanZero()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.Electromyography.MeanEmg = -1;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.Electromyography.MeanEmg)} must be between 0 and ")
            );
        }

        [Fact]
        public async void ShouldReturnFieldRangeError400_WhenElectromyographyPeakEmg_IsLessThanZero()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.Electromyography.PeakEmg = -1;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.Electromyography.PeakEmg)} must be between 0 and ")
            );
        }

        [Fact]
        public async void ShouldReturnFieldRangeError400_WhenLactateProduction_IsLessThanZero()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.LactateProduction.LactateProduction = -1;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.LactateProduction.LactateProduction)} must be between 0 and ")
            );
        }

        [Fact]
        public async void ShouldReturnFieldRangeError400_WhenLactateAerobicRespiration_IsLessThanZero()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.LactateProduction.AerobicRespiration = -1;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.LactateProduction.AerobicRespiration)} must be between 0 and ")
            );
        }

        [Fact]
        public async void ShouldReturnFieldRangeError400_WhenLactateAnaerobicRespiration_IsLessThanZero()
        {
            // Arrange
            var postDto = Activations.RandomContractPostActivationDto;
            postDto.LactateProduction.AnaerobicRespiration = -1;

            // Act
            var response = await _context.ActivationsService.PostActivation(postDto);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest),
                () => response.StringContent.ShouldContain($"{nameof(postDto.LactateProduction.AnaerobicRespiration)} must be between 0 and ")
            );
        }

        #endregion
    }
}
