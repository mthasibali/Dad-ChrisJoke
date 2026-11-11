using ChuckNorrisJokesApp.Models;
using ChuckNorrisJokesApp.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;



namespace ChuckNorrisJokesApp.Services
{




    public class ChuckNorrisApiHttpService : IChuckNorrisApiService
    {
        private static readonly HttpClient _sharedClient = new HttpClient();

        private HttpClient _httpClient;
        private IConfigurationService _configurationService;
        public ChuckNorrisApiHttpService(IConfigurationService service)
        {
            _configurationService = service;

            _httpClient = new HttpClient();
        }



        public async Task<string> GetJokeAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_configurationService.ChuckNorrisApiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();




                ChuckNorrisJoke joke = JsonSerializer.Deserialize<ChuckNorrisJoke>(jsonContent);

                if (joke != null)
                {
                    return joke.value;
                }


            }

            return null;
        }
        public async Task<string> GetRandomJokeAsync()
        {
            try
            {
                // Build a specific request message so headers don't leak into your other API calls
                var request = new HttpRequestMessage(HttpMethod.Get, "https://icanhazdadjoke.com");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("User-Agent", "MyDotNetMauiApp (https://github.com)");

                var response = await _sharedClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jokeData = await response.Content.ReadFromJsonAsync<DadJoke>();
                return jokeData?.Status == 200 ? jokeData.Joke : "No joke found.";
            }
            catch
            {
                return "Failed to load joke.";
            }
        }

    }
}