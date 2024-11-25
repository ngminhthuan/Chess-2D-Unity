using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _whiteTurnTxt;
    [SerializeField] private TMP_Text _blackTurnTxt;

    [SerializeField] private TMP_Text _winningTxt;
    [SerializeField] private GameObject _endGamePanel;

    public void ShowEndGameUI(ChessSkin chessSkin)
    {
        _endGamePanel.gameObject.SetActive(true);

        switch (chessSkin)
        {
            case ChessSkin.None:
            case ChessSkin.White:
                _winningTxt.text = "Black win!";
                break;
            case ChessSkin.Black:
                _winningTxt.text = "White win!";
                break;
        }
    }

    public void ChangeTurnTitle(ChessSkin turn)
    {
        switch (turn)
        {
            case ChessSkin.None:
            case ChessSkin.White:
                _whiteTurnTxt.gameObject.SetActive(true);
                _blackTurnTxt.gameObject.SetActive(false);
                break;
            case ChessSkin.Black:
                _whiteTurnTxt.gameObject.SetActive(false);
                _blackTurnTxt.gameObject.SetActive(true);
                break;
        }
    }

    
    public void OnClickRePlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
