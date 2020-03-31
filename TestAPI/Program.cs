using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestAPI
{
    class Program
    {
        async static void GetPet200()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri("https://petstore.swagger.io/v2/pet/1");
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");

                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            using (HttpContent responseContent = response.Content)
                            {
                                var json = await responseContent.ReadAsStringAsync();
                                Console.WriteLine("GetPet200" + "\r\n" + "Status: " + (int)response.StatusCode + "\r\n" + json + "\r\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("GetPet200" + "\r\n" + "Status: " + (int)response.StatusCode + "\r\n" + "Fail" + "\r\n");
                        }

                    }
                }
            }
        }
        async static void GetPet400()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri("https://petstore.swagger.io/v2/pet/0");
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");

                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            using (HttpContent responseContent = response.Content)
                            {
                                var json = await responseContent.ReadAsStringAsync();
                                Console.WriteLine("GetPet400" + "\r\n" + "Status: " + (int)response.StatusCode + "\r\n" + json + "\r\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("GetPet400" + "\r\n" + "Status: " + (int)response.StatusCode + "\r\n" + "Fail" + "\r\n");
                        }
                    }
                }
            }
        }
        async static void PostPet()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {

                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("petId ", "1"),
                        new KeyValuePair<string, string>("name", "test1988"),
                        new KeyValuePair<string, string>("status", "99"),
                    });
                    var result = await client.PostAsync("https://petstore.swagger.io/v2/pet/1", content);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(resultContent);
                }
            }
        }
        async static void GetPet404()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri("https://petstore.swagger.io/v2/pets/pets");
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");

                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            using (HttpContent responseContent = response.Content)
                            {
                                var json = await responseContent.ReadAsStringAsync();
                                Console.WriteLine("GetPet404" + "\r\n" + "Status: " + (int)response.StatusCode + "\r\n" + json + "\r\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("GetPet404" + "\r\n" + "Status: " + (int)response.StatusCode + "\r\n" + "Fail" + "\r\n");
                        }
                    }
                }
            }
        }
        static async Task Main()
        {
            GetPet200();
            GetPet400();
            GetPet404();
            PostPet();
            Console.ReadKey();
        }
    }
}
