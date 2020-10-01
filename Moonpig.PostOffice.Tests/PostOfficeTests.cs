﻿using System;
using System.Collections.Generic;
using Moonpig.PostOffice.Api.Controllers;
using Moonpig.PostOffice.Core;
using Shouldly;
using Xunit;

namespace Moonpig.PostOffice.Tests
{
    public class PostOfficeTests
    {
        private readonly DespatchDateController _controller;

        public PostOfficeTests()
        {
            _controller = new DespatchDateController();
        }

        public class SingleProductLeadTimeIsAddedToDespatchDate : PostOfficeTests
        {
            public SingleProductLeadTimeIsAddedToDespatchDate()
            {
                _mondayOrderDate = new DateTime(2020, 9, 28);
            }

            private readonly DateTime _mondayOrderDate;

            [Fact]
            public void MondayOrder_WithLeadTimeOfOneDay_HasDespathDateOfTuesday()
            {
                var date = _controller.Get(DespatchDateRequest.SingleProduct(1, _mondayOrderDate));
                date.Date.Date.ShouldBe(_mondayOrderDate.Date.AddDays(1));
            }

            [Fact]
            public void MondayOrder_WithLeadTimeOfThreeDays_HasDespathDateOfThursday()
            {
                var date = _controller.Get(DespatchDateRequest.SingleProduct(3, _mondayOrderDate));
                date.Date.Date.ShouldBe(_mondayOrderDate.Date.AddDays(3));
            }

            [Fact]
            public void MondayOrder_WithLeadTimeOfTwoDays_HasDespathDateOfWednesday()
            {
                var date = _controller.Get(DespatchDateRequest.SingleProduct(2, _mondayOrderDate));
                date.Date.Date.ShouldBe(_mondayOrderDate.Date.AddDays(2));
            }
        }

        public class SupplierWithLongestLeadTimeIsUsedForCalculation : PostOfficeTests
        {
            [Fact]
            public void MondayOrder_WithMaxLeadTimeOfTwoDays_HasDespathDateOfWednesday()
            {
                var mondayOrderDate = new DateTime(2020, 9, 28);
                var date = _controller.Get(new DespatchDateRequest(new List<int> {1, 2}, mondayOrderDate));
                date.Date.Date.ShouldBe(mondayOrderDate.Date.AddDays(2));
            }
        }

        public class LeadTimeIsNotCountedOverAWeekend : PostOfficeTests
        {
            [Fact]
            public void ThursdayOrder_WithLeadTimeOfThreeDay_HasDespatchDateOfTuesday()
            {
                var orderDate = new DateTime(2020, 10, 1);
                var date = _controller.Get(DespatchDateRequest.SingleProduct(3, orderDate));
                date.Date.ShouldBe(orderDate.Date.AddDays(5));
            }

            [Fact]
            public void FridayOrder_WithLeadTimeOfOneDay_HasDespatchDateOfMonday()
            {
                var orderDate = new DateTime(2020, 10, 2);
                var date = _controller.Get(DespatchDateRequest.SingleProduct(1, orderDate));
                date.Date.ShouldBe(orderDate.Date.AddDays(3));
            }
        }
    }
}