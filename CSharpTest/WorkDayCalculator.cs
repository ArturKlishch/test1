using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            DateTime DayOfResults = startDate;
            dayCount -= 1;

            if (weekEnds != null && dayCount > 0)
            {
                foreach (WeekEnd weekEnd in weekEnds)
                {
                    int EndOfWeekDays = (weekEnd.StartDate - DayOfResults).Days - 1;

                    if (EndOfWeekDays > 0 || EndOfWeekDays == 0)
                    {
                        if (dayCount - EndOfWeekDays >= 0)
                        {
                            DayOfResults = DayOfResults.AddDays(EndOfWeekDays);
                            dayCount -= EndOfWeekDays;
                        }
                        else
                        {
                            DayOfResults = DayOfResults.AddDays(dayCount);
                            dayCount = 0;
                        }

                        if (dayCount > 0)
                        {
                            int daysInWeekEnd = (weekEnd.EndDate - weekEnd.StartDate).Days + 1;
                            DayOfResults = DayOfResults.AddDays(daysInWeekEnd);
                        }
                    }
                }
            }

            if (dayCount > 0)
            {
                DayOfResults = DayOfResults.AddDays(dayCount);
            }

            return DayOfResults;
        }
    }
}
