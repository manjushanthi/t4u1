namespace ucGenerator.Extensions
{
    public static class intExt
    {
        public static int KeepMax(this int total, int compare)
        {
            return (compare > total) ? compare : total;
        }
    }
}
