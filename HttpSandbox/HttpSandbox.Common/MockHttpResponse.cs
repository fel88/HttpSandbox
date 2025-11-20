using System.Xml.Linq;

namespace HttpSandbox
{
    public abstract class MockHttpResponse
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; } = true;
        public int Priority { get; set; }

        public List<HttpFilter> Filters = new List<HttpFilter>();

        public MockHttpResponse() { }
        public MockHttpResponse(XElement elem)
        {
            foreach (var fitem in elem.Element("filters").Elements())
            {
                var fkind = fitem.Attribute("kind").Value;
                if (fkind == nameof(ContainsTextHttpFilter))
                {
                    Filters.Add(new ContainsTextHttpFilter(fitem));
                }
            }
        }
        public abstract string GetResponse();
        public abstract void WriteResponse(StreamWriter writer);

        public bool IsApplicable(HttpRequestInfo request)
        {
            return Filters.All(z => z.IsApplicable(request));
        }
               
        public abstract XElement ToXml();
        protected XElement FiltersToXml()
        {
            return new XElement("filters", Filters.Select(z => z.ToXml()));
        }

    }
}
