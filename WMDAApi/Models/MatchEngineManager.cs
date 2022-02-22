using System.Collections.Generic;

namespace WMDAApi.Models
{
    public static class MatchEngineManager
    {
        public static readonly List<MatchEngine> MatchEngines = new List<MatchEngine> { new MatchEngineOne(), new MatchEngineTwo() };
    }
}
