using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHud : MonoBehaviour {
    #region Declarations
    public GameObject gameOverObj;
    public Text endGameScoreText;
    #endregion

    #region MonoBehavior Overrides
    private void Start() {
        GameManager.Instance.GameOver += () => {
            Cursor.visible = true;
            endGameScoreText.text += GameManager.Instance.CurrentFearAmount.ToString();
            gameOverObj.SetActive(true);
        };
    }
    #endregion

    #region Button Handles
    public void GameStartClick() {
        Cursor.visible = false;
        GameManager.Instance.StartGame();
    }

    public void GameEndClick() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
    #endregion
}
