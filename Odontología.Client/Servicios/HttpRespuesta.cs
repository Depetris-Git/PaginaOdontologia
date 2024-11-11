namespace Odontología.Client.Servicios
{
    public class HttpRespuesta<T>
    {
        public T Respuesta { get; set; }
        public bool Error { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
        public HttpRespuesta(T respuesta, bool error, HttpResponseMessage httpResponseMessage)
        {
            Respuesta = respuesta;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public async Task<string> ObtenerError()
        {
            if (!Error)
            {
                return "";
            }
            var statuscode = HttpResponseMessage.StatusCode;
            switch (statuscode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    return HttpResponseMessage.Content.ReadAsStringAsync().Result;
                case System.Net.HttpStatusCode.Unauthorized:
                    return "Error, no está logueado";
                case System.Net.HttpStatusCode.Forbidden:
                    return "No tiene autorización a ejecutar esta función";
                case System.Net.HttpStatusCode.NotFound:
                    return "Error, dirección no encontrada";
                default:
                    return HttpResponseMessage.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
