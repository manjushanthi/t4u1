using System;

namespace SolvencyII.Domain.Attributes
{
    /// <summary>
    /// Global attribute used on the poco classes to identify if the primary key is auto increase.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoIncrementAttribute : Attribute
    {
    }
}
