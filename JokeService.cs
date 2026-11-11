using ChuckNorrisJokesApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace ChuckNorrisJokesApp.Services
{
    
        public class JokeService
        {
            private readonly HttpClient _httpClient;

            // The factory injects the client configured specifically for jokes
            public JokeService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<string> GetRandomJokeAsync()
            {
                try
                {
                    // Because BaseAddress is already set to https://icanhazdadjoke.com, pass an empty string or "/"
                    var response = await _httpClient.GetFromJsonAsync<DadJoke>("");
                    return response?.Status == 200 ? response.Joke : "No joke found.";
                }
                catch
                {
                    return "Failed to load joke.";
                }
            }
        }
}
