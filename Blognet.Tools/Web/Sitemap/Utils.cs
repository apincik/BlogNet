using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Tools.Web.Sitemap
{
    public class Utils
    {
        /// <summary>
        /// Get string value of ChangeFrequency enum.
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static string GetChangeFrequency(ChangeFrequency frequency)
        {
            switch (frequency)
            {
                case ChangeFrequency.ALWAYS:
                    return "always";
                case ChangeFrequency.DAILY:
                    return "daily";
                case ChangeFrequency.HOURLY:
                    return "hourly";
                case ChangeFrequency.MONTHLY:
                    return "monthly";
                case ChangeFrequency.NEVER:
                    return "never";
                case ChangeFrequency.WEEKLY:
                    return "weekly";
                case ChangeFrequency.YEARLY:
                    return "yearly";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Get string value of Priority enum.
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public static string GetPriority(Priority priority)
        {
            switch(priority)
            {
                case Priority.ZERO:
                    return "0.0";
                case Priority.LOWEST:
                    return "0.1";
                case Priority.LOW:
                    return "0.3";
                case Priority.MEDIUM:
                    return "0.5";
                case Priority.HIGH:
                    return "0.8";
                case Priority.MAX:
                    return "1.0";
                default:
                    return "";
            }
        }
    }
}
