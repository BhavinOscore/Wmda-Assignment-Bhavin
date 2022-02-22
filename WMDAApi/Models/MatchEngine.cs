namespace WMDAApi.Models
{
    public abstract class MatchEngine
    {
        public abstract int MatchEngineId { get; }
        public abstract string EndPoint { get; }
    }
}
