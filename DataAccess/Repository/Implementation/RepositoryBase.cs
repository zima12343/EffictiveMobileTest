namespace DataAccess.Repository.Implementation
{
    public class RepositoryBase
    {
        public OrdersContext DB { get; }

        public RepositoryBase(OrdersContext context)
        {
            DB = context;
        }

    }
}
