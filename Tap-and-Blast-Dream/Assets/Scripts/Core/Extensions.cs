
using System.Collections.Generic;

public static class Extensions
{

    public static bool Equals(this string str1, string str2)
    {

        if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2))
        {
            return true;
        }
        if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
        {
            return false;
        }
        if (str1.Trim().Equals(str2.Trim()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ContainsKeySafe<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
    {
        if (dict != null && dict.Keys.Count > 0)
        {
            return dict.ContainsKey(key);
        }
        else
        {
            return false;
        }
    }

}