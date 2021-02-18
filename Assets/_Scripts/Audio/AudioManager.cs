using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    // Singleton
    public static AudioManager instance;

    #region Public Variables
    [Header("References")]
    // GameObject que será utilizado para adicionar os componentes de AudioSource
    public GameObject audioHolder;
    [Header("Audios")]
    // Lista de música disponiveis para serem tocadas
    public List<Audio> musicList;
    // Lista de efeitos sonoros disponiveis para serem tocados
    public List<Audio> sfxList;
    #endregion

    #region Private Variables
    // Referencia aos outros AudioManagers, caso existam
    //      Caso uma outra cena que tenha um AudioManager seja carregada
    //      o AudioManager da cena nova é adicionado nessa lista.
    private List<AudioManager> fallbacks;
    #endregion

    #region Monobehaviour Callbacks
    private void Awake() {
        if (instance == null) {
            // Define o Singleton caso ele não exista
            instance = this;
        } else {
            // Caso ja tenha um Singleton, adiciona o AudioManager atual na lista fallback do Singleton
            instance.AddFallback(this);
        }

        // Define o GameObject desse componente como um GameObject raiz (que não possui um pai), para então poder usar o DontDestroyOnLoad() nele
        transform.SetParent(null);
        // Define o GameObject desse componente para não ser destruido quando uma nova cena for carregada
        DontDestroyOnLoad(gameObject);

        // Para todas as músicas que estavam tocando até esse momento
        instance.StopAllMusic();
        // Para todos os efeitos sonoros que estavam tocando até esse momento
        instance.StopAllSFX();

        // Inicializa esse AudioManager
        Initialize();
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Inicializa o AudioManager atual
    /// </summary>
    private void Initialize () {
        // Para cada música na lista musicList, adiciona um componente AudioSource ao audioHolder e configura ele
        foreach(Audio music in musicList) {
            music.source = audioHolder.AddComponent<AudioSource>();
            music.source.clip = music.clip;
            music.source.volume = music.volume;
            music.source.playOnAwake = music.playOnAwake;
            music.source.loop = music.loop;

            if (music.playOnAwake) {
                music.source.Play();
            }
        }

        // Para cada efeito sonoro na lista sfxList, adiciona um componente AudioSource ao audioHolder e configura ele
        foreach(Audio sfx in sfxList) {
            sfx.source = audioHolder.AddComponent<AudioSource>();
            sfx.source.clip = sfx.clip;
            sfx.source.volume = sfx.volume;
            sfx.source.playOnAwake = sfx.playOnAwake;
            sfx.source.loop = sfx.loop;

            if (sfx.playOnAwake) {
                sfx.source.Play();
            }
        }
    }

    /// <summary>
    /// Procura uma música com o nome <paramref name="name"/> na lista musicList, bem como na lista musicList de todos os outros AudioManagers na lista fallbacks
    /// </summary>
    /// <param name="name">O nome da música a ser procurada</param>
    /// <returns>Caso encontre, retorna o Audio referente a música com nome <paramref name="name"/>, caso contrário retorna null.</returns>
    private Audio GetMusic (string name) {
        foreach (Audio audio in musicList) {
            if (audio.name.Equals(name)) {
                return audio;
            }
        }

        if (fallbacks != null) {
            foreach (AudioManager fallback in fallbacks) {
                Audio result = fallback.GetMusic(name);

                if (result != null) {
                    return result;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Procura um efeito sonoro com o nome <paramref name="name"/> na lista sfxList, bem como na lista sfxList de todos os outros AudioManagers na lista fallbacks
    /// </summary>
    /// <param name="name">O nome do efeito sonoro a ser procurado</param>
    /// <returns>Caso encontre, retorna o Audio referente ao efeito sonoro com o nome <paramref name="name"/>, caso contrário retorna null.</returns>
    private Audio GetSFX (string name) {
        foreach (Audio audio in sfxList) {
            if (audio.name.Equals(name)) {
                return audio;
            }
        }

        if (fallbacks != null) {
            foreach (AudioManager fallback in fallbacks) {
                Audio result = fallback.GetSFX(name);

                if (result != null) {
                    return result;
                }
            }
        }

        return null;
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Toca uma música de nome <paramref name="name"/>
    /// </summary>
    /// <param name="name">O nome da música a ser tocada</param>
    public void PlayMusic (string name) {
        Audio result = GetMusic(name);

        if (result != null) {
            result.source.Play();
        } else {
            Debug.LogWarning(string.Format("[AudioManager/PlayMusic] : Music '{0:s}' not found.", name));
        }
    }

    /// <summary>
    /// Toca um efeito sonoro de nome <paramref name="name"/>
    /// </summary>
    /// <param name="name">O nome do efeito sonoro a ser tocado</param>
    public void PlaySFX (string name) {
        Audio result = GetSFX(name);

        if (result != null) {
            result.source.Play();
        } else {
            Debug.LogWarning(string.Format("[AudioManager/PlaySFX] : SFX '{0:s}' not found.", name));
        }
    }

    /// <summary>
    /// Para de tocar todas as músicas que estão tocando no momento, bem como todas as músicas de todos os AudioManagers na lista fallbacks
    /// </summary>
    public void StopAllMusic () {
        foreach (Audio music in musicList) {
            if (music.source != null && music.source.isPlaying) {
                music.source.Stop();
            }
        }

        if (fallbacks != null) {
            foreach (AudioManager fallback in fallbacks) {
                fallback.StopAllMusic();
            }
        }
    }

    /// <summary>
    /// Para de tocar todos os efeitos sonoros que estão tocando no momento, bem como todos os efeitos sonoros de todos os AudioManagers na lista fallbacks
    /// </summary>
    public void StopAllSFX () {
        foreach (Audio sfx in sfxList) {
            if (sfx.source != null && sfx.source.isPlaying) {
                sfx.source.Stop();
            }
        }

        if (fallbacks != null) {
            foreach (AudioManager fallback in fallbacks) {
                fallback.StopAllSFX();
            }
        }
    }

    /// <summary>
    /// Adiciona um AudioManager na lista fallbacks
    /// </summary>
    /// <param name="fallback">O AudioManager a ser adicionado na lista</param>
    public void AddFallback (AudioManager fallback) {
        if (fallbacks == null) {
            fallbacks = new List<AudioManager>();
        }

        fallbacks.Add(fallback);
    }
    #endregion
}
