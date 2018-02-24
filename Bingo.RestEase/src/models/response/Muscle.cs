using Bingo.RestEase.Support;
using Newtonsoft.Json;

namespace Bingo.RestEase.Models.Response
{
    public class Muscle : BingoEntity
    {
        public string Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string GroupId { get; set; }
        public string RegionId { get; set; }

        public override bool Equals(object obj)
        {
            return this.DeepEquals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
