namespace VisualNode.Util
{
    public class TabContent
    {
        [System.Obsolete]
        private readonly string _header;
        private readonly object _content;

        [System.Obsolete]
        public TabContent(string header, object content)
        {
            _header = header;
            _content = content;
        }

        public TabContent(object content)
        {
            _content = content;
        }

        [System.Obsolete]
        public string Header
        {
            get { return _header; }
        }

        public object Content
        {
            get { return _content; }
        }
    }
}
