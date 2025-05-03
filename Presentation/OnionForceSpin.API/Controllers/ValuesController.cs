using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionForceSpin.Application.Interfaces.UnitOfWorks;
using OnionForceSpin.Domain.Entities;

namespace OnionForceSpin.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await unitOfWork.GetReadRepository<Product>().GetAllAsync());
        }
    }
}
