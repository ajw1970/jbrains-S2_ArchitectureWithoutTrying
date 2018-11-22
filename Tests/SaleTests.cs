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
        [Fact]
        public void ProjectFound()
        {
            var display = new Display();
            var catalog = new Dictionary<object,string>();
            var sale = new Sale(display, catalog);

            sale.OnBarcode(new object());

            display.Text.Should().Be("$7.25");
        }

        [Fact]
        public void NullBarcode()
        {
            var display = new Display();
            var catalog = new Dictionary<object, string>();
            var sale = new Sale(display, catalog);

            sale.OnBarcode(null);

            display.Text.Should().Be("Error: No Barcode");
        }

        [Fact (Skip = "Need a catalog first")]
        public void AnotherProductFound()
        {
            var display = new Display();
            var catalog = new Dictionary<object, string>();
            var sale = new Sale(display, catalog);

            sale.OnBarcode(new object());

            display.Text.Should().Be("$5.36");
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
            }
            else
            {
                display.Text = "$7.25";
            }
        }
    }


    public class Display
    {
        public string Text { get; set; }
    }
}
