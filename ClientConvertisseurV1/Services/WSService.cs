using ClientConvertisseurV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace ClientConvertisseurV1.Services
{
    public class WSService : IService
    {
        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        public WSService(string uri)
        {

            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Devise>> GetDevisesAsync(string nomControleur)
        {
            try
            {
                return await client.GetFromJsonAsync<List<Devise>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
