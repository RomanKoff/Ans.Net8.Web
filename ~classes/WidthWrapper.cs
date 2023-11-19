using Ans.Net8.Common;
using System.Text;

namespace Ans.Net8.Web
{

    public class WidthWrapper
    {

        /* ctor */


        public WidthWrapper(
            string html,
            WidthsEnum width,
            string cssClass)
        {
            Html = html;
            Width = width;
            CssClass = cssClass;
        }


        /* properties */


        public string Html { get; set; }
        public WidthsEnum Width { get; set; }
        public string CssClass { get; set; }


        /* functions */


        public override string ToString()
        {
            var sb1 = new StringBuilder();
            switch (Width)
            {
                case WidthsEnum.Full:
                    sb1.Append("w-100");
                    break;
                case WidthsEnum.ExtraLarge:
                    sb1.Append("w-40rem");
                    break;
                case WidthsEnum.Large:
                    sb1.Append("w-30rem");
                    break;
                case WidthsEnum.Medium:
                    sb1.Append("w-20rem");
                    break;
                case WidthsEnum.Small:
                    sb1.Append("w-15rem");
                    break;
                case WidthsEnum.ExtraSmall:
                    sb1.Append("w-10rem");
                    break;
                default:
                    sb1.Append("w-6rem");
                    break;
            }
            if (!string.IsNullOrEmpty(CssClass))
            {
                if (sb1.Length > 0)
                    sb1.Append(' ');
                sb1.Append(CssClass);
            }
            return (sb1.Length == 0)
                ? Html : $"<div class='{sb1}'>{Html}</div>";
        }

    }

}
