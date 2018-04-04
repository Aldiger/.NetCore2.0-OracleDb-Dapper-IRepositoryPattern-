using DataAccess.Entity;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using NetCoreOracle.Model;

namespace NetCoreOracle.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var data = _repository.GetList();
           
            return Ok(data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _repository.GetProduct(id);
            if (data==null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var prod = new Product
                {
                    Name = product.Name,
                    Model = product.Model,
                    Price = product.Price
                };
                if (_repository.Add(prod)>0)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        // POST api/values
        [HttpPost("[action]")]
        public IActionResult PostWithProcedure([FromBody]ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var prod = new Product
                {
                    Name = product.Name,
                    Model = product.Model,
                    Price = product.Price
                };
                if (_repository.AddWithProcedure(prod) > 0)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProductModel p)
        {
            if (ModelState.IsValid)
            {
                if (id==p.Id)
                {
                    var result=_repository.EditProduct(new Product { Model = p.Model, Name = p.Name, Price = p.Price,Id=p.Id });
                    if (result>0)
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.DeleteProdcut(id)>0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
