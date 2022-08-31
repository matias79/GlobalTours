using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LugaresController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public LugaresController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Lugar>>> GeLugares()
        {
            var lugares = await _db.lugar.ToListAsync();
            return Ok(lugares);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lugar>> GeLugar(int id)
        {
            return await _db.lugar.FindAsync(id);
        }
    }
}