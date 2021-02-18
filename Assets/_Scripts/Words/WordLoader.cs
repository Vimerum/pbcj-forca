using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class WordLoader  {

    #region Private Variables
    // Lista de palavras disponiveis
    private static List<string> words;
    #endregion

    #region Private Functions
    /// <summary>
    /// Carrega todas as palavras no arquivo words.txt
    /// </summary>
    private static void LoadFile () {
        if (words != null) {
            return;
        }

        words = new List<string>();
        //StreamReader reader = new StreamReader("Assets/Resources/words.txt");
        TextAsset asset = Resources.Load<TextAsset>("words");

        string[] lines = Regex.Split ( asset.text, "\n|\r|\r\n" );
        foreach (string line in lines) {
            if (line.Length > 0) {
                words.Add(line.ToUpper());
            }
        }
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Retorna uma palavra aleatória
    /// </summary>
    /// <returns>A palavra aleatória</returns>
    public static string GetRandomWord () {
        LoadFile();

        int index = Random.Range(0, words.Count);
        return words[index];
    }
    #endregion
}
