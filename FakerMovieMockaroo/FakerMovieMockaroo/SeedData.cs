using FakerMovieMockaroo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FakerMovieMockaroo
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FakerMovieMockarooContext(
                serviceProvider.GetRequiredService<DbContextOptions<FakerMovieMockarooContext>>()))
            {
                if (context.Movie.Any())
                {
                    return;
                }

                context.Add()

                context.SaveChanges();
            }
        }

        public static async Task<List<Movie>> GetRandomMovie()
        {
            MockarooMovieRequest request = new MockarooMovieRequest()
            {
                Count = 10,
                AlwaysArray = true,
                Movies = new List<Field>()
            };

            request.Movies.Add(new Field { Name = "movieTitle", Type = "Movie Title" });
            request.Movies.Add(new Field { Name = "releaseDate", Type = "Date", Min = "1/1/1900", Max = "1/1/2100", Format = "%d/%m/%y" });
            request.Movies.Add(new Field { Name = "genre", Type = "Movie Genre" });
            request.Movies.Add(new Field { Name = "price", Type = "Money", Symbol = "random"});

            using (var client = new HttpClient())
            {
                try
                {
                    HttpClient http = new HttpClient();
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string postBody = JsonConvert.SerializeObject(request);
                    client.BaseAddress = new Uri("https://mockaroo.com/api/generate.json?key=a7d78710");
                    var response = await client.PostAsync($"&fields=", new StringContent(postBody, Encoding.UTF8));
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawMovie = JsonConvert.DeserializeObject<MockarooMovieRequest>(stringResult);

                    List

                    return new OkObjectResult(rawMovie);
                }
                catch(HttpRequestException httpRequestException)
                {
                    return "Error generating random movie";
                }
            }
        }
    }
}
