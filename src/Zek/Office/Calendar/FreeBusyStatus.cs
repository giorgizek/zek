namespace Zek.Office
{
    public enum FreeBusyStatus
    {
        /// <summary>Unknown</summary>
        Unknown = -1, // 0xFFFFFFFF
        /// <summary>Free</summary>
        Free = 0,
        /// <summary>Tentative</summary>
        Tentative = 1,
        /// <summary>Busy</summary>
        Busy = 2,
        /// <summary>Oof</summary>
        Oof = 3,
        /// <summary>Working Elsewhere</summary>
        WorkingElsewhere = 4,
    }
}