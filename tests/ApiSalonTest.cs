using System;
using RA;
using Xunit;

namespace test
{
    public class ApiSalonTest
    {
         [Fact]
        public void EmptyNameTest()
        {
             var body =new{
                Name = "",
                SeatHeight = 2, 
                SeatWidth = 3
            };
            new RestAssured()
            .Given()
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("empty name header test", b =>  b == 400)
                .AssertAll();
        }


        [Fact]
        public void EmptySeatHeightTest()
        {
             var body =new{
                Name = "kop", 
                SeatWidth = 3
            };
            new RestAssured()
            .Given()
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("empty seat-height header test", b =>  b == 400)
                .AssertAll();
        }


        [Fact]
        public void EmptySeatWidthTest()
        {
             var body =new{
                Name = "kop",
                SeatHeight = 2, 
            };
            new RestAssured()
            .Given()
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("empty seat-width header test", b =>  b == 400)
                .AssertAll();
        }

        [Fact]
        public void NegetiveseatHeightTest()
        {
             var body =new{
                Name = "kop",
                SeatHeight = -2, 
                SeatWidth = 3
            };
            new RestAssured()
            .Given()
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("negetive seat-height header test", b =>  b == 400)
                .AssertAll();
        }

        [Fact]
        public void NegetiveSeatWidthTest()
        {
             var body =new{
                Name = "kop",
                SeatHeight = 2, 
                SeatWidth = -3
            };
            new RestAssured()
            .Given()
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("negetive seat-width header test", b =>  b == 400)
                .AssertAll();
        }
    }  
}