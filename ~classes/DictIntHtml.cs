using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

    public class DictIntHtml
        : Dictionary<int, HtmlString>
    {

        /* ctors */


        public DictIntHtml(
            string serialize,
            bool useTypograf = false)
        {
            foreach (var item1 in _masking(serialize).Split(';'))
            {
                var a1 = item1.Split('=');
                var key1 = a1[0].ToInt(0);
                var value1 = a1.Length == 2
                    ? _decode(_unmasking(a1[1]))
                    : key1.ToString();
                Add(key1, value1.ToHtml(useTypograf));
            }
        }


        public DictIntHtml(
            char valueSeparator,
            bool useTypograf,
            params string[] items)
        {
            foreach (var item1 in items)
            {
                var a1 = item1.Split(valueSeparator);
                Add(a1[0].ToInt(0), a1[1].ToHtml(useTypograf));
            }
        }


        /* functions */


        public override string ToString()
        {
            return string.Join(";",
                this.Select(x => $"{x.Key}={_encode(x.Value.ToString())}")
                    .ToArray());
        }


        /* privates */


        private const string _codeSemicolon = "\\;";
        private const string _maskSemicolon = "###1###";
        private const string _codeEqually = "\\=";
        private const string _maskEqually = "###2###";

        private static string _encode(string source)
            => source
                .Replace(";", _codeSemicolon)
                .Replace("=", _codeEqually);

        private static string _decode(string source)
            => source
                .Replace(_codeSemicolon, ";")
                .Replace(_codeEqually, "=");

        private static string _masking(string source)
            => source
                .Replace(_codeSemicolon, _maskSemicolon)
                .Replace(_codeEqually, _maskEqually);

        private static string _unmasking(string source)
            => source
                .Replace(_maskSemicolon, _codeSemicolon)
                .Replace(_maskEqually, _codeEqually);

    }

}
