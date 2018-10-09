namespace Acuasol.Modelos
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Noticias
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "fecha")]
        public List<string> Fecha { get; set; }
    }
}
