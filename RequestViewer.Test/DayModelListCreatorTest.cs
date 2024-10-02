using System;
using System.Collections.Generic;
using System.Linq;
using RequestViewer.BusinessLogic.Services;
using RequestViewer.Domain.Models;
using Xunit;

namespace RequestViewer.Test
{
    public class DayModelListCreatorTest
    {
        [Fact]
        public void SuccessCreateDayModelListToSeptember()
        {
            var days = DayModelListCreator.Create(
                new Period()
                {
                    StartDate = DateTime.Parse("01.09.2024"),
                    EndDate = DateTime.Parse("30.09.2024")
                },
                new List<Day>(),
                true,
                true
            ).ToList();

            var assert1 =
                days[0].Day == "ПН" &&
                days[1].Day == "ВТ" &&
                days[2].Day == "СР" &&
                days[3].Day == "ЧТ" &&
                days[4].Day == "ПТ" &&
                days[5].Day == "СБ" &&
                days[6].Day == "ВС";

            var assert2 =
                string.IsNullOrEmpty(days[7].Day) &&
                string.IsNullOrEmpty(days[8].Day) &&
                string.IsNullOrEmpty(days[9].Day) &&
                string.IsNullOrEmpty(days[10].Day) &&
                string.IsNullOrEmpty(days[11].Day) &&
                string.IsNullOrEmpty(days[12].Day);

            var assert3 = new List<bool>();
            var j = 1;
            for (int i = 13; i < 43; i++)
            {
                assert3.Add(days[i].Day == new DateTime(2024, 9, j).ToShortDateString());
                j++;
            }

            Assert.True(assert1 && assert2 && assert3.All(x => x));
        }

        [Fact]
        public void SuccessCreateDayModelListToOctober()
        {
            var days = DayModelListCreator.Create(
                new Period()
                {
                    StartDate = DateTime.Parse("01.10.2024"),
                    EndDate = DateTime.Parse("31.10.2024")
                },
                new List<Day>(),
                true,
                true
            ).ToList();

            var assert1 =
                days[0].Day == "ПН" &&
                days[1].Day == "ВТ" &&
                days[2].Day == "СР" &&
                days[3].Day == "ЧТ" &&
                days[4].Day == "ПТ" &&
                days[5].Day == "СБ" &&
                days[6].Day == "ВС";

            var assert2 = string.IsNullOrEmpty(days[7].Day);

            var assert3 = new List<bool>();
            var j = 1;
            for (var i = 8; i < 39; i++)
            {
                assert3.Add(days[i].Day == new DateTime(2024, 10, j).ToShortDateString());
                j++;
            }

            Assert.True(assert1 && assert2 && assert3.All(x => x));
        }
    }
}
