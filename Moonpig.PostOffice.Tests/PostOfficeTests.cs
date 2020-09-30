using Moonpig.PostOffice.Core;

namespace Moonpig.PostOffice.Tests
{
    using System;
    using System.Collections.Generic;
    using Api.Controllers;
    using Shouldly;
    using Xunit;

    public class PostOfficeTests
    {
        private DateTime _mondayOrderDate;

        public PostOfficeTests()
        {
            _mondayOrderDate = new DateTime(2020, 9, 28);
        }

        [Fact]
        public void OneProductWithLeadTimeOfOneDay()
        {
            DespatchDateController controller = new DespatchDateController();
            var date = controller.Get(new DespatchDateRequest(new List<int>() {1}, _mondayOrderDate));
            date.Date.Date.ShouldBe(_mondayOrderDate.Date.AddDays(1));
        }

        [Fact]
        public void OneProductWithLeadTimeOfTwoDay()
        {
            DespatchDateController controller = new DespatchDateController();
            var date = controller.Get(new DespatchDateRequest(new List<int>() { 2 }, _mondayOrderDate));
            date.Date.Date.ShouldBe(_mondayOrderDate.Date.AddDays(2));
        }

        [Fact]
        public void OneProductWithLeadTimeOfThreeDay()
        {
            DespatchDateController controller = new DespatchDateController();
            var date = controller.Get(new DespatchDateRequest(new List<int>() { 3 }, _mondayOrderDate));
            date.Date.Date.ShouldBe(_mondayOrderDate.Date.AddDays(3));
        }

        [Fact]
        public void SaturdayHasExtraTwoDays()
        {
            DespatchDateController controller = new DespatchDateController();
            var date = controller.Get(new DespatchDateRequest(new List<int>() { 1 }, new DateTime(2018,1,26)));
            date.Date.ShouldBe(new DateTime(2018, 1, 26).Date.AddDays(3));
        }

        [Fact]
        public void SundayHasExtraDay()
        {
            DespatchDateController controller = new DespatchDateController();
            var date = controller.Get(new DespatchDateRequest(new List<int>() { 3 }, new DateTime(2018, 1, 25)));
            date.Date.ShouldBe(new DateTime(2018, 1, 25).Date.AddDays(4));
        }
    }
}
