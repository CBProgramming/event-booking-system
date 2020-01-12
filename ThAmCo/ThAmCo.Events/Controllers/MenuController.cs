using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ThAmCo.Events.Data;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Controllers
{
    //Menu controller to manage menu processes via Catering API
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

        //Returns list of menus, accessed via catering API
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

        //Returns details of menu based on menu id, accessed via catering API
        public async Task<IActionResult> Details(int menuId)
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

        //Returns view of menu details to be edited, accessed via catering API
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

        //Accesses catering API and puts new menu information in record based on menudto provided
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MenuId,Name,CostPerHead,Starter,Main,Dessert")] MenuDto menu)
        {
            if (menu.MenuId == 0 || menu == null)
            {
                return NotFound();
            }
            // prevent menu cost being less than 0
            // done locally rather than on API to prevent the API enforsing business logic on the API consumer
            if (menu.CostPerHead < 0)
            {
                menu.Message = "Menu cost cannot be less than £0";
                return View(menu);
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

        //Reusable method, sets up http client for catering API
        public HttpClient setupClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["MenusBaseURI"]);
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);
            return client;
        }

        //Returns view used to create new menu
        public IActionResult Create(MenuDto menu)
        {
            return View(menu);
        }

        //Accesses catering API and creates new menu record, using menudto provided
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([Bind("Name,CostPerHead,Starter,Main,Dessert")] MenuDto menu)
        {
            if (menu == null)
            {
                return NotFound();
            }
            // prevent menu cost being less than 0
            // done locally rather than on API to prevent the API enforsing business logic on the API consumer
            if (menu.CostPerHead < 0)
            {
                menu.Message = "Menu cost cannot be less than £0";
                return RedirectToAction("Create", menu);
            }
            string uri = "/api/Menu/";
            var client = setupClient();
            if ((await client.PostAsJsonAsync<MenuDto>(uri, menu)).IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                menu.Message = "Something went wrong, please try again.";
                return RedirectToAction("Create", menu);
            }
        }
    }
}