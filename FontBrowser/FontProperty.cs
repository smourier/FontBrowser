namespace FontBrowser
{
    public class FontProperty
    {
        public FontProperty(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
        public override string ToString() => Name;
    }
}
