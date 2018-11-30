using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;

namespace PointOfSaleTests
{
    public class SellOneItemControllerTest
    {
        [Fact]
        public void ProductFound()
        {
            true.Should().BeTrue();
        }
    }
}
