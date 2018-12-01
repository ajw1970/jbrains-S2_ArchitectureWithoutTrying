using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
            var irrelevantPrice = Price.Cents(795);
            var catalogStub = new CatalogStub(knownBarcode: "12345", knownPrice: irrelevantPrice);
            var displaySpy = new DisplaySpy();
            var saleController = new SaleController(catalogStub, displaySpy);

            saleController.OnBarcode("12345");

            DisplaySpy.DisplayPriceCalledWith.Should().Be(irrelevantPrice);
        }

        [Fact]
        public void ProductNotFound()
        {
            var productNeverFoundCatalog = new CatalogDummy();
            var displaySpy = new DisplaySpy();
            var saleController = new SaleController(productNeverFoundCatalog, displaySpy);

            saleController.OnBarcode("::product not found::");

            DisplaySpy.DisplayProductNotFoundCalledWith.Should().Contain("::product not found::");
        }

        [Fact]
        public void EmptyBarcode()
        {
            var displaySpy = new DisplaySpy();
            var saleController = new SaleController(null, displaySpy);
            
            saleController.OnBarcode("");
            
            DisplaySpy.DisplayEmptyBarcodeCalled.Should().BeTrue();
        }
        
        public class CatalogDummy : ICatalog
        {
            public Price FindPrice(string barcode)
            {
                return null;
            }
        }

        public class Price
        {
            public static Price Cents(int centsValue)
            {
                return new Price();
            }

            public override string ToString()
            {
                return "a Price";
            }
        }

        public interface ICatalog
        {
            Price FindPrice(string barcode);
        }

        public class CatalogStub : ICatalog
        {
            private readonly PricedBarcode knownBarcode;

            public CatalogStub(string knownBarcode, Price knownPrice)
            {
                this.knownBarcode = new PricedBarcode(knownBarcode, knownPrice);
            }

            public Price FindPrice(string barcode)
            {
                if (knownBarcode.Barcode == barcode)
                    return knownBarcode.Price;
                else
                {
                    return null;
                }
            }

            private class PricedBarcode
            {
                public string Barcode { get; }
                public Price Price { get; }

                public PricedBarcode(string barcode, Price price)
                {
                    Barcode = barcode;
                    Price = price;
                }
            }
        }

        public interface IDisplay
        {
            void DisplayPrice(Price price);
            void DisplayProductNotFound(string barcode);
            void DisplayEmptyBarcodeMessage();
        }

        public class DisplaySpy : IDisplay
        {
            public void DisplayPrice(Price price)
            {
                DisplayPriceCalledWith = price;
            }

            void IDisplay.DisplayProductNotFound(string barcode)
            {
                DisplayProductNotFoundCalledWith = barcode;
            }

            public void DisplayEmptyBarcodeMessage()
            {
                DisplayEmptyBarcodeCalled = true;
            }

            public static Price DisplayPriceCalledWith { get; private set; }
            public static string DisplayProductNotFoundCalledWith { get; private set; }
            public static bool DisplayEmptyBarcodeCalled { get; private set; }
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