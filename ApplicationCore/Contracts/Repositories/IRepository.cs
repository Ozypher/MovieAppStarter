namespace ApplicationCore.Contracts.Repositories;

//We are creating a generic repo interface here where T is going to be a class of entities
// Base Repo is going to have commonly used CRUD methods (Create,Read,Update,Delete)
public interface IRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    T Add(T entity);
    T Update(T entity);
    T Delete(T entity);

}