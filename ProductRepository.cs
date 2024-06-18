public class ProductRepository
{
    private List<Product> products = [];


    public Product? GetProduct(int productId)
    {

        return products.Find(product => product.Id == productId);
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }
}