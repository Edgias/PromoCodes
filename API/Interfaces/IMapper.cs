namespace TheRoom.PromoCodes.API.Interfaces
{
    public interface IMapper<TEntity, TRequest, TResponse>
    {
        TEntity Map(TRequest request);

        TResponse Map(TEntity entity);

        void Map(TEntity entity, TRequest request);

    }
}
