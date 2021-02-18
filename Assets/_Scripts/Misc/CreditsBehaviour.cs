using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBehaviour : MonoBehaviour
{
    /// <summary>
    /// Função que fecha o jogo, independente se ele esta sendo executado no editor do Unity ou em uma build Standalone
    /// </summary>
    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
