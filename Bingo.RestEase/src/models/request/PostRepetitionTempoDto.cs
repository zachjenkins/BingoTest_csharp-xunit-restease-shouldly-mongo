using Bingo.RestEase.Models.Response;

namespace Bingo.RestEase.Models.Request
{
    public class PostRepetitionTempoDto
    {
        public string Type { get; set; }

        public double? Duration { get; set; }

        public double? ConcentricDuration { get; set; }

        public double? EccentricDuration { get; set; }

        public double? IsometricDuration { get; set; }

        public RepetitionTempo ToRepetitionTempo()
        {
            return new RepetitionTempo
            {
                Type = Type,
                Duration = Duration,
                ConcentricDuration = ConcentricDuration,
                EccentricDuration = EccentricDuration,
                IsometricDuration = IsometricDuration
            };
        }
    }
}
