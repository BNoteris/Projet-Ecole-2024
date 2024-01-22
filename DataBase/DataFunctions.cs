namespace Projet2023;

public interface DataFunctions<T>  //Interface de base pour la création de nouveaux types de personnes dans la database
{
     Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> GetIdByNameAsync(string name);
}
