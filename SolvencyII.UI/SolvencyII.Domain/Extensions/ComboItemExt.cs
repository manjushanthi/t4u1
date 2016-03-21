using SolvencyII.Domain.Entities;

namespace SolvencyII.Domain.Extensions
{
    /// <summary>
    /// Extension of ComboItem;
    /// Conversion of ComboItems to OpenComboItems
    /// </summary>
    public static class ComboItemExt
    {
        public static OpenComboItem ConvertToOpenComboItem(this ComboItem item)
        {
            return new OpenComboItem {IsAbstract = item.IsAbstract, Text = item.Text, Name = item.Value, Include = item.Include};
        }

    }
}
