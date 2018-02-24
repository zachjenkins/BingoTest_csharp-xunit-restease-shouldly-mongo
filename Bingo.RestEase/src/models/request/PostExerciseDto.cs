using Bingo.RestEase.Models.Response;

namespace Bingo.RestEase.Models.Request
{
    public class PostExerciseDto : RequestObject
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string LongName { get; set; }

        public Exercise ToExercise()
        {
            return new Exercise
            {
                Name = Name,
                ShortName = ShortName,
                LongName = LongName,
            };
        }
    }
}
