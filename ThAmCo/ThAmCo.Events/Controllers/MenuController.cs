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
            var client = setupClient();
            string uri = "/api/Menu";
            IEnumerable<MenuDto> menus;
            try
            {
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

        public async Task<IActionResult> Edit(int menuId)
        {
            var client = setupClient();
            string uri = "/api/Menu?menuId=" + menuId;
            MenuDto menu;
            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                menu = await response.Content.ReadAsAsync<MenuDto>();
            }
            catch (HttpRequestException e)
            {
                return NotFound();
            }
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MenuId,Name,CostPerHead,Starter,Main,Dessert")] MenuDto menu)
        {
            if (menu.MenuId == 0 || menu == null)
            {
                return NotFound();
            }
            string uri = "/api/Menu/";
            var client = setupClient();
            if((await client.PutAsJsonAsync<MenuDto>(uri, menu)).IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                menu.Message = "Something went wrong, please try again.";
                return RedirectToAction("Edit",menu);
            }
        }

        public HttpClient setupClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["MenusBaseURI"]);
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);
            return client;
        }
    }
}