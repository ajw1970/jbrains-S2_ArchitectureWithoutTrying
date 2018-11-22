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

        [Fact]
        public void NullBarcode()
        {
            var display = new Display();
            var sale = new Sale(display);

            sale.OnBarcode(null);

            display.Text.Should().Be("Error: No Barcode");
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
