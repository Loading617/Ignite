using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using InstagramFeedViewer.Models;

public class InstagramController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _accessToken = "<YOUR_LONG_LIVED_ACCESS_TOKEN>";
    private readonly string _instagramUserId = "<YOUR_INSTAGRAM_USER_ID>";

    public InstagramController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<IActionResult> Index()
    {
        var url = $"https://graph.instagram.com/{_instagramUserId}/media?fields=id,caption,media_url,permalink,media_type,timestamp&access_token={_accessToken}";

        var response = await _httpClient.GetStringAsync(url);
        var json = JsonDocument.Parse(response);

        var posts = new List<InstagramPost>();
        foreach (var item in json.RootElement.GetProperty("data").EnumerateArray())
        {
            posts.Add(new InstagramPost
            {
                Id = item.GetProperty("id").GetString(),
                Caption = item.GetProperty("caption").GetString(),
                MediaUrl = item.GetProperty("media_url").GetString(),
                Permalink = item.GetProperty("permalink").GetString(),
                MediaType = item.GetProperty("media_type").GetString(),
                Timestamp = item.GetProperty("timestamp").GetDateTime()
            });
        }

        return View(posts);
    }
}
