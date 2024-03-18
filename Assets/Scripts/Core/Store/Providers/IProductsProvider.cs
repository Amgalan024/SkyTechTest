using Core.Store.Models;
using Cysharp.Threading.Tasks;

namespace Core.Store.Providers
{
    public interface IProductsProvider
    {
        UniTask<Products> GetProducts();
    }
}