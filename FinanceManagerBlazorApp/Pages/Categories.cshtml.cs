using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace FinanceManagerBlazorApp.Pages
{
    public class CategoriesModel : PageModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public void OnGet()
        {
        }

        public async Task RefreshList()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7016/api/Category/GetCategories");
            client.Dispose();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            Categories = await JsonSerializer.DeserializeAsync<IEnumerable<Category>>(responseStream);
        }
    }
}
