using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Public Variables
    [Header("Settings")]
    // Tempo que a tela de Loading ficará visivel
    public float timeToWait = 5f;
    [Header("References")]
    // Referencia ao painel de interface do menu inicial
    public GameObject mainMenuPanel;
    // Referencia ao painel de interface de loading
    public GameObject loadingPanel;
    #endregion

    #region Private Variables
    // Variavel para armazenar a corrotina de inicio de jogo
    private Coroutine startGameCoroutine;
    #endregion

    #region Monobehaviour Callbacks
    private void Start() {
        // Define estado inicial dos paineis de interface
        mainMenuPanel.SetActive(true);
        loadingPanel.SetActive(false);
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Carrega a tela com o jogo em si
    /// </summary>
    public void StartGame () {
        if (startGameCoroutine == null) {
            startGameCoroutine = StartCoroutine(StartGameCO());
        }
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Corrotina de inicio de jogo, deixa a tela de loading visivel por timeToWait segundos, e depois carrega a cena com o jogo em si
    /// </summary>
    /// <returns>Corrotina de inicio do jogo</returns>
    private IEnumerator StartGameCO () {
        mainMenuPanel.SetActive(false);
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene("MainScene");
    }
    #endregion
}
