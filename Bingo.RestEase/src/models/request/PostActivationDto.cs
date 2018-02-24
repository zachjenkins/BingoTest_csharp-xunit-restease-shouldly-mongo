using Bingo.RestEase.Models.Response;

namespace Bingo.RestEase.Models.Request
{
    public class PostActivationDto : RequestObject
    {
        public string ExerciseId { get; set; }
        
        public string MuscleId { get; set; }
        
        public double? RangeOfMotion { get; set; }
        
        public double? ForceOutputPercentage { get; set; }
        
        public PostRepetitionTempoDto RepetitionTempo { get; set; }

        public PostEmgResultDto Electromyography { get; set; }

        public PostLactateResultDto LactateProduction { get; set; }

        public Activation ToActivation()
        {
            var activation = new Activation
            {
                ExerciseId = ExerciseId,
                MuscleId = MuscleId,
                RangeOfMotion = RangeOfMotion,
                ForceOutputPercentage = ForceOutputPercentage
            };

            if (RepetitionTempo != null)
                activation.RepetitionTempo = RepetitionTempo.ToRepetitionTempo();
            if (Electromyography != null)
                activation.Electromyography = Electromyography.ToEmgResult();
            if (LactateProduction != null)
                activation.LactateProduction = LactateProduction.ToLactateResult();

            return activation;
        }
    }
}
