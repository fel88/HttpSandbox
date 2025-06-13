

namespace HttpSandbox
{
    public abstract class MockHttpResponse
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; } = true;
        public List<HttpFilter> Filters = new List<HttpFilter>();

        public abstract string GetResponse();

        public bool IsApplicable(HttpRequestInfo request)
        {
            return Filters.All(z => z.IsApplicable(request));
        }

        public virtual IEnumerable<Command> GetCommands()
        {
            return [];
        }
    }
}
