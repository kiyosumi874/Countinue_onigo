using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneBase : MonoBehaviour
{
    [SerializeField] private List<string> mNextSceneName;

    [SerializeField] protected AudioClip mClickAudio;
    [SerializeField] protected AudioSource mAudioSource;
    [SerializeField] private float mFadeInSeconds;
    [SerializeField] private float mFadeOutSeconds;
    [SerializeField] private Image mFadeInFadeOutPanel;
    protected const int mDefaultNextSceneNum = 0;
    private float mFadeTime;
    protected bool mIsOnClick;
    protected bool mIsComplitedFadeIn;
    protected bool mIsComplitedFadeOut;

    private void Start()
    {
        mIsComplitedFadeOut = false;
        mIsComplitedFadeIn = false;
        mIsOnClick = false;
    }
    protected void PlayAudio(AudioClip _audioClip)
    {
        mAudioSource.clip = _audioClip;
        mAudioSource.Play();
    }
    public void OnClick()
    {
        if (!mIsOnClick)
        {
            SetActiveFadeInFadeOutPanel(true);
            FadeTimeInit();
            mIsOnClick = true;
            PlayAudio(mClickAudio);
        }
    }

    protected void SceneChange(int _NextSceneNum)
    {
        SceneManager.LoadScene(mNextSceneName[_NextSceneNum]);  
    }
    private void FadeTimer()
    {
        mFadeTime += Time.deltaTime;

    }
    protected void SetActiveFadeInFadeOutPanel(bool _active)
    {
        mFadeInFadeOutPanel.gameObject.SetActive(_active);
    }
    protected void FadeIn()
    {
        if (mFadeTime > mFadeInSeconds) 
        {
            mIsComplitedFadeIn= true;
        } 
        else if (mFadeTime == 0)
        {
            mFadeInFadeOutPanel.CrossFadeAlpha(0, mFadeInSeconds, false);
        } 
        FadeTimer();
    }
    protected void FadeOut()
    {
        if (mFadeTime > mFadeInSeconds) 
        {
            mIsComplitedFadeOut= true;
        }
        else if (mFadeTime == 0)
        {
            mFadeInFadeOutPanel.CrossFadeColor(Color.black, mFadeOutSeconds, false, true);
        }
         
        FadeTimer();
    }
    protected void FadeTimeInit()
    {
        mFadeTime = 0;
    }
}
