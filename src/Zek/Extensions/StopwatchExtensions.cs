using System;
using System.Diagnostics;

namespace Zek.Extensions
{
    public static class StopwatchExtensions
    {
        /// <summary>
        /// ითვლის სავარაუდო დროს რაიმე პროცესის დასრულების
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <param name="total"></param>
        /// <param name="processed"></param>
        /// <returns></returns>
        public static TimeSpan GetEta(this Stopwatch stopwatch, int total, int processed)
        {
            /* this is based off of:
             * (TimeTaken / linesProcessed) * linesLeft=timeLeft
             * so we have
             * (10/100) * 200 = 20 Seconds now 10 seconds go past
             * (20/100) * 200 = 40 Seconds left now 10 more seconds and we process 100 more lines
             * (30/200) * 100 = 15 Seconds and now we all see why the copy file dialog jumps from 3 hours to 30 minutes :-)
             * 
             */
            var elapsedMinutes = stopwatch.ElapsedMilliseconds / 1000F / 60F;
            var minutesLeft = elapsedMinutes / processed * (total - processed);
            return TimeSpan.FromMinutes(minutesLeft);
        }
        /// <summary>
        /// ითვლის სავარაუდო დროს რაიმე პროცესის დასრულების
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <param name="total"></param>
        /// <param name="processed"></param>
        /// <returns></returns>
        public static TimeSpan GetEta(this Stopwatch stopwatch, long total, long processed)
        {
            var elapsedMinutes = stopwatch.ElapsedMilliseconds / 1000F / 60F;
            var minutesLeft = elapsedMinutes / processed * (total - processed);
            return TimeSpan.FromMinutes(minutesLeft);
        }
    }
}
