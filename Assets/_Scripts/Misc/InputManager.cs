using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

    #region Private Variables
    // Representa as ultimas letras apertadas
    private List<char> lastChars;
    #endregion

    #region Monobehaviour Callbacks
    private void Start() {
        lastChars = new List<char>();
    }

    private void Update() {
        // Se o jogo esta no estado certo e se alguma tecla foi pressionada, continua com a captura de novos caracteres
        if (GameManager.instance.status == GameManager.GameStatus.InGame && Input.anyKeyDown) {
            string input = Input.inputString.ToUpper();

            // Itera sobre todos os caracteres pressionados nesse frame e verifica se é um caractere correto ou não
            foreach (char c in input.ToCharArray()) {
                if (!lastChars.Contains(c)) {
                    WordManager.instance.ShowLetter(c);
                }
            }

            // Atualiza ultimos caracteres pressionados
            lastChars.Clear();
            lastChars.AddRange(input);
        }
    }
    #endregion
}
