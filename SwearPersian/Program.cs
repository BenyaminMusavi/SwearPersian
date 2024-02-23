using SwearPersian;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

DataContainer dataContainer = new DataContainer();

FilterPersianWords filterPersianWords = new FilterPersianWords();

FilterPersianWords filter = new FilterPersianWords();
filter.Test("بنیامین","موسوی");


//Console.WriteLine(filterPersianWords.IsSensitivePhrase("سلام"));
//Console.WriteLine(filterPersianWords.IsSensitivePhrase("عن"));
//Console.WriteLine(filterPersianWords.IsSensitivePhrase("خر"));
//Console.WriteLine("-----------------------------------------");

//Console.WriteLine(filterPersianWords.IsSensitivePhrase("عن خر"));
//Console.WriteLine(filterPersianWords.IsSensitiveSentence("خر عن"));
//Console.WriteLine("-----------------------------------------");

//Console.WriteLine(filterPersianWords.IsSensitivePhrase("خ|ر"));
//Console.WriteLine(filterPersianWords.IsSensitiveSentence("خ|ر"));
//Console.WriteLine("-----------------------------------------");

Console.WriteLine(filterPersianWords.IsSensitivePhrase("عبارت حساس"));
Console.WriteLine(filterPersianWords.IsSensitivePhrase("این جمله حاوی عبارت حساس میباشد"));
Console.WriteLine("-----------------------------------------");
Console.WriteLine(filterPersianWords.IsSensitivePhrase("عبارت حساس"));
Console.WriteLine(filterPersianWords.IsSensitiveSentence("این جمله حاوی عبارت حساس میباشد"));
Console.WriteLine("-----------------------------------------");

Console.WriteLine(filterPersianWords.IsSensitivePhrase("سیامع نارمی"));
Console.WriteLine(filterPersianWords.IsSensitiveSentence("سیامع نارمی"));
Console.WriteLine("-----------------------------------------");

Console.WriteLine(filterPersianWords.IsSensitivePhrase("ع ن"));
Console.WriteLine(filterPersianWords.IsSensitiveSentence("ع ن"));
Console.WriteLine("-----------------------------------------");

Console.WriteLine(filterPersianWords.IsSensitiveSentence("عنخر"));
Console.WriteLine(filterPersianWords.IsSensitiveSentence("خرعن"));
Console.WriteLine("-----------------------------------------");


Console.WriteLine(filterPersianWords.IsSensitivePhrase("خ ر ع ن"));
Console.WriteLine(filterPersianWords.IsSensitivePhrase("ع ن خ ر"));
Console.WriteLine("-----------------------------------------");


Console.WriteLine(filterPersianWords.RemoveSensitivePhrasesSpace("خ ر ع ن"));
Console.WriteLine(filterPersianWords.RemoveSensitivePhrasesSpace("ع ن خ ر"));
Console.WriteLine("-----------------------------------------");


var a = filterPersianWords.RemoveSensitivePhrases("عن | خر");
var b = filterPersianWords.GetSensitivePhrasesWithMatches("خر | ر");
Console.WriteLine("-----------------------------------------");



Console.WriteLine(a);

Console.ReadKey();