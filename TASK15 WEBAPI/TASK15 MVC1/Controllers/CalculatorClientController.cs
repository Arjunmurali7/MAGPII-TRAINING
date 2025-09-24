using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CalculatorMvc.Controllers //namespace of the controller
{
    public class CalculatorClientController : Controller  //inherit from controller class
    {
        private readonly HttpClient _client;//declare httpclient recieve and send request to the api

        //httpclientfactory intrfce help to create and manage instance This configures the "CalculatorApi" client so that every time you call CreateClient("CalculatorApi"), you get a pre-configured HttpClient.
        public CalculatorClientController(IHttpClientFactory factory)//injecting the factory constructor 
        {
            _client = factory.CreateClient("CalculatorApi");//create an instance of httpclient using the factory 
        }

        public IActionResult Index() => View(); //return the view

        [HttpPost]
        public async Task<IActionResult> Add(int a, int b) //async method to handle post request
        {
            var result = await _client.GetFromJsonAsync<int>($"api/calculator/add?a={a}&b={b}"); //send get request to the api and get the result
            ViewBag.Result = result; //store the result in viewbag to display in the view
            ViewBag.Operation = $"{a} + {b} ="; //store the result in viewbag to display in the view
            return View("Index"); //return the view with the result
        }

        [HttpPost]
        public async Task<IActionResult> Subtract(int a, int b) //async method to handle post request
        {
            var result = await _client.GetFromJsonAsync<int>($"api/calculator/subtract?a={a}&b={b}"); //send get request to the api and get the result
            ViewBag.Result = result;
            ViewBag.Operation = $"{a} - {b} =";
            return View("Index");
        }

    }
}
