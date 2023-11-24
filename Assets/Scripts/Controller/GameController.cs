using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Drawing;

public class GameController : MonoBehaviour
{
    [SerializeField] Text resultText = null;
    [SerializeField] Canvas resultCanvas = null;
    [SerializeField] Button retryButton = null;
    [SerializeField] Button returnButton = null;

    string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        GameSoundManager.Instance.PlayBGM(GameSoundManager.BGMType.Stage);
        retryButton.onClick.AddListener(OnClickRetryStageButton);
        returnButton.onClick.AddListener(OnClickReturnButton);
    }



    // �{�^��UI�Ŏg����public
    void OnClickRetryStageButton()
    {
        StartCoroutine(ResultButton(sceneName));
    }

    void OnClickReturnButton()
    {
        StartCoroutine(ResultButton("StageSelect"));
    }

    IEnumerator ResultButton(string stageSelect)
    {
        GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.Click);
        GameSceneManager.Instance.FadeOut();
        while (GameSceneManager.Instance.IsFadeOut)
        {
            yield return null;
        }
        SceneManager.LoadScene(stageSelect);
    }


    // �v���C���[���Ŏ��S�����ۂɂ��̊֐����Ă�
    public void FailureGame()
    {
        resultText.color = new UnityEngine.Color(255, 0, 0, 255);
        StartCoroutine(Result("GameOver"));
    }

    // �v���C���[���ŃN���A�����ۂɂ��̊֐����Ă�
    public void ClearGame()
    {
        resultText.color = new UnityEngine.Color(0, 255, 0, 255);
        StartCoroutine(Result("  Clear "));
    }


    IEnumerator Result(string message)
    {
        resultText.text = message;
        resultCanvas.enabled = true;
        GameSoundManager.Instance.PlayBGMStop();
        yield break;
    }
}
