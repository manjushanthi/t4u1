using System;

namespace ucGenerator.Classes
{
    /// <summary>
    /// Specifies the type of template
    /// </summary>
    [Flags]
    public enum enumClosedTemplateType
    {
        None = 0,
        HorizontalSeparator = 1,
        VerticalSeparator = 2,
        FixedDimension = 4
    }
}
