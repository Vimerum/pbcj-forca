using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private void Update() {
        string input = Input.inputString.ToUpper();

        foreach (char c in input.ToCharArray()) {
            WordManager.instance.ShowLetter(c);
        }
    }
}
