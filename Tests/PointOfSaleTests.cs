using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;

namespace Tests
{
    public class PointOfSaleTests
    {
        private readonly DisplaySpy display = new DisplaySpy();

        [Fact]
        public void StartsWithGreeting()
        {
            new PointOfSaleTerminal(display, null);

            display.Displayed.Should().Be("Welcome!");
        }

        [Fact]
        public void UnrecognizedBarCode()
        {
            var pos = new PointOfSaleTerminal(display, new InMemoryGateway());

            pos.OnBarcode("bad");

            display.Displayed.Should().Be("Error");
        }

        [Fact]
        public void RecognizedBarCode()
        {
            var gateway = new InMemoryGateway();
            gateway.AddItem(new Item { Barcode = "good", Price = 2.22 });
            var pos = new PointOfSaleTerminal(display, gateway);

            pos.OnBarcode("good");

            display.Displayed.Should().Be("$2.22");
        }
    }

    public class Item
    {
        public string Barcode { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"{Price:C}";
        }

        public class NullItem : Item
        {
            public NullItem()
            {
                Barcode = "";
                Price = 0;
            }

            public override string ToString()
            {
                return "Error";
            }
        }
    }

    public interface IGateway
    {
        Item LookupItemByBarcode(string barcode);
    }

    public class InMemoryGateway : IGateway
    {
        private Item item;

        public InMemoryGateway()
        {
            item = new Item.NullItem();
        }

        public Item LookupItemByBarcode(string barcode)
        {
            if (item.Barcode.Equals(barcode.TrimEnd()))
                return item;
            else
                return new Item.NullItem();
        }

        public void AddItem(Item item)
        {
            this.item = item;
        }
    }

    public interface IDisplay
    {
        void Show(string message);
    }

    public class DisplaySpy : IDisplay
    {
        public void Show(string message)
        {
            Displayed = message;
        }

        public string Displayed { get; private set; }
    }

    public class PointOfSaleTerminal
    {
        private readonly IDisplay display;
        private readonly IGateway gateway;

        public PointOfSaleTerminal(IDisplay display, IGateway gateway)
        {
            this.display = display;
            this.gateway = gateway;
            this.display.Show("Welcome!");
        }

        public void OnBarcode(string barcode)
        {
            var item = gateway.LookupItemByBarcode(barcode);
            display.Show(item.ToString());
        }
    }
}
