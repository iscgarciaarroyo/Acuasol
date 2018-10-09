namespace Acuasol.Servicios
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Modelos;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    //using Domain;

    public class ApiServicio
    {
        public async Task<Respuesta> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = "Favor de encender su conexión a internet",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                "google.com");
            if (!isReachable)
            {
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = "Favor de checar su conexión su internet",
                };
            }

            return new Respuesta
            {
                Exito = true,
                Mensaje = "Ok",
            };
        }

        public async Task<TokenRespuesta> GetToken(
            string urlBase,
            string username,
            string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var Respuesta = await client.PostAsync("Token",
                    new StringContent(string.Format(
                    "grant_type=password&username={0}&password={1}",
                    username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var ResultadoJSON = await Respuesta.Content.ReadAsStringAsync();
                var Resultado = JsonConvert.DeserializeObject<TokenRespuesta>(
                    ResultadoJSON);
                return Resultado;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Respuesta> Get<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var Respuesta = await client.GetAsync(url);

                if (!Respuesta.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        Exito = false,
                        Mensaje = Respuesta.StatusCode.ToString(),
                    };
                }

                var Resultado = await Respuesta.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(Resultado);
                return new Respuesta
                {
                    Exito = true,
                    Mensaje = "Ok",
                    Resultado = model,
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = ex.Message,
                };
            }
        }

        public async Task<Respuesta> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var Respuesta = await client.GetAsync(url);
                var Resultado = await Respuesta.Content.ReadAsStringAsync();

                if (!Respuesta.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        Exito = false,
                        Mensaje = Resultado,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(Resultado);
                return new Respuesta
                {
                    Exito = true,
                    Mensaje = "Ok",
                    Resultado = list,
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = ex.Message,
                };
            }
        }

        public async Task<Respuesta> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var Respuesta = await client.GetAsync(url);
                var Resultado = await Respuesta.Content.ReadAsStringAsync();

                if (!Respuesta.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        Exito = false,
                        Mensaje = Resultado,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(Resultado);
                return new Respuesta
                {
                    Exito = true,
                    Mensaje = "Ok",
                    Resultado = list,
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = ex.Message,
                };
            }
        }

        public async Task<Respuesta> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var Respuesta = await client.GetAsync(url);

                if (!Respuesta.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        Exito = false,
                        Mensaje = Respuesta.StatusCode.ToString(),
                    };
                }

                var Resultado = await Respuesta.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(Resultado);
                return new Respuesta
                {
                    Exito = true,
                    Mensaje = "Ok",
                    Resultado = list,
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = ex.Message,
                };
            }
        }

        public async Task<Respuesta> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var Respuesta = await client.PostAsync(url, content);
                var Resultado = await Respuesta.Content.ReadAsStringAsync();

                if (!Respuesta.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Respuesta>(Resultado);
                    error.Exito = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(Resultado);

                return new Respuesta
                {
                    Exito = true,
                    Mensaje = "Record added OK",
                    Resultado = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = ex.Message,
                };
            }
        }

        public async Task<Respuesta> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var Respuesta = await client.PostAsync(url, content);

                if (!Respuesta.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        Exito = false,
                        Mensaje = Respuesta.StatusCode.ToString(),
                    };
                }

                var Resultado = await Respuesta.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(Resultado);

                return new Respuesta
                {
                    Exito = true,
                    Mensaje = "Record added OK",
                    Resultado = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = ex.Message,
                };
            }
        }

        public async Task<Respuesta> Put<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var Respuesta = await client.PutAsync(url, content);
                var Resultado = await Respuesta.Content.ReadAsStringAsync();

                if (!Respuesta.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Respuesta>(Resultado);
                    error.Exito = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(Resultado);

                return new Respuesta
                {
                    Exito = true,
                    Resultado = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = ex.Message,
                };
            }
        }

        public async Task<Respuesta> Delete<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var Respuesta = await client.DeleteAsync(url);
                var Resultado = await Respuesta.Content.ReadAsStringAsync();

                if (!Respuesta.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Respuesta>(Resultado);
                    error.Exito = false;
                    return error;
                }

                return new Respuesta
                {
                    Exito = true,
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = ex.Message,
                };
            }
        }
    }
}
