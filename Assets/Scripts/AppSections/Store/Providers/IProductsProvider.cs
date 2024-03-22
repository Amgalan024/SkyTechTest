using AppSections.Store.Models;
using Cysharp.Threading.Tasks;

namespace AppSections.Store.Providers
{
    public interface IProductsProvider
    {
        UniTask<Products> GetProducts();
    }
}