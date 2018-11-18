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
        [Fact]
        public void StartsWithGreeting()
        {
            DisplaySpy display = new DisplaySpy();
            var pos = new PointOfSaleTerminal(display);

            display.Displayed.Should().Be("Welcome!");
        }

        [Fact]
        public void UnrecognizedBarCode()
        {
            DisplaySpy display = new DisplaySpy();
            var pos = new PointOfSaleTerminal(display);

            pos.OnBarcode("bad");

            display.Displayed.Should().Be("Error");
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

        public PointOfSaleTerminal(IDisplay display)
        {
            this.display = display;
            this.display.Show("Welcome!");
        }

        public void OnBarcode(string bad)
        {
            display.Show("Error");
        }
    }
}
