using ITS.Solution.Exam._2022.DAL;
using ITS.Solution.Exam._2022.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.Solution.Exam._2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly ILogger<CodeController> _logger;
        private readonly FoodDbContext _ctx;
        private readonly Mapper _mapper;

        public CodeController(ILogger<CodeController> logger, FoodDbContext ctx, Mapper mapper)
        {
            _logger = logger;
            _ctx = ctx;
            _mapper = mapper;
        }

        /// <summary>
        /// Chiamata di test per verificare il corretto raggiungimento del servizio
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{barcode:regex(^[[0-9]]{{13}})}")]
        public IActionResult Get(string barcode)
        {
            Product? product = _ctx.Products.Include(p => p.Category).SingleOrDefault(p => p.Barcode == barcode);
            
            if(product == null)
            {
                return UnprocessableEntity();
            }

            ProductModel model = _mapper.MapEntityToModel(product);
            model.BetterProducts = _ctx.Products
                .Where(p => p.Nutriscore_Value <= product.Nutriscore_Value 
                && p.CategoryId == product.CategoryId
                && p.Barcode != barcode).Take(3).ToList()
                .ConvertAll(_mapper.MapEntityToModel);

            return Ok(model);
        }
    }
}
