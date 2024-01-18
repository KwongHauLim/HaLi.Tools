using System.Diagnostics;
using System.Globalization;

namespace HaLi.Tools.Extends;

[DebuggerStepThrough]
public static class ExtendDateTime
{
    private const long __MAXTICK__ = 3155378975999999999L;

    public static long Round(this long dateTicks, TimeSpan span)
    {
        long ticks = (dateTicks + (span.Ticks / 2)) / span.Ticks;
        return Math.Min(ticks * span.Ticks, __MAXTICK__);
    }
    public static long Round(this long dateTicks, TimeSpan span, TimeSpan grace)
    {
        long ticks = (dateTicks + (span.Ticks - grace.Ticks)) / span.Ticks;
        return Math.Min(ticks * span.Ticks, __MAXTICK__);
    }
    public static long Floor(this long dateTicks, TimeSpan span)
    {
        long ticks = (dateTicks / span.Ticks);
        return ticks * span.Ticks;
    }
    public static long Ceil(this long dateTicks, TimeSpan span)
    {
        long ticks = (dateTicks + span.Ticks - 1) / span.Ticks;
        return Math.Min(ticks * span.Ticks, __MAXTICK__);
    }
    public static DateTime Round(this DateTime date, TimeSpan span)
    {
        return new DateTime(date.Ticks.Round(span));
    }
    public static DateTime Round(this DateTime date, TimeSpan span, TimeSpan grace)
    {
        return new DateTime(date.Ticks.Round(span, grace));
    }
    public static DateTime Floor(this DateTime date, TimeSpan span)
    {
        return new DateTime(date.Ticks.Floor(span));
    }
    public static DateTime Ceil(this DateTime date, TimeSpan span)
    {
        return new DateTime(date.Ticks.Ceil(span));
    }
    public static DateTimeOffset Round(this DateTimeOffset date, TimeSpan span)
    {
        return new DateTimeOffset(date.Ticks.Round(span), date.Offset);
    }
    public static DateTimeOffset Round(this DateTimeOffset date, TimeSpan span, TimeSpan grace)
    {
        return new DateTimeOffset(date.Ticks.Round(span, grace), date.Offset);
    }
    public static DateTimeOffset Floor(this DateTimeOffset date, TimeSpan span)
    {
        return new DateTimeOffset(date.Ticks.Floor(span), date.Offset);
    }
    public static DateTimeOffset Ceil(this DateTimeOffset date, TimeSpan span)
    {
        return new DateTimeOffset(date.Ticks.Ceil(span), date.Offset);
    }
    public static TimeSpan Round(this TimeSpan time, TimeSpan span)
    {
        return new TimeSpan(time.Ticks.Round(span));
    }
    public static TimeSpan Round(this TimeSpan time, TimeSpan span, TimeSpan grace)
    {
        return new TimeSpan(time.Ticks.Round(span, grace));
    }
    public static TimeSpan Floor(this TimeSpan time, TimeSpan span)
    {
        return new TimeSpan(time.Ticks.Floor(span));
    }
    public static TimeSpan Ceil(this TimeSpan time, TimeSpan span)
    {
        return new TimeSpan(time.Ticks.Ceil(span));
    }
    public static TimeSpan Multiple(this TimeSpan time, double m)
    {
        return TimeSpan.FromSeconds(time.TotalSeconds * m);
    }
    public static bool AboveZero(this TimeSpan time)
    {
        return time.Ticks > 0L;
    }

    public static int GetWeekOfYear(this DateTime date)
    {
        GregorianCalendar ca = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
        return ca.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
    }

    public static int GetWeekOfMonth(this DateTime date)
    {
        GregorianCalendar ca = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
        int weekToday = ca.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        int weekFirst = ca.GetWeekOfYear(new DateTime(date.Year, date.Month, 1), CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        return weekToday - weekFirst;
    }

    /// <summary>
    /// Return 00:00 of date
    /// </summary>
    public static DateTime ToStartDate(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
    /// <summary>
    /// Return 00:00 of next day of date
    /// </summary>
    public static DateTime ToNextDate(this DateTime date) => new DateTime(date.Year, date.Month, date.Day + 1, 0, 0, 0);

    public static long ToUnixTime(this DateTime date)
    {
        DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (long)date.ToUniversalTime().Subtract(sTime).TotalSeconds;
    }

    public static DateTime ToDateTime(this long unix)
    {
        DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return sTime.AddSeconds(unix);
    }
}
