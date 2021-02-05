using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordManager : MonoBehaviour {

    public static WordManager instance;

    [Header("References")]
    public GameObject wordPrefab;
    public Transform wordHolder;

    private string word;
    private List<Letter> letters;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this;

        word = WordLoader.GetRandomWord();
        Debug.Log("A palavra selecionada foi " + word);

        letters = new List<Letter>();
        for (int i = 0; i < word.Length; i++) {
            Letter letter = GetLetter(word[i]);

            GameObject letterGO = Instantiate(wordPrefab, wordHolder) as GameObject;
            TextMeshProUGUI letterText = letterGO.GetComponent<TextMeshProUGUI>();
            letter.AddPosition(letterText);
        }
    }

    public void ShowLetter (char letter) {
        foreach (Letter l in letters) {
            if (l.Char == letter) {
                l.Reveal();
            }
        }
    }

    private Letter GetLetter (char character) {
        foreach (Letter l in letters) {
            if (l.Char == character) {
                return l;
            }
        }

        Letter newLetter = new Letter(character);
        letters.Add(newLetter);
        return newLetter;
    }
}
