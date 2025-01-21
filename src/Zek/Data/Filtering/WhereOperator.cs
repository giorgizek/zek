namespace Zek.Data.Filtering
{
    [Flags]
    public enum WhereOperator
    {
        None = 0,
        Equals = 1,
        NotEquals = 2,

        GreaterThan = 4,
        GreaterThanOrEquals = 8,
        LessThan = 16,
        LessThanOrEquals = 32,

        Between = 64,
        NotBetween = 128,
        Overlap = 256,
        NotOverlap = 512,

        Like = 1024,//todo
        NotLike = 2048,//todo
        Contains = 4096,
        NotContains = 8192,//todo
        Begins = 16384,
        NotBegins = 32768,//todo
        Ends = 65536,
        NotEnds = 131072,//todo
        

        In = 262144,//todo
        NotIn = 524288,//todo

        ContainsAny = 1048576,

        ForText = Equals | NotEquals | GreaterThan | GreaterThanOrEquals | LessThan | LessThanOrEquals | Between | NotBetween | Like | NotLike | Contains | NotContains | Begins | NotBegins | Ends | NotEnds | In | NotIn,
        ForDate = Equals | NotEquals | GreaterThan | GreaterThanOrEquals | LessThan | LessThanOrEquals | Between | NotBetween,
        ForNumber = Equals | NotEquals | GreaterThan | GreaterThanOrEquals | LessThan | LessThanOrEquals | Between | NotBetween | In | NotIn,
    }
}
