using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Utility
{
    public static class ApplicationJsonConvertor
    {

        public static bool TryToJsonNode(this string Content, out JsonNode jsonNode)
        {
            jsonNode = null;
            try
            {
                if (string.IsNullOrEmpty(Content))
                {

                    return false;
                }

                jsonNode = JsonNode.Parse(Content);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
