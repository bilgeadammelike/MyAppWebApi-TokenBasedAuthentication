using MyAppWebApi.Apis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyAppWebApi.Apis.Controllers
{
    public class TestController : ApiController
    {


        // [Authorize(Roles = "user")] //kullanıcı rolüne sahip olanları istersek sadece user erişsin diye de kullanılabilir.

        [HttpPost]
        [Authorize] //Authorize attribute token olmayanların erişimini engeller
        public List<MyData> Post()
        {
            return new List<MyData> { new MyData() { Name = "Test1" }, new MyData() { Name = "Test2" }, new MyData() { Name = "Tes3" } };
        }
    }
}
