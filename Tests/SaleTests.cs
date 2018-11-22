using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;

namespace Tests
{
    public class SaleOneItemTests
    {
        private readonly Display display;
        private readonly Dictionary<object, string> catalog;
        private readonly Sale sale;

        public SaleOneItemTests()
        {
            display = new Display();
            catalog = new Dictionary<object,string>();
            sale = new Sale(display, catalog);
        }

        [Fact]
        public void ProjectFound()
        {
            var barcode1 = new object();
            catalog.Add(barcode1, "$7.25");

            sale.OnBarcode(barcode1);

            display.Text.Should().Be("$7.25");
        }

        [Fact]
        public void NullBarcode()
        {
            sale.OnBarcode(null);

            display.Text.Should().Be("Error: No Barcode");
        }

        [Fact]
        public void AnotherProductFound()
        {
            var barcode1 = new object();
            catalog.Add(barcode1, "$7.25");
            var barcode2 = new object();
            catalog.Add(barcode2, "$5.36");

            sale.OnBarcode(barcode2);

            display.Text.Should().Be("$5.36");
        }

        [Fact]
        public void ProductNotFound()
        {
            sale.OnBarcode(new object());
            display.Text.Should().Be("Product Not Found");
        }
    }

    public class Sale
    {
        private readonly Display display;
        private readonly Dictionary<object, string> catalog;

        public Sale(Display display, Dictionary<object, string> catalog)
        {
            this.display = display;
            this.catalog = catalog;
        }

        public void OnBarcode(object barcode)
        {
            if (barcode == null)
            {
                display.Text = "Error: No Barcode";
                return;
            }

            if (catalog.ContainsKey(barcode))
            {
                display.Text = catalog[barcode];
            }
            else
            {
                display.Text = "Product Not Found";
            }
        }
    }


    public class Display
    {
        public string Text { get; set; }
    }
}
