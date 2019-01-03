using System.Collections.Generic;

namespace BusinessLayer.Parsers
{
    public interface IParser<T> where T : class
    {
        IEnumerable<T> ParseFile(string fileName);
    }
}