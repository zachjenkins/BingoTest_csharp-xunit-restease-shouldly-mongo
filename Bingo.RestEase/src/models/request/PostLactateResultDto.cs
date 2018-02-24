using Bingo.RestEase.Models.Response;

namespace Bingo.RestEase.Models.Request
{
    public class PostLactateResultDto : RequestObject
    {
        public double? LactateProduction { get; set; }

        public double? AerobicRespiration { get; set; }

        public double? AnaerobicRespiration { get; set; }

        public LactateResult ToLactateResult()
        {
            return new LactateResult
            {
                LactateProduction = LactateProduction,
                AerobicRespiration = AerobicRespiration,
                AnaerobicRespiration = AnaerobicRespiration
            };
        }
    }
}
