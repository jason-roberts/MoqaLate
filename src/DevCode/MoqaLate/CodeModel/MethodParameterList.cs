using System.Collections.Generic;
using System.Text;

namespace MoqaLate.CodeModel
{
    public class MethodParameterList : List<MethodParameter>
    {
        public override string ToString()
        {
            if (Count == 0)
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var param in this)
            {
                sb.Append(param.Type);
                sb.Append(" ");
                sb.Append(param.Name);
                sb.Append(", ");
            }

            return sb.ToString(0, sb.Length - 2);
        }
    }
}