using System.Text;

namespace HttpSandbox
{
    public static class BinaryReaderExtension
    {

        public static String ReadLine(this BinaryReader reader)
        {
            var result = new StringBuilder();
            bool foundEndOfLine = false;
            char ch;
            while (!foundEndOfLine)
            {
                try
                {
                    ch = reader.ReadChar();
                }
                catch (EndOfStreamException ex)
                {
                    if (result.Length == 0) return null;
                    else break;
                }

                switch (ch)
                {
                 //   case '\r':
                     //   if (reader.PeekChar() == '\n') reader.ReadChar();
                      //  foundEndOfLine = true;
                      //  break;
                    case '\n':
                        foundEndOfLine = true;
                        break;
                    default:
                        result.Append(ch);
                        break;
                }
            }
            return result.ToString().TrimEnd();
        }
    }
}
