using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq; // Asegúrate de instalar Newtonsoft.Json

[ApiController]
[Route("api/[controller]")]
public class GiphyController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private static List<string> _gifList = new List<string>();
    private const string GiphyApiKey = "wEjmHI9VfhZ4tUg3y7cmfk76qEVc9TVA"; // 
    private const int GifLimit = 20; // Número a obtener

    public GiphyController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        LoadGifs().Wait(); // cargar los gifs
         }

    // cargar gifs
    private async Task LoadGifs()
    {
        if (!_gifList.Any())
        {
            var apiUrl = $"https://api.giphy.com/v1/gifs/trending?api_key={GiphyApiKey}&limit={GifLimit}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var json = JObject.Parse(response);
            _gifList = json["data"].Select(gif => gif["images"]["original"]["url"].ToString()).ToList();
        }
    }

    [HttpGet("{number}")]
    public IActionResult GetGif(int number)
    {
        if (number < 1 || number > GifLimit)
        {
            return BadRequest("Número fuera de rango. Debe estar entre 1 y 20.");
        }

        var gifUrl = _gifList[number - 1]; // Ajusta el índice para que sea 0-based
        return Ok(new { Url = gifUrl });
    }
}
