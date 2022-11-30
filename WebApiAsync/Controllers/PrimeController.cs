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

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<ActionResult> GetAsync([FromRoute] int id) {
        await Task.CompletedTask;
        return Ok("Action result = " + id);
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
    public async Task<ActionResult> Process([FromBody] int id)
    {
        await _productManager.Add(new Product { Id = id, Name = Guid.NewGuid().ToString()}, CancellationToken.None);

        return AcceptedAtAction(nameof(Pooling), new { id = id }, id);
    }


    [HttpGet("Pooling/{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Pooling([FromRoute] int id)
    {
        return null;
    }
}
