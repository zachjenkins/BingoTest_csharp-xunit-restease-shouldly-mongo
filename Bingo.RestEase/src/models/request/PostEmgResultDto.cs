using Bingo.RestEase.Models.Response;

namespace Bingo.RestEase.Models.Request
{
    public class PostEmgResultDto : RequestObject
    {
        public double? MeanEmg { get; set; }
        
        public double? PeakEmg { get; set; }

        public EmgResult ToEmgResult()
        {
            return new EmgResult
            {
                MeanEmg = MeanEmg,
                PeakEmg = PeakEmg
            };
        }
    }

    
}
