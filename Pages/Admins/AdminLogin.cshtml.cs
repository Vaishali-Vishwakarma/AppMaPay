using AppMaPay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace AppMaPay.Pages.Admins
{
    public class AdminLoginModel : PageModel
    {
        public void OnGet()
        {
        }

        [BindProperty]
        public Admin admin { get; set; }

        HttpClient httpClient = new HttpClient();

        AdminDetails adminDetails = new AdminDetails();
        
        public async Task<IActionResult> OnPostAsync(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                httpClient.BaseAddress = new Uri("https://localhost:7182/api/admin");
                var responselogin = httpClient.PostAsJsonAsync<Admin>("admin", admin);
                responselogin.Wait();
                var resultlogin = responselogin.Result;
                
                var result = resultlogin.Content.ReadAsStringAsync().Result;
                
                //if (adminDetails.result.result)
                if(resultlogin.IsSuccessStatusCode)
                {
                    adminDetails = JsonConvert.DeserializeObject<AdminDetails>(result);
                    return Redirect("/Admins/dashboard");
                }
                else
                {
                    //return BadRequest(adminDetails.result.message);
                    return BadRequest(result);
                }
            }
        }
    }
}
