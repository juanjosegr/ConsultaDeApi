using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


class LeccutraPaginaWeb
{
    private static HttpClient _httpClient = new HttpClient();
    static string url = "https://rickandmortyapi.com/api/character";

    private static async Task Main(string[] args)
    {
        try
        {
            HttpResponseMessage respuesta = await _httpClient.GetAsync(url); // Realizar la solicitud a la API
            respuesta.EnsureSuccessStatusCode(); // Asegurarse de que la solicitud fue exitosa

            // Leer la respuesta como una cadena JSON
            string respuestaBody = await respuesta.Content.ReadAsStringAsync();

            // Deserializar la respuesta JSON en un diccionario
            Dictionary<string, object> datos = JsonConvert.DeserializeObject<Dictionary<string, object>>(respuestaBody);
            
            // Trabajar con el diccionario
            foreach (var dato in datos)
            {
                //Console.WriteLine($"Clave: {dato.Key}, Valor: {dato.Value}");
                // Aquí se recorre el diccionario y se imprime cada par clave-valor en la consola.
            }

            // Deserializar la respuesta JSON en un objeto dinámico
            dynamic data = JsonConvert.DeserializeObject(respuestaBody);
            
            // Obtener la lista de resultados
            JArray resultados = (JArray)data.results;

            // Buscar el personaje con id igual a 1
            foreach (JObject personaje in resultados)
            {
                if (personaje.ContainsKey("id") && Convert.ToInt32(personaje["id"]) == 1)
                {
                    foreach (var propiedad in personaje)
                    {
                        Console.WriteLine($"Clave: {propiedad.Key}, Valor: {propiedad.Value}");
                    }
                }
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error al realizar la solicitud: {e.Message}");
        }
    }
}