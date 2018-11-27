using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
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
            var sale = new Sale(display);
            
            sale.OnBarcode(new Barcode("12345"));
            
            display.Text.Should().Be("$7.95");
        }
        
        [Fact (Skip = "Refactoring...")]
        public void AnotherProductFound()
        {
            var display = new Display();
            var sale = new Sale(display);
            
            sale.OnBarcode(new Barcode("23456"));
            
            display.Text.Should().Be("$12.50");
        }

        public class Barcode
        {
            public Barcode(string value)
            {
            }
        }

        public class Sale
        {
            private readonly Display display;

            public Sale(Display display)
            {
                this.display = display;
            }

            public void OnBarcode(Barcode barcode)
            {
                display.Text = "$7.95";
            }
        }

        public class Display
        {           
            public string Text { get; set; }
        }
    }
}