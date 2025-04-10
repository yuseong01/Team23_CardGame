using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource CardSoundAudioSource;
    [SerializeField] AudioSource StageStateAudioSource;


    //unity변수에 들어갈부분, 다른곳에서 요청하는 각각의 해당하는 오디오소스의 교체를 위해
    [SerializeField] private AudioClip flipSuccessSoundClip;    
    [SerializeField] private AudioClip flipFailSoundClip;     
    [SerializeField] private AudioClip gameBGMClip;    
    [SerializeField] private AudioClip setCardClip;
    [SerializeField] private AudioClip touchCardClip;
    [SerializeField] private AudioClip stageClearClip;
    [SerializeField] private AudioClip stageFailClip;

    private void Awake()
    {
        instance = this;

    }



    public void PlayGameBGM()
    {
        bgmAudioSource.clip = gameBGMClip;
        bgmAudioSource.Play();
    }
    public void PlayFlipSuccessSound()  //나중에는 clip을 매개변수로 받아와서 함수를 하나만 만들어도 됨
    {
        CardSoundAudioSource.clip = flipSuccessSoundClip;
        CardSoundAudioSource.Play();
    }
    public void PlayFlipFailSound()
    {
        CardSoundAudioSource.clip = flipFailSoundClip;
        CardSoundAudioSource.Play();
    }
    public void PlaySetCardSound()
    {
        CardSoundAudioSource.clip = setCardClip;
        CardSoundAudioSource.Play();
    }
    public void PlayTouchCardSound()
    {
        CardSoundAudioSource.clip = flipFailSoundClip;
        CardSoundAudioSource.Play();
    }
    public void PlayStageClearSound(bool stageClearCheck)
    {
        if (stageClearCheck)
        {
            StageStateAudioSource.clip = stageClearClip;

        }
        else
        {
            StageStateAudioSource.clip = stageFailClip;
        }
        StageStateAudioSource.Play();
    }
}
