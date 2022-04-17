namespace SchedulerMain.Models
{
    public class Day
    {
        public DayOfWeek DayOfWeek { get; set; }
        public int? DayIndexInMonth { get; set; }

        public DateTime? Date { get; set; }
        public int WeekOfMonth {get; set; }
    }

    public class Month
    {
        public enum MonthEnum
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        public MonthEnum MonthOfYear => (MonthEnum)MonthIndex;

        public int MonthIndex { get; set; }

        public string? Name { get; set; }
        public Day? CurrentDay { get; set; }
        public List<Day> Days { get; set; } = new List<Day>();
        public string? Year { get; set; }
        public int SkippedDays => (int)Days.First().DayOfWeek == 0 ? 6 : (int)Days.First().DayOfWeek - 1;
    }

    public static class MonthFactory
    {
        public static Month GetSelectedMonth(DateTime dateTime)
        {
            dateTime = SubtractTimeAndDays(dateTime);
            var month = new Month();
            var days = new List<Day>();

            var daysInSelectedMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            var monthsWeek = 1;

            for (int i = 0; i < daysInSelectedMonth; i++)
            {
                var daysDate = dateTime.AddDays(i);
                days.Add(new Day { DayOfWeek = daysDate.DayOfWeek, DayIndexInMonth = i + 1, Date = daysDate, WeekOfMonth = monthsWeek });
                month.Days = days;
                month.MonthIndex = dateTime.Month;
                month.Year = dateTime.Year.ToString();
                if (daysDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    monthsWeek += 1;
                }
            }


            return month;
        }

        private static DateTime SubtractTimeAndDays(DateTime dateTime)
        {
            var subtractedDateTime = dateTime.AddDays(-dateTime.Day + 1);
            subtractedDateTime = subtractedDateTime.AddHours(-dateTime.Hour);
            subtractedDateTime = subtractedDateTime.AddMinutes(-dateTime.Minute);
            subtractedDateTime = subtractedDateTime.AddSeconds(-dateTime.Second);
            subtractedDateTime = subtractedDateTime.AddMilliseconds(-dateTime.Millisecond);

            return subtractedDateTime;
        }
    }

    public class CalendarViewModel
    {
        public CalendarViewModel(Month activeMonth)
        {
            ActiveMonth = activeMonth;
        }

        public string Name { get => "Calendar name"; }
        public Month? ActiveMonth { get; set; }
        public String? ActiveYear => ActiveMonth?.Year;
    }
}
