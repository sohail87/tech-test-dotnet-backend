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
        private DespatchDateController _controller;

        public PostOfficeTests()
        {
            _controller = new DespatchDateController();
        }

        public class SingleProductLeadTimeIsAddedToDespatchDate : PostOfficeTests
        {
            private readonly DateTime _mondayOrderDate;

            public SingleProductLeadTimeIsAddedToDespatchDate()
            {
                _mondayOrderDate = new DateTime(2020, 9, 28);
            }

            [Fact]
            public void MondayOrder_WithLeadTimeOfOneDay_HasDespathDateOfTuesday()
            {
                var date = _controller.Get(DespatchDateRequest.SingleProduct(1, _mondayOrderDate));
                date.Date.Date.ShouldBe(_mondayOrderDate.Date.AddDays(1));
            }

            [Fact]
            public void MondayOrder_WithLeadTimeOfTwoDays_HasDespathDateOfWednesday()
            {
                var date = _controller.Get(DespatchDateRequest.SingleProduct(2, _mondayOrderDate));
                date.Date.Date.ShouldBe(_mondayOrderDate.Date.AddDays(2));
            }

            [Fact]
            public void MondayOrder_WithLeadTimeOfThreeDays_HasDespathDateOfThursday()
            {
                var date = _controller.Get(DespatchDateRequest.SingleProduct(3, _mondayOrderDate));
                date.Date.Date.ShouldBe(_mondayOrderDate.Date.AddDays(3));
            }
        }

        public class DespatchDateIncorporatesWeekendClosure : PostOfficeTests
        {
        [Fact]
        public void SaturdayHasExtraTwoDays()
        {
            var orderDate = new DateTime(2018,1,26);
            var date = _controller.Get(DespatchDateRequest.SingleProduct(1, orderDate));
            date.Date.ShouldBe(orderDate.Date.AddDays(3));
        }

        [Fact]
        public void SundayHasExtraDay()
        {
            var orderDate = new DateTime(2018, 1, 25);
            var date = _controller.Get(DespatchDateRequest.SingleProduct(3, orderDate));
            date.Date.ShouldBe(orderDate.Date.AddDays(5));
        }
}
        public class SupplierLeadTimeIncorporatesWeekendClosure : PostOfficeTests
        {
            [Fact]
            public void FridayOrderWithMaxLeadTimeOfTwoDaysWillBeFourDays()
            {
                var orderDate = new DateTime(2020, 9, 25);
                var date = _controller.Get(DespatchDateRequest.SingleProduct(2, orderDate));
                date.Date.ShouldBe(orderDate.Date.AddDays(4));
            }
            [Fact]
            public void ThursdayOrderWithMaxLeadTimeOfThreeDaysWillBeFiveDays()
            {
                var orderDate = new DateTime(2020, 10, 1);
                var date = _controller.Get(DespatchDateRequest.SingleProduct(3, orderDate));
                date.Date.ShouldBe(orderDate.Date.AddDays(5));
            }
        }
    }
}
