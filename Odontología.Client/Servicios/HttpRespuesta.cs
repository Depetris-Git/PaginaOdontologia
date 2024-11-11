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
    }
}
