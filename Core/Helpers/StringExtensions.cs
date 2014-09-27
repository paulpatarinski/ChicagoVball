using System;

namespace Core
{
	public static class StringExtensions
	{
		public static string TruncateString (this String originalString, int length)
		{
			if (string.IsNullOrEmpty (originalString)) {
				return originalString;
			}
			if (originalString.Length > length) {
				return originalString.Substring (0, length) + "...";
			} else {
				return originalString;
			}
		}
	}
}

