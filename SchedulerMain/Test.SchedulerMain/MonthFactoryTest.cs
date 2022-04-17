using NUnit.Framework;
using SchedulerMain.Models;
using System;

namespace Test.SchedulerMain
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MonthFactory_GetSelectedMonth_MonthReturned()
        {
            var year = 1999;
            var month = 11;
            var day = 15;
            var hour = 23;
            var minute = 59;
            var second = 12;

            var date = new DateTime(year, month, day, hour, minute, second);
            var createdMonth = MonthFactory.GetSelectedMonth(date);

            Assert.IsNotNull(month);

            CollectionAssert.AllItemsAreNotNull(createdMonth.Days);
            CollectionAssert.AllItemsAreUnique(createdMonth.Days);

            Assert.IsTrue(createdMonth.Days.Count == 30);
            Assert.AreEqual(DayOfWeek.Monday, createdMonth.Days[0].DayOfWeek);
            Assert.AreEqual(1, createdMonth.Days[0].DayIndexInMonth);
            Assert.AreEqual(DayOfWeek.Tuesday, createdMonth.Days[1].DayOfWeek);
        }
    }
}