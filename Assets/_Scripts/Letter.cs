using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Letter {

    public char Char { get; private set; }
    public bool Revealed { get; private set; }
    public List<TextMeshProUGUI> Letters { get; private set; }

    public Letter (char _char) {
        Char = _char;
        Revealed = false;
        Letters = new List<TextMeshProUGUI>();
    }

    public void AddPosition (TextMeshProUGUI letter) {
        if (!Letters.Contains(letter)) {
            Letters.Add(letter);
        }
    }

    public void Reveal () {
        if (!Revealed) {
            Revealed = true;

            foreach (TextMeshProUGUI t in Letters) {
                t.text = Char.ToString();
            }
        }
    }

    public bool Equals(char obj) {
        return Char == obj;
    }

    public override bool Equals(object obj) {
        var letter = obj as Letter;
        return letter != null &&
               Char == letter.Char;
    }

    public override int GetHashCode() {
        var hashCode = 1303098579;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + Char.GetHashCode();
        return hashCode;
    }
}
