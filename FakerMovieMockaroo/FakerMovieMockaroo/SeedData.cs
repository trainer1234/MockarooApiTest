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

                var result = GetRandomMovie();
                List<Movie> generatedMovies = result.Result;
                context.AddRange(generatedMovies);

                context.SaveChanges();
            }
        }

        public static async Task<List<Movie>> GetRandomMovie()
        {
            MockarooMovieRequest request = new MockarooMovieRequest();

            request.Fields = new List<MovieField>();
            request.Fields.Add(new MovieField() { name = "Title", type = "Movie Title" });

            request.Fields.Add(new MovieField() { name = "ReleaseDate", type = "Date", min = "1/1/1900", max = "1/1/2050", format = "%m/%d/%Y" });
            request.Fields.Add(new MovieField() { name = "Genre", type = "Movie Genres" });
            request.Fields.Add(new MovieField() { name = "Price", type = "Number", decimals = 2, min = "1", max = "1000000000"});
            request.Fields.Add(new MovieField() { name = "Currency", type = "Currency" });

            var entitiesJson = new List<object>();
            for(int i = 0; i < request.Fields.Count; i++)
            {
                entitiesJson.Add(request.Fields[i]);
            }

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string postBody = JsonConvert.SerializeObject(entitiesJson);

                    client.BaseAddress = new Uri("https://mockaroo.com/api/generate.json?key=a7d78710&count=1000&array=true");
                    var content = new StringContent(postBody, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(client.BaseAddress, content);
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawMovie = JsonConvert.DeserializeObject<List<Movie>>(stringResult);
                    
                    return rawMovie;
                }
                catch(HttpRequestException httpRequestException)
                {
                    throw;
                }
            }
        }
    }
}
