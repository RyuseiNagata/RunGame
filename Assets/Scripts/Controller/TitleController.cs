using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] Button startButton = null;
    [SerializeField] Button settingButton = null;
    

    [SerializeField] GameObject[] volumeSlider = new GameObject[3];
    [SerializeField] GameObject startObject = null;

    int switchSetting = 0;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        GameSoundManager.Instance.PlayBGM(GameSoundManager.BGMType.Title);
        startButton.onClick.AddListener(OnClickStartButton);
        settingButton.onClick.AddListener(OnSettingStartButton);
        
        for (int i = 0; i < volumeSlider.Length; i++)
        {
            volumeSlider[i].SetActive(false);
        }
    }

    // ƒ{ƒ^ƒ“UI‚ÅŽg‚¤ˆ×public
    void OnClickStartButton()
    {
        StartCoroutine(StartButton());
    }

    void OnSettingStartButton()
    {
        switchSetting++;
        GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.Click);
        if (switchSetting % 2 == 0)
        {
            startObject.SetActive(true);
            for (int i = 0; i < volumeSlider.Length; i++)
            {
                volumeSlider[i].SetActive(false);
            }
        }
        else if (switchSetting % 2 == 1)
        {
            startObject.SetActive(false);
            for (int i = 0; i < volumeSlider.Length; i++)
            {
                volumeSlider[i].SetActive(true);
            }
        }
    }

    IEnumerator StartButton()
    {
        GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.Click);
        GameSceneManager.Instance.FadeOut();
        while (GameSceneManager.Instance.IsFadeOut)
        {
            yield return null;
        }
        SceneManager.LoadScene("StageSelect");
    }
}
