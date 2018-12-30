namespace Utils
{
    public static class ContextUtils
    {
        internal static object Wrap(params object[] args)
        {
            if (args.Length == 1)
            {
                return args[0];
            }
            return args;
        }

        internal static T GetFrom<T>(object context) where T : class
        {
            var t = context as T;
            if (t != null)
            {
                return t;
            }

            var a = context as object[];
            if (a != null)
            {
                foreach (var c in a)
                {
                    t = c as T;
                    if (t != null)
                    {
                        return t;
                    }
                }
            }
            return null;
        }
    }
}