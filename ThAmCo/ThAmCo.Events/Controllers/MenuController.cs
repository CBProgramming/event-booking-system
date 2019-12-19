using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ThAmCo.Events.Data;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Controllers
{
    public class MenuController : Controller
    {
        private readonly EventsDbContext _context;
        private readonly IConfiguration _configuration;

        public MenuController(EventsDbContext context,
                                IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["MenusBaseURI"]);
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);
            IEnumerable<MenuDto> menus;
            try
            {
                string uri = "/api/Menu";
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                menus = await response.Content.ReadAsAsync<List<MenuDto>>();
            }
            catch (HttpRequestException e)
            {
                menus = Array.Empty<MenuDto>();
            }

            return View(menus);
        }
    }
}