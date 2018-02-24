﻿using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bingo.RestEase.Support
{
    public static class Support
    {
        public static bool DeepEquals(this object actual, object expected)
        {
            CompareLogic compareLogic = new CompareLogic
            {
                Config =
                {
                    AttributesToIgnore = new List<Type>
                    {
                        typeof(JsonIgnoreAttribute)
                    }
                }
            };
            return compareLogic.Compare(actual, expected).AreEqual;
        }

        public static bool Is24BitHex(this string actual)
        {
            return Regex.IsMatch(actual, @"\A\b[0-9a-fA-F]+\b\Z") && actual.Length == 24;
        }

        public static bool IsNot24BitHex(this string actual)
        {
            return !Is24BitHex(actual);
        }
    }
}
