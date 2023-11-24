using System.Linq.Expressions;
using UnityEngine;

public class GameSoundManager : SingletonMonoBehaviour<GameSoundManager>
{
    protected override bool dontDestroyOnLoad { get { return true; } }

    public AudioClip[] BGMClips;
    public AudioClip[] SEClips;

    AudioSource[] BGMSources = new AudioSource[3];
    AudioSource[] SESources = new AudioSource[5];

    public enum BGMType
    {
        Title,
        StageSelect,
        Stage
    }

    public enum SEType
    {
        Jump,
        GravitySwitch,
        Click,
        Clear,
        Death,
    }

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < BGMSources.Length; i++)
        {
            BGMSources[i] = gameObject.AddComponent<AudioSource>();
            BGMSources[i].clip = BGMClips[i];
            BGMSources[i].loop = true;
        }

        for (int i = 0; i < SESources.Length; i++)
        {
            SESources[i] = gameObject.AddComponent<AudioSource>();
            SESources[i].clip = SEClips[i];
        }
    }

    public void PlayBGM(BGMType bgmType)
    {
        PlayBGMStop();
        BGMSources[(int)bgmType].PlayOneShot(BGMClips[(int)bgmType]);
    }

    public void PlaySE(SEType seType)
    {
        SESources[(int)seType].PlayOneShot(SEClips[(int)seType]);
    }

    public void PlayBGMStop()
    {
        for (int i = 0; i < BGMSources.Length; i++)
        {
            BGMSources[i].Stop();
        }
    }


    public void SoundSliderOnBGMValueChange(float newSliderValue)
    {
        for (int i = 0; i < BGMSources.Length; i++)
        {
            // 音楽の音量をスライドバーの値に変更
            BGMSources[i].volume = newSliderValue;
        }
       
    }

    public void SoundSliderOnSEValueChange(float newSliderValue)
    {
        for (int i = 0; i < BGMSources.Length; i++)
        {
            // 音楽の音量をスライドバーの値に変更
            SESources[i].volume = newSliderValue;
        }
    }
}
