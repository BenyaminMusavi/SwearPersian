using System.ComponentModel.DataAnnotations;

namespace SwearPersian;

public class FilterPersianWords 
{
    DataContainer dataContainer;

    public FilterPersianWords() //: base(customAbsolutePath)
    {
        dataContainer = new DataContainer();
    }

    private bool AreEqual(string phrase, string sensitivePhrase)
    {
        return phrase.Contains(sensitivePhrase);
    }

    /// <summary>
    /// Checks if the given phrase is sensitive,
    /// </summary>
    /// <param name="phrase">Phrase to check.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns <c>true</c> if phrase is sensitive otherwise returns <c>false</c> </returns> 
    /// آیا یک عبارت حساس است یا خیر
    public bool IsSensitivePhrase(string phrase)
    {
        return phrase switch
        {
            null => throw new ArgumentNullException($"{nameof(phrase)} can't be null."),

            "" or " " => false,

            _ => dataContainer.GetStrings().Any(p => AreEqual(phrase, p)
            )
        };
    }

    /// <summary>
    /// Checks if the given sentence contains any sensitive phrases.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <param name="expectCount">Expected number
    /// sensitive phrases.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns true if  number of sensitive phrases is above <c>expectCount</c></returns>
    /// //آیا یک جمله حاوی عبارات حساس است یا خیر
    public bool IsSensitiveSentence(string sentence, ushort expectCount = 0)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var inputWordList = sentence.GetPhrasesInSentence();
        var counter = inputWordList.Count(IsSensitivePhrase);
        return counter > expectCount;
    }

    /// <summary>
    /// Removes all sensitive phrases from the given sentence.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns filtered sentence.</returns>
    /// حذف تمامی عبارات حساس در یک جمله
    public string RemoveSensitivePhrases(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var splitLines = sentence.GetLinesInSentence();


        foreach (var (value, index) in splitLines.Select((value, index) => (value, index)))
        {
            var a = value.GetPhrasesInSentence();
            var b = dataContainer.GetStrings();
            var c = a.Except(b);
            splitLines[index] = string.Join(" ", c);
        }

        return splitLines.Aggregate("", (current, splitLine) => current + ("\n" + splitLine)).TrimStart();
    }

    /// <summary>
    /// Finds sensitive phrases in the given sentence.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns <c>List</c> of sensitive phrases in the given sentence.</returns>
    /// // یافتن تمامی عبارات حساس از یک جمله
    public IEnumerable<string> GetSensitivePhrases(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var splitLines = sentence.GetLinesInSentence();

        var result = new List<string>();

        foreach (var lines in splitLines)
            result.AddRange(lines.GetPhrasesInSentence().Where(IsSensitivePhrase).ToList());

        return result;
    }

    /// <summary>
    /// Finds sensitive phrases and configured phrases that caused the phrases to be sensitive.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns <c>List</c> of sensitive phrases in the given sentence.</returns>
    /// یافتن عبارات حساس و عبارات پیکربندی شده دکه باعث حساسیت عبارات شده اند در یک جمله
    public Dictionary<string, List<string>> GetSensitivePhrasesWithMatches(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var splitLines = sentence.GetLinesInSentence();

        var result = new Dictionary<string, List<string>>();

        foreach (var line in splitLines)
            foreach (var phrase in line.GetPhrasesInSentence())
            {
                if (result.ContainsKey(phrase))
                {
                    continue;
                }

                var sensitivePhrases = dataContainer.GetStrings().Where(sensitivePhrase => AreEqual(phrase, sensitivePhrase))
                    .ToList();
                if (sensitivePhrases.Any())
                {
                    result.Add(phrase, sensitivePhrases);
                }
            }

        return result;
    }


    public bool RemoveSensitivePhrasesSpace(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        return IsSensitivePhrase(sentence.GetPhrasesInSentences());
    }

    public bool Test(string name, string family)
    {
        var model = new NewObj();
        model.Name = name;
        model.Family = family;

        var context = new ValidationContext(model, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(model, context, results, true);

        if (!isValid)
        {
            foreach (var validationResult in results)
            {
                Console.WriteLine(validationResult.ErrorMessage);
            }
            return false;
        }
        else
        {
            Console.WriteLine("model is valid!");
            return true;
        }
    }

}
