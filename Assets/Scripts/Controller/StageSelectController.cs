using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour
{
    [SerializeField] Button stage1Button = null;
    [SerializeField] Button stage2Button = null;
    [SerializeField] Button stage3Button = null;
    [SerializeField] Button stage4Button = null;
    [SerializeField] Button stage5Button = null;
    [SerializeField] Button backMenu = null;


    // Start is called before the first frame update
    void Start()
    {
        GameSoundManager.Instance.PlayBGM(GameSoundManager.BGMType.StageSelect);
        stage1Button.onClick.AddListener(OnClickStage1Button);
        stage2Button.onClick.AddListener(OnClickStage2Button);
        stage3Button.onClick.AddListener(OnClickStage3Button);
        stage4Button.onClick.AddListener(OnClickStage4Button);
        stage5Button.onClick.AddListener(OnClickStage5Button);
        backMenu.onClick.AddListener(OnClickBackMenuButton);
    }



    // ƒ{ƒ^ƒ“UI‚ÅŽg‚¤ˆ×public
    void OnClickStage1Button()
    {
        StartCoroutine(StageSelectButton("Stage1"));
    }

    void OnClickStage2Button()
    {
        StartCoroutine(StageSelectButton("Stage2"));
    }

    void OnClickStage3Button()
    {
        StartCoroutine(StageSelectButton("Stage3"));
    }

    void OnClickStage4Button()
    {
        StartCoroutine(StageSelectButton("Stage4"));
    }

    void OnClickStage5Button()
    {
        StartCoroutine(StageSelectButton("Stage5"));
    }

    void OnClickBackMenuButton()
    {
        StartCoroutine(StageSelectButton("TitleScene"));
    }

    IEnumerator StageSelectButton(string stageSelect)
    {
        GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.Click);
        GameSceneManager.Instance.FadeOut();
        while (GameSceneManager.Instance.IsFadeOut)
        {
            yield return null;
        }
        SceneManager.LoadScene(stageSelect);
    }
}
