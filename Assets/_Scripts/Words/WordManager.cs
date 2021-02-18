using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordManager : MonoBehaviour {

    // Singleton
    public static WordManager instance;

    #region Public Variables
    [Header("References")]
    // Prefab para as letras
    public GameObject letterPrefab;
    // Transform do gameobject que será o pai das letras a serem instanciadas
    public Transform wordHolder;
    // Palavra atual que o jogador esta tentando adivinhar
    public string Word { get; private set; }
    #endregion

    #region Private Variables
    // Lista das Letters que representam a palavra atual
    private List<Letter> letters;
    #endregion

    #region Monobehaviour Callbacks
    private void Awake() {
        // Singleton
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this;
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Retorna a Letter que representa o caractere <paramref name="character"/>
    /// </summary>
    /// <param name="character">O caractere a ser procurado</param>
    /// <returns>A instancia de Letter que representa o caractere</returns>
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

    /// <summary>
    /// Confere se a condição de vitória foi atingida
    /// </summary>
    private void CheckVictory () {
        bool victory = true;
        foreach (Letter l in letters) {
            victory = victory && l.Revealed;
        }

        if (victory) {
            GameManager.instance.Victory();
        }
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Inicializa o WordManager, escolhendo uma nova palavra e instanciando as letras na interface
    /// </summary>
    public void Initialize () {
        Word = WordLoader.GetRandomWord();
        Debug.Log("A palavra selecionada foi " + Word);

        if (letters == null) {
            letters = new List<Letter>();
        } else {
            letters.Clear();
        }

        if (wordHolder.childCount > 0) {
            foreach (Transform child in wordHolder) {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < Word.Length; i++) {
            Letter letter = GetLetter(Word[i]);

            GameObject letterGO = Instantiate(letterPrefab, wordHolder) as GameObject;
            TextMeshProUGUI letterText = letterGO.GetComponent<TextMeshProUGUI>();
            letter.AddPosition(letterText);
        }
    }

    /// <summary>
    /// Confere se o caractere <paramref name="letter"/> esta presente na palavra, se estiver mostra ele e reconhece o acerto
    /// </summary>
    /// <param name="letter">O caractere a ser procurado</param>
    public void ShowLetter (char letter) {
        foreach (Letter l in letters) {
            if (l.Char == letter) {
                if (l.Reveal()) {
                    GameManager.instance.RecognizeHit();
                    CheckVictory();
                }
                return;
            }
        }

        GameManager.instance.RecognizeError();
    }
    #endregion
}
