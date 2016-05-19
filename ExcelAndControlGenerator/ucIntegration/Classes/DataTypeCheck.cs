using SolvencyII.Domain;

namespace ucIntegration.Classes
{
    internal static class DataTypeCheck
    {
        public static bool Check(string type, MAPPING map)
        {
            switch (type.ToUpper())
            {
                    // B,P, D, M, S, I, E, M
                case "BOOLEAN":
                    return map.DATA_TYPE == "B";
                case "TRUE":
                    return map.DATA_TYPE == "B";
                case "DATE":
                    return map.DATA_TYPE == "D";
                case "ENUMERATION/CODE":
                    return map.DATA_TYPE == "E";
                case "URI":
                case "STRING":
                case "VARCHAR":
                    return ((map.DATA_TYPE == "S") || (map.DATA_TYPE == "E"));
                case "INTEGER":
                    return map.DATA_TYPE == "I";
                case "PERCENTAGE":
                    return map.DATA_TYPE == "P";
                case "DECIMAL":
                    return map.DATA_TYPE == "B";
                case "MONETARY":
                case "NUMERIC":
                default:
                    return ((map.DATA_TYPE == "M") || (map.DATA_TYPE == "I") || (map.DATA_TYPE == "P"));

            }
        }
    }
}
