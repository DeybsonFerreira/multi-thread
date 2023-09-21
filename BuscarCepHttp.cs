using multi_thread.Models;
using Newtonsoft.Json;

namespace multi_thread
{
    public class BuscarCepHttp
    {
        private readonly string Url = $"https://viacep.com.br/ws";
        public async Task<Endereco> FindAddress(string cep)
        {
            using HttpClient client = new();
            await Task.Delay(2000);
            try
            {
                Console.WriteLine($".... buscando cep {cep}");
                HttpResponseMessage response = await client.GetAsync($"{Url}/{cep}/json");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Endereco>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            }
            return null;
        }
    }
}