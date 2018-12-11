using FluentAssertions;
using Xunit;

namespace PointOfSaleTests
{
    public class DisplayPricesToConsoleTest
    {
        [Theory]
        [InlineData(789, "$7.89")]
        [InlineData(520, "$5.20")]
        [InlineData(400, "$4.00")]
        [InlineData(0, "$0.00")]
        [InlineData(2, "$0.02")]
        [InlineData(37, "$0.37")]
        [InlineData(418976, "$4,189.76")]
        [InlineData(210832281, "$2,108,322.81")]
        public void Test(int priceInCents, string expectedFormattedPrice)
        {
            Format(Price.Cents(priceInCents)).Should().Be(expectedFormattedPrice);
        }

        private static string Format(Price cents)
        {
            return $"{cents.DollarValue():C}";
        }
    }
}