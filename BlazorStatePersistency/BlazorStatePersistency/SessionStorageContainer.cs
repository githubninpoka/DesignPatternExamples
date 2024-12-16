using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Hosting.Server;

namespace BlazorStatePersistency
{
    public class SessionStorageContainer<T>
    {
        private readonly ProtectedSessionStorage _pss;


        public SessionStorageContainer(ProtectedSessionStorage pss)
        {
            _pss = pss;
        }

        public async Task SetAsync(T? item)
        {
            if (item != null)
            {
                await _pss.SetAsync("item", item);
            }
        }
        public async Task<T?> GetAsync()
        {
            var itemResult = await _pss.GetAsync<T>("item");

            if (itemResult.Success && itemResult.Value is T)
            {
                return itemResult.Value;

            }
            else
            {
                return default;
            }
        }
        public async Task DeleteAsync()
        {
            await _pss.DeleteAsync("item");
        }
    }
}
