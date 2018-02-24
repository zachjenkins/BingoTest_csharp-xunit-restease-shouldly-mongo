namespace Bingo.RestEase.Models.Request
{
    public abstract class RequestObject
    {
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
