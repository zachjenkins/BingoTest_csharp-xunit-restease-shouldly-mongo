using Bingo.RestEase.Support;
using Newtonsoft.Json;

namespace Bingo.RestEase.Models.Response
{
    public class BingoEntity
    {
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
