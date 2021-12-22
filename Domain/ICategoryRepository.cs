namespace Domain
{
    public interface ICategoryRepository
    {
        Category Get(int id);
        List<Category> GetAll();
        Category Add(Category category);
        Category Modify(Category category);
        void Remove(int id);
        Category GetWithProducts(int id);
    }
}
