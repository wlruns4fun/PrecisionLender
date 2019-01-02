using System;
using System.Text;

namespace UnitTesting
{
    public static class UnitTesting // we don't have to create an object instance state, so we can just use a static class if we like
    {
        public static String InterleaveStrings(String string1, String string2) // and a static method
        {
            StringBuilder result = new StringBuilder();

            if (string1 == null) // if string1 is null, we can short-circuit straight to just using string2
            {
                result.Append(string2);
            }
            else if (string2 == null) // and same if string2 is null
            {
                result.Append(string1);
            }
            else // otherwise, we've already verified both strings are not null so we can piece them together until we run out of characters  
            {
                for (int i = 0; i < Math.Max(string1.Length, string2.Length); i++)
                {
                    if (i < string1.Length)
                    {
                        result.Append(string1[i]);
                    }

                    if (i < string2.Length)
                    {
                        result.Append(string2[i]);
                    }
                }
            }

            return result.ToString();
        }
    }
}