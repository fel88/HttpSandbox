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
            if (elem.Attribute("name") != null)
                Name = elem.Attribute("name").Value;
            if (elem.Attribute("enabled") != null)
                IsEnabled = bool.Parse(elem.Attribute("enabled").Value);
            if (elem.Attribute("priority") != null)
                Priority = int.Parse(elem.Attribute("priority").Value);

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
        public virtual void WriteResponse(StreamWriter writer)
        {
            writer.WriteLine(GetResponse());
            writer.Flush();
        }

        public bool IsApplicable(HttpRequestInfo request)
        {
            return Filters.All(z => z.IsApplicable(request));
        }

        public abstract XElement ToXml();

        protected void UpdateXmlNode(XElement elem)
        {
            elem.Add(new XAttribute("priority", Priority));
            elem.Add(new XAttribute("enabled", IsEnabled));
            elem.Add(new XAttribute("name", Name ?? string.Empty));
            elem.Add(FiltersToXml());
        }

        private XElement FiltersToXml()
        {
            return new XElement("filters", Filters.Select(z => z.ToXml()));
        }

        public virtual MockHttpResponse Clone()
        {
            var xml = ToXml();
            var ret = (Activator.CreateInstance(GetType(), new object[] { xml }) as MockHttpResponse);
            ret.Name += "_cloned";
            return ret;
        }
    }
}
