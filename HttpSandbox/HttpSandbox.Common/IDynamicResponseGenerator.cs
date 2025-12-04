namespace HttpSandbox
{
    public interface IDynamicResponseGenerator
    {
        Task WriteToStream(HttpRequestInfo request, Stream stream);
        string GetMime(HttpRequestInfo request);
        long GetLength(HttpRequestInfo request);
    }
}
