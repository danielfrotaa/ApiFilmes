using ApiMovies.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiMovies.Services
{
    public class GetMoviesService
    {
        public List<Datum> getMovies(string title, int page)
        {
            var listMovies = new List<Datum>();
            using (var http = new HttpClient())
            {

                var url = new Uri($"https://jsonmock.hackerrank.com/api/movies/search/?Title={title}&page={page}");
                var result = http.GetAsync(url).GetAwaiter().GetResult();
                var resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var response = JsonConvert.DeserializeObject<Movies>(resultContent);

                    foreach (var item in response.Data)
                    {
                        listMovies.Add(item);
                    }
                    return listMovies;
                }
                return null;
            }
        }

        public object Get(string title)
        {
            var listMovies = new List<Datum>();
            using (var http = new HttpClient())
            {

                var url = new Uri($"https://jsonmock.hackerrank.com/api/movies/search/?Title={title}&page=1");
                var result = http.GetAsync(url).GetAwaiter().GetResult();
                var resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var response = JsonConvert.DeserializeObject<Movies>(resultContent);
                    for (int i = 1; i <= response.total_pages; i++)
                    {
                        var filmes = getMovies(title, i);

                        foreach (var item in filmes)
                        {
                            listMovies.Add(item);
                        }
                    }

                    var teste = from a in listMovies
                           group a by a.Year into g
                           select new { year = g.Key, movies = g.Count()};

                    return new { moviesByYears = teste, total = listMovies.Count() };
                }
                return null;
            }
        }



    }
}
