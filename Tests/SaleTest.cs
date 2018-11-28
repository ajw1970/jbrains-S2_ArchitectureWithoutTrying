using System;
using System.Collections;
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

        [Fact]
        public void AnotherProductFound()
        {
            var display = new Display();
            var sale = new Sale(display);

            sale.OnBarcode(new Barcode("23456"));

            display.Text.Should().Be("$12.50");
        }

        [Fact]
        public void ProductNotFound()
        {
            Display display = new Display();
            var sale = new Sale(display);

            sale.OnBarcode(new Barcode("99999"));

            display.Text.Should().Be("Product not found for 99999");
        }

        [Fact]
        public void EmptyBarcode()
        {
            Display display = new Display();
            var sale = new Sale(display);

            sale.OnBarcode(new Barcode(""));

            display.Text.Should().Be("Scanning error: Empty barcode");
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
                if (barcode.Equals(new Barcode("")))
                {
                    display.Text = "Scanning error: Empty barcode";
                }
                else
                {
                    IDictionary pricesByBarcode = new Dictionary<Barcode, Price>
                    {
                        {new Barcode("12345"), new Price(7.95)}, 
                        {new Barcode("23456"), new Price(12.50)}
                    };

                    if (barcode == new Barcode("12345"))
                        display.Text = pricesByBarcode[barcode].ToString();
                    else if (barcode == new Barcode("23456"))
                        display.Text = "$12.50";
                    else
                        display.Text = $"Product not found for {barcode}"; 
                }
            }

            public class Price
            {
                private readonly int cents;

                public Price(double dollars)
                {
                    this.cents = (int)(dollars * 100);
                }

                public override string ToString()
                {
                    var dollars = cents / 100.0;
                    return $"{dollars:C}";
                }
            }
        }

        public class Display
        {
            public string Text { get; set; }
        }
    }
}