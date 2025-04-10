using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource flipSoundAudioSource;

    //unity변수에 들어갈부분, 다른곳에서 요청하는 각각의 해당하는 오디오소스의 교체를 위해
    [SerializeField] private AudioClip flipSuccessSoundClip;    //설정
    [SerializeField] private AudioClip flipFailSoundClip;    //설정 
    [SerializeField] private AudioClip gameBGMClip;    //설정

    public void PlayGameBGM()
    {
        bgmAudioSource.clip = gameBGMClip;
        bgmAudioSource.Play();
    }
    public void PlayFlipSuccessSound()  //나중에는 clip을 매개변수로 받아와서 함수를 하나만 만들어도 됨
    {
        flipSoundAudioSource.clip = flipSuccessSoundClip;
        flipSoundAudioSource.Play();
    }
    public void PlayFlipFailSound()
    {
        flipSoundAudioSource.clip = flipFailSoundClip;
        flipSoundAudioSource.Play();
    }
}
