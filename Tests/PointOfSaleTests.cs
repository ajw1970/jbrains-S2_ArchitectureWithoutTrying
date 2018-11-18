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
    }

    public interface IDisplay
    {
        void Display(string message);
    }

    public class DisplaySpy : IDisplay
    {
        public void Display(string message)
        {
            Displayed = message;
        }

        public string Displayed { get; private set; }
    }

    public class PointOfSaleTerminal
    {
        public PointOfSaleTerminal(IDisplay display)
        {
            display.Display("Welcome!");
        }
    }
}
