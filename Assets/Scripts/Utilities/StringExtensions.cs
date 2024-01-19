using UnityEngine;

namespace Utilities
{
    public static class StringExtensions
    {
        public static void CopyToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }
    }
}
