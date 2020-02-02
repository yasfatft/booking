using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


[Route("hello")]
[ApiController]
public class ValuesController : ControllerBase 
{
    // GET api/values
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Hello World!";
    }
}
