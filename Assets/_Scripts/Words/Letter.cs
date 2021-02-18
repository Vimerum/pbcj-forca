using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Letter {

    #region Public Variables
    // Caractere representado por essa letra
    public char Char { get; private set; }
    // Variavel de controle para saber se essa letra ja foi revelada
    public bool Revealed { get; private set; }
    // Lista de componentes de texto que possuem o caractere representado por essa letra
    public List<TextMeshProUGUI> Letters { get; private set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Letra que representa o caractere <paramref name="_char"/>
    /// </summary>
    /// <param name="_char">O caractere representado por essa letra</param>
    public Letter (char _char) {
        Char = _char;
        Revealed = false;
        Letters = new List<TextMeshProUGUI>();
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Adiciona um novo componente de texto que contem o caractere representado por essa letra
    /// </summary>
    /// <param name="letter">O novo componente de texto</param>
    public void AddPosition (TextMeshProUGUI letter) {
        if (!Letters.Contains(letter)) {
            Letters.Add(letter);
        }
    }

    /// <summary>
    /// Revela essa letra, ou seja, o jogador acertou essa letra
    /// </summary>
    /// <returns>Retorna true se a letra foi descoberta, caso a letra ja tenha sido descoberta, retorna false</returns>
    public bool Reveal () {
        if (!Revealed) {
            Revealed = true;

            foreach (TextMeshProUGUI t in Letters) {
                t.text = Char.ToString();
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Retorna se o objeto <paramref name="obj"/> é igual a essa classe
    /// </summary>
    /// <param name="obj">O objeto a ser comparado</param>
    /// <returns>Retorna true se os objetos são iguais, caso contrario retorna false</returns>
    public override bool Equals(object obj) {
        var letter = obj as Letter;
        return letter != null &&
               Char == letter.Char;
    }

    /// <summary>
    /// Retorna o HashCode que representa uma instancia dessa classe
    /// </summary>
    /// <returns>O HashCode da instancia dessa classe</returns>
    public override int GetHashCode() {
        var hashCode = 1303098579;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + Char.GetHashCode();
        return hashCode;
    }
    #endregion
}
