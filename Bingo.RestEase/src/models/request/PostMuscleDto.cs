using Bingo.RestEase.Models.Response;

namespace Bingo.RestEase.Models.Request
{
    public class PostMuscleDto : RequestObject
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string LongName { get; set; }

        public string GroupId { get; set; }

        public string RegionId { get; set; }

        public Muscle ToMuscle()
        {
            return new Muscle
            {
                Name = Name,
                ShortName = ShortName,
                LongName = LongName,
                GroupId = GroupId,
                RegionId = RegionId
            };
        }
    }
}
