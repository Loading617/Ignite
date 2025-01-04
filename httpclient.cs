using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class InstagramPost
{
    public string Id { get; set; }
    public string Caption { get; set; }
    public string Media_Type { get; set; }
    public string Media_Url { get; set; }
    public string Timestamp { get; set; }
}

public class InstagramFeed
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<List<InstagramPost>> GetInstagramFeedAsync(string accessToken)
    {
        string url = $"https://graph.instagram.com/me/media?fields=id,caption,media_type,media_url,timestamp&access_token={accessToken}";

        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<InstagramApiResponse>(jsonResponse);
            return result.Data;
        }
        else
        {
            throw new Exception($"API Error: {response.StatusCode}");
        }
    }
}

public class InstagramApiResponse
{
    public List<InstagramPost> Data { get; set; }
}
