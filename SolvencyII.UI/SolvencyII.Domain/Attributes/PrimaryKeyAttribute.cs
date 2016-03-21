using System;

namespace SolvencyII.Domain.Attributes
{
    /// <summary>
    /// Global attribute used on the poco classes to identify the primary key.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
    }
}
