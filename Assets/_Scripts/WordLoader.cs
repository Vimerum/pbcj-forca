using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class WordLoader  {

    private static List<string> words;

    public static string GetRandomWord () {
        ReadWords();

        int index = Random.Range(0, words.Count);
        return words[index];
    }

    private static List<string> ReadWords () {
        if (words == null) {
            LoadFile();
        }

        return words;
    }

    private static void LoadFile () {
        words = new List<string>();
        StreamReader reader = new StreamReader("Assets/Resources/words.txt");

        string line;
        while ((line = reader.ReadLine()) != null) {
            words.Add(line.ToUpper());
        }
    }

}
