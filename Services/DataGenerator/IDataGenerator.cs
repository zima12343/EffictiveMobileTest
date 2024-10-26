namespace Services.DataGenerator
{
    public interface IDataGenerator
    {
        Task GenerateAreaWithOrders(int areaAmount, int amountInArea = 25);
    }
}
