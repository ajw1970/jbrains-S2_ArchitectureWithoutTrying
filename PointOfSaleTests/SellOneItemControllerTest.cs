using Xunit;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace PointOfSaleTests
{
    public class SellOneItemControllerTest
    {
        private readonly IDisplay display;

        public SellOneItemControllerTest()
        {
            display = Substitute.For<IDisplay>();
        }

        [Fact]
        public void ProductFound()
        {
            var irrelevantPrice = Price.Cents(795);
            var catalog = Substitute.For<ICatalog>();
            catalog.FindPrice("::product found::").Returns(irrelevantPrice);
            var saleController = new SaleController(catalog, display);

            saleController.OnBarcode("::product found::");

            display.Received().DisplayPrice(irrelevantPrice);
        }

        [Fact]
        public void ProductNotFound()
        {
            var productNeverFoundCatalog = Substitute.For<ICatalog>();
            productNeverFoundCatalog.FindPrice("::any product::").ReturnsNullForAnyArgs();
            var saleController = new SaleController(productNeverFoundCatalog, display);

            saleController.OnBarcode("::product not found::");
            
            display.Received().DisplayProductNotFound(Arg.Is<string>(a => a.Contains("::product not found::")));
        }

        [Fact]
        public void EmptyBarcode()
        {
            var saleController = new SaleController(null, display);
            
            saleController.OnBarcode("");
            
            display.Received().DisplayEmptyBarcodeMessage();
        }

        public interface IDisplay
        {
            void DisplayPrice(Price price);
            void DisplayProductNotFound(string barcode);
            void DisplayEmptyBarcodeMessage();
        }

        public class SaleController
        {
            private readonly ICatalog catalog;
            private readonly IDisplay display;

            public SaleController(ICatalog catalog, IDisplay display)
            {
                this.catalog = catalog;
                this.display = display;
            }

            public void OnBarcode(string barcode)
            {
                if ("".Equals(barcode))
                {
                    display.DisplayEmptyBarcodeMessage();
                    return;
                }
                
                var price = catalog.FindPrice(barcode);
                if (price == null)
                    display.DisplayProductNotFound(barcode);
                else
                    display.DisplayPrice(price);
            }
        }
    }
}