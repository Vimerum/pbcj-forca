using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    /// <summary>
    /// Representa todos os possiveis estados de jogo
    /// </summary>
    public enum GameStatus { InGame, Victory, Defeat}

    // Singleton
    public static GameManager instance;

    #region Public Variables
    [ReadOnly]
    // Representa o estado atual do jogo
    public GameStatus status;
    [Header("Settings")]
    // Pontos a serem atribuidos por acerto
    public int pointsPerHit = 10;
    // Número maximo de erros 
    public int maxErrors = 6;
    [Header("Prefabs")]
    // Prefab que representa o indicador de acerto
    public GameObject hitIndicatorPrefab;
    // Prefab que representa o indicador de erro
    public GameObject errorIndicatorPrefab;
    [Header("References - Geral")]
    // Referencia ao painel de interface de jogo
    public GameObject inGamePanel;
    // Referencia ao painel de interface de vitória
    public GameObject victoryPanel;
    // Referencia ao painel de interface de derrota
    public GameObject defeatPanel;
    [Header("References - InGame")]
    // Referencia ao componente de texto dos pontos
    public TextMeshProUGUI scoreValue;
    // Referencia ao componente de texto dos erros
    public TextMeshProUGUI errorValue;
    // Referencia ao pai dos indicadores de acerto e erro
    public Transform indicatorParent;
    [Header("References - Defeat")]
    // Referencia ao componente de texto da palavra a ser revelada
    public TextMeshProUGUI revealValue;
    #endregion

    #region Private Variables
    // Pontuação atual do jogador
    private int score = 0;
    // Quantidade atual de erros
    private int currErrors = 0;
    #endregion

    #region Monobehaviour Callbacks
    private void Awake() {
        // Caso um Singleton ja tenha sido definido, destroi esse GameManager
        if (instance != null) {
            Destroy(this);
            return;
        }
        // Caso contrario, define esse GameManager com o Singleton
        instance = this;

        StartGame();
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Ativa e desativa os paineis de acordo com o estado atual do jogo
    /// </summary>
    private void TogglePanels () {
        inGamePanel.SetActive(status == GameStatus.InGame);
        victoryPanel.SetActive(status == GameStatus.Victory);
        defeatPanel.SetActive(status == GameStatus.Defeat);
    }

    /// <summary>
    /// Atualiza a pontuação na interface
    /// </summary>
    private void UpdateScore () {
        scoreValue.text = string.Format("{0:d}", score);
    }

    /// <summary>
    /// Atualiza a quantidade de erros na interface
    /// </summary>
    private void UpdateErrors () {
        errorValue.text = string.Format("{0:d}/{1:d}", currErrors, maxErrors);
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Começa uma nova partida
    /// </summary>
    public void StartGame () {
        AudioManager.instance.PlayMusic("main");

        status = GameStatus.InGame;

        score = 0;
        currErrors = 0;

        TogglePanels();
        UpdateScore();
        UpdateErrors();

        WordManager.instance.Initialize();
    }

    /// <summary>
    /// Encerra o jogo. Chamar apenas se não tiver a intenção de começar uma nova partida. Para começar uma nova partida, chamar StartGame()
    /// </summary>
    public void QuitGame () {
        SceneManager.LoadScene("Credits");
    }

    /// <summary>
    /// Reconhece um acerto por parte do jogador
    /// </summary>
    public void RecognizeHit () {
        AudioManager.instance.PlaySFX("yes");

        score += pointsPerHit;
        UpdateScore();

        GameObject hitIndicator = Instantiate(hitIndicatorPrefab, indicatorParent) as GameObject;
        hitIndicator.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("+{0:d}", pointsPerHit);
        Destroy(hitIndicator, 1f);
    }

    /// <summary>
    /// Reconhece um erro por parte do jogador
    /// </summary>
    public void RecognizeError () {
        AudioManager.instance.PlaySFX("no");

        currErrors++;
        UpdateErrors();

        GameObject errorIndicator = Instantiate(errorIndicatorPrefab, indicatorParent) as GameObject;
        Destroy(errorIndicator, 1f);

        if (currErrors >= maxErrors) {
            Defeat();
        }
    }

    /// <summary>
    /// Inicia sequencia de vitória
    /// </summary>
    public void Victory () {
        AudioManager.instance.PlaySFX("ahh");

        status = GameStatus.Victory;
        TogglePanels();
    }

    /// <summary>
    /// Inicia sequencia de derrota
    /// </summary>
    public void Defeat () {
        AudioManager.instance.PlaySFX("laugh");

        revealValue.text = WordManager.instance.Word;

        status = GameStatus.Defeat;
        TogglePanels();
    }
    #endregion
}
