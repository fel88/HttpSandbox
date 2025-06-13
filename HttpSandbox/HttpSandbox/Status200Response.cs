namespace HttpSandbox
{
    public class Status200Response : MockHttpResponse
    {
        public override string GetResponse()
        {
            return "HTTP/1.1 200 OK\r\nContent-Type: application/json\r\nContent-Length: 19\r\n\r\n{“success\":\"true\"}";
            //HTTP/1.1 200 OK\r\nContent-Type: application/json\r\n\r\n           
        }
    }
}
