namespace API.Repositories.Interface;

public interface IRepositories<Entity> where Entity : class
{
    public IEnumerable<Entity> Get();
    public Entity Get(int id);
    public int Insert(Entity entity);
    public int Update(Entity entity);
    public int Delete(int id);
}