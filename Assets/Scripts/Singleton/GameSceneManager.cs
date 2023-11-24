using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : SingletonMonoBehaviour<GameSceneManager>
{
    protected override bool dontDestroyOnLoad { get { return true; } }


    [SerializeField] Image box = null;
    [SerializeField] Image image = null;
    float angle = 400.0f;
    float changeSpeed = 15.0f;


    bool isFadeIn = false;      // FadeIn�ɂȂ������𔻒�
    public bool IsFadeIn
    {
        get { return isFadeIn; }
    }
    bool isFadeOut = true;     // FadeOut�ɂȂ������𔻒�
    public bool IsFadeOut
    {
        get { return isFadeOut; }
    }

    void Update()
    {
        if (isFadeIn)
        {
            FadeIn();
        }
    }

    void FadeIn()
    {
        StartCoroutine(FadeInMove());
    }

    // ���̃X�N���v�g��FadeOut�����
    public void FadeOut()
    {
        StartCoroutine(FadeOutMove());
    }


    IEnumerator FadeInMove()
    {
        box.enabled = true;
        image.enabled = true;

        box.transform.rotation *= Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.back);
        box.transform.localScale -= Vector3.one * changeSpeed * Time.deltaTime;
        if (box.transform.localScale.x <= 0)
        {
            isFadeOut = true;
            isFadeIn = false;

            box.enabled = false;
            image.enabled = false;

            yield return null;
        }
    }

    IEnumerator FadeOutMove()
    {
        while (true)
        {
            box.enabled = true;
            image.enabled = true;

            box.transform.rotation *= Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.back);
            box.transform.localScale += Vector3.one *  changeSpeed * Time.deltaTime;

            if (box.transform.localScale.x >= 25)
            {
                isFadeIn = true;
                isFadeOut = false;

                yield break;
            }
            yield return null;
        }
    }
}
