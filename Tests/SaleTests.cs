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
            var sale = new Sale(display);

            sale.OnBarcode(new object());

            display.Text.Should().Be("$7.25");
        }

    }

    public class Sale
    {
        private readonly Display display;

        public Sale(Display display)
        {
            this.display = display;
        }

        public void OnBarcode(object barcode)
        {
            display.Text = "$7.25";
        }
    }


    public class Display
    {
        public string Text { get; set; }

        public void DisplayPriceForBarcode(object barcode)
        {
            Text = "$7.25";
        }
    }
}
