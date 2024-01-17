using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelationalHumanResources.Dtos;
using RelationalHumanResources.Models;
using RelationalHumanResources.Services;

namespace RelationalHumanResources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IGenService<Employee, long> _emplService;
        private readonly ILogger<ClientController> _logger;

        private readonly IGenService<Project, long> _projService;

        public ClientController(IGenService<Employee, long> service, 
            ILogger<ClientController> logger, IGenService<Project, 
                long> projService)
        {
            _emplService = service;
            _logger = logger;
            _projService = projService;
        }

        [HttpPost]
        public Task<ApiResult<Employee>> CreateAsync(Employee employee)
        {
            return _emplService.CreateAsync(employee);
        }

    }
}
