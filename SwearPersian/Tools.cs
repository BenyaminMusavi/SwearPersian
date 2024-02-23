using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SwearPersian;

public static class Tools
{
    private const char Separator = ' ';
    private const string Pattern = "\r\n|\r|\n";

    public static IEnumerable<string> GetPhrasesInSentence(this string sentence)
    {
        return sentence.Split(Separator);
    }

    public static string[] GetLinesInSentence(this string sentence)
    {
        return Regex.Split(sentence, Pattern);
    }

    public static string GetPhrasesInSentences(this string sentence)
    {
        var updated = Regex.Replace(sentence, @"[^a-zA-Z\u0600-\u06FF]+", "");

        return updated;
    }

}
