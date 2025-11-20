using System.Xml.Linq;

namespace HttpSandbox
{
    public class ContainsTextHttpFilter : HttpFilter
    {
        public ContainsTextHttpFilter() { }
        public ContainsTextHttpFilter(XElement fitem)
        {
            Filter = fitem.Element("filter").Value;
        }

        public string Filter { get; set; }
        public override XElement ToXml()
        {
            XElement ret = new XElement("httpFilter");
            ret.Add(new XAttribute("kind", nameof(ContainsTextHttpFilter)));
            ret.Add(new XElement("filter", new XCData(Filter)));
            return ret;
        }

        internal override bool IsApplicable(HttpRequestInfo request)
        {
            return request.Raw.Contains(Filter);
        }
    }
}
