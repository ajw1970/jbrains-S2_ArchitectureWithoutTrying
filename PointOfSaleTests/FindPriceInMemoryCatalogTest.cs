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
            var catalog = CatalogWith("12345", foundPrice);
            catalog.FindPrice("12345").Should().Be(foundPrice);
        }

        private static ICatalog CatalogWith(string barcode, Price price)
        {
            return new InMemoryCatalog(new Dictionary<string, Price> { {barcode, price} });
        }

        [Fact]
        public void ProductNotFound()
        {
            var catalog = CatalogWithout("12345");
            catalog.FindPrice("12345").Should().Be(null);
        }
        
        private static ICatalog CatalogWithout(string barcodeToAvoid)
        {
            return new InMemoryCatalog(new Dictionary<string, Price>());
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