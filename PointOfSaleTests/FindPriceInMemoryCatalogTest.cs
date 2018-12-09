using System.Collections.Generic;

namespace PointOfSaleTests
{
    public class FindPriceInMemoryFindPriceInCatalogTest : FindPriceInCatalogContract
    {
        protected override ICatalog CatalogWith(string barcode, Price price)
        {
            return new InMemoryCatalog(new Dictionary<string, Price> { {barcode, price} });
        }

        protected override ICatalog CatalogWithout(string barcodeToAvoid)
        {
            return new InMemoryCatalog(new Dictionary<string, Price>
            {
                {"Anything but " + barcodeToAvoid, Price.Cents(0)}
            });
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