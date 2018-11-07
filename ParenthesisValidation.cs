using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.CodeDom;

class Solution {

    static void Main(string[] args)
    {
        //var s = "([])(]";
        //var s = "([]){}";
        //var s = ")(][}{";
        var s = "({({[([([])])]})})";

        bool result = IsValid(s);

        Console.WriteLine(result);
        Console.ReadKey();
    }

    private static bool IsValid(string s)
    {
        var lastBrecket = new Stack<char>();

        var closing = new[] {')', '}', ']'};
        for (var i = 0; i < s.Length; i++)
        {
            var ch = s[i];
            if (closing.Contains(ch))
            {
                if (i == 0) return false;
                var peek = lastBrecket.Peek();
                if (ch == ')' && peek == '('
                    || ch == ']' && peek == '['
                    || ch == '}' && peek == '{')
                {
                    lastBrecket.Pop();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                lastBrecket.Push(ch);
            }
        }
        return lastBrecket.Count == 0;
    }
}
