using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;
using ThAmCo.Catering.Models;


namespace ThAmCo.Catering.Controllers
{
    //Resful catering API used for managing menus
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenusDbContext _context;

        public MenuController(MenusDbContext context)
        {
            _context = context;
        }

        //Returnss existing menu based on menu id provided 
        [HttpGet]
        public async Task<IActionResult> Get(int? menuId)
        {
            if (menuId == null)
            {
                var menus = await _context.Menus.ToListAsync();
                List<MenuDto> menusDto = new List<MenuDto>();
                foreach (Menu m in menus)
                {
                    menusDto.Add(new MenuDto(m));
                }
                return Ok(menusDto);
            }
            else
            {
                var menu = await _context.Menus.FindAsync(menuId);
                MenuDto menuDto = new MenuDto(menu);
                return Ok(menuDto);
            }

        }

        //Edits existing menu details based on menuDto provided
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] MenuDto menuDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var menu = await _context.Menus.FindAsync(menuDto.MenuId);
            if (menu != null)
            {
                try
                {
                    menu.Name = menuDto.Name;
                    menu.CostPerHead = menuDto.CostPerHead;
                    menu.Starter = menuDto.Starter;
                    menu.Main = menuDto.Main;
                    menu.Dessert = menuDto.Dessert;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception e)
                {

                }
            }

            return NotFound();
        }

        //Creates new menu based on menuDto provided
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuDto menuDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
            Menu menu = new Menu();
            menu.Name = menuDto.Name;
            menu.CostPerHead = menuDto.CostPerHead;
            menu.Starter = menuDto.Starter;
            menu.Main = menuDto.Main;
            menu.Dessert = menuDto.Dessert;
            _context.Add(menu);
            await _context.SaveChangesAsync();
            return Ok();
            }
            catch (Exception e)
            {

            }
            return NotFound();
        }
    }
}
