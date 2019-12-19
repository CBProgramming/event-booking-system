using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;
using ThAmCo.Catering.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThAmCo.Catering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenusDbContext _context;

        public MenuController(MenusDbContext context)
        {
            _context = context;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var menus = await _context.Menus.ToListAsync();
            List<MenuDto> menusDto = new List<MenuDto>();
            foreach (Menu m in menus)
            {
                menusDto.Add(new MenuDto(m));
            }
            return Ok(menusDto);
        }
    }
}
