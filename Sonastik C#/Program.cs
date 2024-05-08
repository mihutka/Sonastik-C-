using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Translator
{
    // Чтение словаря для перевода с русского на эстонский
    public static Dictionary<string, string> ReadRussianToEstonianDictionary()
    {
        string[] russianWords = File.ReadAllLines("rus.txt");
        string[] estonianWords = File.ReadAllLines("est.txt");

        return russianWords.Zip(estonianWords, (r, e) => new { Russian = r.Trim().ToLower(), Estonian = e.Trim().ToLower() })
                           .ToDictionary(pair => pair.Russian, pair => pair.Estonian);
    }

    // Чтение словаря для перевода с эстонского на русский
    public static Dictionary<string, string> ReadEstonianToRussianDictionary()
    {
        string[] russianWords = File.ReadAllLines("rus.txt");
        string[] estonianWords = File.ReadAllLines("est.txt");

        return estonianWords.Zip(russianWords, (e, r) => new { Estonian = e.Trim().ToLower(), Russian = r.Trim().ToLower() })
                            .ToDictionary(pair => pair.Estonian, pair => pair.Russian);
    }

    // Добавление слова на русском в файл
    public static void AddWordToRussianDictionary(string word)
    {
        using (StreamWriter sw = File.AppendText("rus.txt"))
        {
            sw.WriteLine(word.Trim().ToLower());
        }
    }

    // Добавление слова на эстонском в файл
    public static void AddWordToEstonianDictionary(string word)
    {
        using (StreamWriter sw = File.AppendText("est.txt"))
        {
            sw.WriteLine(word.Trim().ToLower());
        }
    }
}


