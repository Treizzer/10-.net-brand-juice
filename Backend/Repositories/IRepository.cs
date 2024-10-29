namespace Backend.Repositories {

    public interface IRepository<TEntity> {

        Task<IEnumerable<TEntity>> get ();

        Task<TEntity?> getById (int id);

        Task add (TEntity entity);

        void update (TEntity entity);

        void delete (TEntity entity);

        Task save ();

        IEnumerable<TEntity> search (Func<TEntity, bool> filter);

    }

}
