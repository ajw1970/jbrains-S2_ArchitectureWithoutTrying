namespace PointOfSaleTests
{
    public class Price
    {
        public static Price Cents(int centsValue)
        {
            return new Price();
        }

        public override string ToString()
        {
            return "a Price";
        }
    }
}