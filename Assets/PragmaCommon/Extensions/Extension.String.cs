using System.Text;

namespace Pragma.Common
{
    public static partial class Extension
    {
        public static string ToSnakeCase(this string str)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < str.Length - 1; ++i)
            {
                var ch = str[i];
                var nCh = str[i + 1];

                if (char.IsUpper(ch) && char.IsLower(nCh))
                {
                    sb.Append("_");
                }

                sb.Append(ch.ToString().ToLower());
            }

            sb.Append(str[str.Length - 1].ToString().ToLower());

            return sb.ToString().TrimStart('_');
        }
    }
}