using System;
using Xunit;
using RA;

namespace test
{
    public class ApiTest
    {
        [Fact]
        public void ExpiredStartTimeTest()
        {
            var body = new{
                StartTime = "2017-11-01T00:00:00",
                EndTime="2017-11-02T01:00:00", 
                Summary ="Man of focus...",
                Price ="29.99",
                SalonId="1",
                Title = "John Wick Chapter 3: parabellum"
            };
            new RestAssured()
            .Given()
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("expired start time header test", s =>  s == 400)
                .AssertAll();

        }
        
        [Fact]
        public void StartTimeGreatarThanEndTimeTest()
        {
            var body = new{
                StartTime = "2017-11-02T00:00:00",
                EndTime="2017-11-01T01:00:00", 
                Summary ="Man of focus...",
                Price ="29.99",
                SalonId="1",
                Title = "John Wick Chapter 3: parabellum"
            };
            new RestAssured()
            .Given()
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("conflict show time table header test", s =>  s == 400)
                .AssertAll();
        }

        [Fact]
        public void NegativePriceTest()
        {
             var body = new{
                StartTime = "2020-11-01T01:00:00",
                EndTime="2020-11-01T02:00:00", 
                Summary ="jafarshow",
                Price ="-45",
                SalonId="1",
                Title = "fhdajlk"
            };
            new RestAssured()
            .Given()
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("negative price header test", b =>  b == 400)
                .AssertAll();

        }

        [Fact]
        public void UnvalidEndTimeTest()
        {
            var body = new{
                StartTime = "2020-11-01T02:00:00",
                EndTime="2020-11-01T01  :00:00", 
                Summary ="jafarshow",
                Price ="30",
                SalonId="1",
                Title = "fhdajlk"
                
            };
            new RestAssured()
            .Given()
                .Name("end time test")
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("start time header test", b =>  b == 400)
                // .TestBody("start time body test", b => { Console.WriteLine(b); return true; })
                .AssertAll();
        }

        [Fact]
        public void SalonIdIsNotAvailableTest()
        {
            var body = new{
                StartTime = "2020-11-01T02:00:00",
                EndTime="2020-11-01T01:00:00", 
                Summary ="jafarshow",
                Price ="30",
                SalonId="18",
                Title = "fhdajlk"
                
            };
            new RestAssured()
            .Given()
                .Name("salon id not available test")
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("salon id not available header test", b =>  b == 400)
                .AssertAll();
        }

        [Fact]
        public void TitleCharacterTest()
        {
            var body =new{
                StartTime = "2020-11-01T00:00:00",
                EndTime="2020-11-01T01:00:00", 
                Summary ="moretha",
                Title="woejfnerijnfwirjnfeirjfnerijfnerijfnerifjn",
                Price ="30",
                SalonId="1",

            };
            new RestAssured()
            .Given()
                .Name("title  test")
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("title header test", b =>  b == 400)
                .AssertAll();
        }

        [Fact]
        public void PriceCeilingTest()
        {
            var body =new{
                StartTime = "2020-11-01T00:00:00",
                EndTime="2020-11-01T01:00:00", 
                Summary ="jafarshow",
                Price ="120",
                SalonId="1",
                Title = "fhdajlk"

            };
            new RestAssured()
            .Given()
                .Name("max price test")
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("max price header test", b =>  b == 400)
                .AssertAll();
        }

        [Fact]
        public void MinimumShowTimeTest()
        {
            var body =new{
                StartTime = "2020-11-01T00:00:00",
                EndTime="2020-11-01T18:00:00", 
                Summary ="jafarsh//comparing diffrence between start and end with show timeow",
                Price ="80",
                SalonId="1",
                Title = "fhdajlk"
            };
            new RestAssured()
            .Given()
                .Name("min show time test")
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("min show time header test", b =>  b == 400)
                .AssertAll();
        }

        [Fact]
        public void MaximumShowTimeTest()
        {
             var body =new{
                StartTime = "2020-11-01T00:00:00",
                EndTime="2020-11-01T00:15:00", 
                Summary ="jafarshow",
                Price ="80",
                SalonId="1",
                Title = "fhdajlk"
            };
            new RestAssured()
            .Given()
                .Name("max show time  test")
                .Header("Content-Type","application/json")
                .Header("Accept-Encoding","utf-8")
                .Body(body.ToString())
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("max show time header test", b =>  b == 400)
                .AssertAll();
        }
    }
}