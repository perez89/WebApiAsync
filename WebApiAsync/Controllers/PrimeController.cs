using System.Reflection;
using System.Xml.Linq;

namespace WebApiAsync.Controllers;

public class PrimeController : BaseController
{
    private readonly IProductManager _productManager;
    public PrimeController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    [HttpGet("{val}")]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<ActionResult> GetAsync(string val) {
        await Task.CompletedTask;
        return Ok("Action result = " + val);
    }


    [HttpGet("exist/{exist}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]

    public async Task<ActionResult<Product>> GetAsync(bool exist)
    {
        return await Task.Run<ActionResult<Product>>(() =>
        {
            if (exist)
            {               
                return Created("www.test.com/1", new Product { Id = 1, Name = "Perez" });
            }
            else
            {
                return NotFound("Action result not found");
            }
        });    
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]

    public async Task<ActionResult> Process([FromBody] int Id)
    {
        await _productManager.Add(new Product { Id = Id, Name = Guid.NewGuid().ToString()}, CancellationToken.None);

        return Accepted(Id);
    }


    //[HttpGet("Pooling/{Id}{Name}")]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status202Accepted)]

    //public async Task<ActionResult> Pooling([FromRoute] string Id)
    //{
    //    return null;
    //}
}
