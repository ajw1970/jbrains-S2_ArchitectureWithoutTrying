using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace PointOfSaleTests
{
    public class FindPriceInMemoryCatalogTest
    {
        [Fact]
        public void ProductFound()
        {
            var foundPrice = Price.Cents(1250);
            var catalog = new InMemoryCatalog(new Dictionary<string, Price> { {"12345", foundPrice} });
            catalog.FindPrice("12345").Should().Be(foundPrice);
        }

        [Fact]
        public void ProductNotFound()
        {
            var catalog = new InMemoryCatalog(new Dictionary<string, Price>());
            catalog.FindPrice("::not found::").Should().Be(null);
        }

        public class InMemoryCatalog : ICatalog
        {
            private readonly Dictionary<string, Price> pricesByBarcode;

            public InMemoryCatalog(Dictionary<string, Price> pricesByBarcode)
            {
                this.pricesByBarcode = pricesByBarcode;
            }

            public Price FindPrice(string barcode)
            {
                return pricesByBarcode.ContainsKey(barcode) ? pricesByBarcode[barcode] : null;
            }
        }
    }
}