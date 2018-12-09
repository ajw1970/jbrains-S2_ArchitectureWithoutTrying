namespace PointOfSaleTests
{
    public interface ICatalog
    {
        Price FindPrice(string barcode);
    }
}