using EcommShop.DataAccessor.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomShopVers2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SampleController : ControllerBase
    {
        public SampleController()
        {

        }

        [HttpGet("SampleData_NoRole")]
        public IActionResult UserRole()
        {
            return Ok(new {data = "Sample data, no admin role"});
        }

        [Authorize(Roles ="ADMIN")]
        [HttpGet("SampleData_ADMIN_Role")]
        public IActionResult AdminRole()
        {
            return Ok(new { data = "Sample data with ADMIN Role" });
        }
    }
}
