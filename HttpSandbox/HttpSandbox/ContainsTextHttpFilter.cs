namespace HttpSandbox
{
    public class ContainsTextHttpFilter : HttpFilter
    {
        public string Filter { get; set; }
        internal override bool IsApplicable(HttpRequestInfo request)
        {
            return request.Raw.Contains(Filter);
        }
    }
}
