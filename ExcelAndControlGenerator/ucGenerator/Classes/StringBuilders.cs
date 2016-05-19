using System.Text;

namespace ucGenerator.Classes
{
    /// <summary>
    /// A single object managing the four string builders needed for writing a template
    /// </summary>
    public class StringBuilders
    {
        public StringBuilder sbInstantiate = new StringBuilder();
        public StringBuilder sbProperties = new StringBuilder();
        public StringBuilder sbThisControl = new StringBuilder();
        public StringBuilder sbDeclare = new StringBuilder();

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}", sbInstantiate, sbProperties, sbThisControl, sbDeclare);
        }

    }
}
