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
            if (dayCount > 1)
                dayCount -= 1;

            if (weekEnds != null && dayCount > 0)
            {
                foreach (WeekEnd weekEnd in weekEnds)
                {
                    int workingDays;
                    bool isItWeekEndDay = DayOfResults >= weekEnd.StartDate && DayOfResults <= weekEnd.EndDate;
                    if (isItWeekEndDay)
                    {
                        workingDays = 0;
                        DayOfResults = weekEnd.EndDate.AddDays(1);
                    }
                    else
                    {
                        workingDays = (weekEnd.StartDate - DayOfResults).Days;
                    }

                    if (workingDays >= 0)
                    {
                        if (dayCount - workingDays >= 0)
                        {
                            DayOfResults = DayOfResults.AddDays(workingDays);
                            dayCount -= workingDays;
                        }
                        else
                        {
                            DayOfResults = DayOfResults.AddDays(dayCount);
                            dayCount = 0;
                        }

                        if (dayCount > 0 && !isItWeekEndDay)
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
