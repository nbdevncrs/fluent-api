using System.Text;
using ObjectPrinting.SettingsAppliers;

namespace ObjectPrinting
{
    internal static class PrintingFormatter
    {
        public static string Format(PrintingNode root)
        {
            if (root == null) return string.Empty;
            var sb = new StringBuilder();
            
            if (root.IsLeaf)
            {
                sb.Append(root.Value);
                return sb.ToString();
            }
            
            sb.AppendLine(root.Type?.Name ?? root.Name);

            foreach (var child in root.Children)
                AppendChild(sb, child, 1);

            return sb.ToString().TrimEnd('\r', '\n');
        }

        private static void AppendChild(StringBuilder sb, PrintingNode node, int indent)
        {
            var indentStr = new string('\t', indent);

            if (node.IsLeaf)
            {
                sb.Append(indentStr)
                  .Append(node.Name)
                  .Append(" = ")
                  .AppendLine(node.Value?.ToString() ?? "null");
                return;
            }
            
            sb.Append(indentStr)
              .Append(node.Name)
              .Append(" = ")
              .AppendLine();
            
            var header = node.Type?.Name ?? node.Name;
            sb.Append(new string('\t', indent + 1)).AppendLine(header);

            foreach (var grand in node.Children)
            {
                AppendSubChild(sb, grand, indent + 2);
            }
        }

        private static void AppendSubChild(StringBuilder sb, PrintingNode node, int indent)
        {
            var indentStr = new string('\t', indent);

            if (node.IsLeaf)
            {
                sb.Append(indentStr)
                  .Append(node.Name)
                  .Append(" = ")
                  .AppendLine(node.Value?.ToString() ?? "null");
                return;
            }
            
            sb.Append(indentStr)
              .Append(node.Type?.Name ?? node.Name)
              .AppendLine();

            foreach (var child in node.Children)
                AppendSubChild(sb, child, indent + 1);
        }
    }
}
