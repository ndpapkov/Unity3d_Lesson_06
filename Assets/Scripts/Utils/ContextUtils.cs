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

        internal static T Get<T>(object context) where T : class
        {
            var result = context as T;
            if (result != null)
            {
                return result;
            }

            var contextObjects = context as object[];
            if (contextObjects != null)
            {
                foreach (var contextObject in contextObjects)
                {
                    result = contextObject as T;
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
    }
}