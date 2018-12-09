using FluentAssertions;
using Xunit;

namespace PointOfSaleTests
{
    public abstract class FindPriceInCatalogContract
    {
        [Fact]
        public void ProductFound()
        {
            var foundPrice = Price.Cents(1250);
            var catalog = CatalogWith("12345", foundPrice);
            AssertionExtensions.Should((object) catalog.FindPrice("12345")).Be(foundPrice);
        }

        protected abstract ICatalog CatalogWith(string barcode, Price price);

        [Fact]
        public void ProductNotFound()
        {
            var catalog = CatalogWithout("12345");
            AssertionExtensions.Should((object) catalog.FindPrice("12345")).Be(null);
        }

        protected abstract ICatalog CatalogWithout(string barcodeToAvoid);
    }
}