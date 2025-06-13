
namespace HttpSandbox
{
    public abstract class HttpFilter
    {
        internal abstract bool IsApplicable(HttpRequestInfo request);
        
    }
}
