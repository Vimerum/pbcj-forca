using UnityEngine;
using System.Collections;

[System.Serializable]
public class Audio {

    /*
     * Classe usada para configurar os efeitos sonoros/músicas
     */

    public string name;
    [Range(0f, 1f)]
    public float volume = 1f;
    public bool playOnAwake;
    public bool loop;
    public AudioClip clip;

    [ReadOnly]
    public AudioSource source;
}
