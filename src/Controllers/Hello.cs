using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace Booking.Controllers
{

    
   
    public class ValuesController : ControllerBase 
    {
        // GET api/values
        [HttpGet]
        // public ActionResult<string> Get()
        // {
        //     return "Hello World!";
        // }
        //[HttpGet, Route("{id}")]
        // [Route("{id}")]
        // public ActionResult<string> GetWithId(int id){
        //     return id.ToString();
        // }
        // [Route("{name}/{id}")]
        // public ActionResult <string> getWithName(string name){
        //     return name;    
        // }
        [Route("hello")]
         
        public IActionResult SayHello(){
            ResponseExample response =new ResponseExample("Hello World");
            return Ok(response);
        }
        [Route("myname")]
        public IActionResult SayName(){
            ResponseExample response=new ResponseExample("Borna");
            return Ok(response);
        }


     

    }
        public class ResponseExample
    {
        
        public string response{get; set;}

        public ResponseExample(string response){
            this.response=response;
        }
    }
}
