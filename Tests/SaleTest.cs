using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Xunit;
using FluentAssertions;

namespace Tests
{
    public class SellOneItemTest
    {
        [Fact]
        public void ProductFound()
        {
            var display = new Display();
            var sale = new Sale();
            
            sale.OnBarcode(new Barcode("12345"));
            
            display.GetText().Should().Be("$7.95");
        }

        public class Barcode
        {
            public Barcode(string value)
            {
            }
        }

        public class Sale
        {
            public Sale()
            {
            }

            public void OnBarcode(Barcode barcode)
            {
            }
        }

        public class Display
        {
            public string GetText() => "$7.95";
        }
    }
}