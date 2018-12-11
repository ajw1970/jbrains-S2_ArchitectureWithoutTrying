namespace PointOfSaleTests
{
    public class Price
    {
        private readonly int centsValue;

        public Price(int centsValue)
        {
            this.centsValue = centsValue;
        }

        public static Price Cents(int centsValue)
        {
            return new Price(centsValue);
        }

        public override string ToString()
        {
            return "a Price";
        }

        public double DollarValue()
        {
            return centsValue / 100.0d;
        }
    }
}