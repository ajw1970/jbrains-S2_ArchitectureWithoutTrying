using FluentAssertions;
using Xunit;

namespace Tests
{
    public class BarcodeWholeValueTest
    {
        [Fact]
        public void CompareEqualValues()
        {
            var barcode1 = new Barcode("12345");
            var barcode2 = new Barcode("12345");

            barcode1.Equals(barcode2).Should().BeTrue();
        }
        
        [Fact]
        public void CompareDifferentValues()
        {
            var barcode1 = new Barcode("12345");
            var barcode2 = new Barcode("23456");

            barcode1.Equals(barcode2).Should().BeFalse();
        }
    }
}