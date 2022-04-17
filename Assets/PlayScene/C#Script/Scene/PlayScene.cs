using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScene : SceneBase
{
    
    [SerializeField] private float mTimeLimit;
    [SerializeField] private int mFailedSceneNum;
    [SerializeField] private Text mTimeText;
    [SerializeField] private Text mEnemyCountText;
    [SerializeField] private Text mCountDownText;
    [SerializeField] private List<GameObject> mEnemys = new List<GameObject>();
    [SerializeField] private GameObject mCountDownPanel;
    

    private int mEnemyCount;
    private int mEnemyMax;
    private float mTime;
    private const float mCountDownMaxSeconds = 3;
    private bool mIsComplitedCountDown;
    public void EnemyCounter()
    {
        mEnemyCount++;
    }
    private void Timer()
    {
        mTime += Time.deltaTime;
        if (mTime > mTimeLimit) 
        {
            FadeOut();
        }
        mTimeText.text = (mTimeLimit - mTime).ToString("N2");
    }
    private bool GameStartPreparation()
    {
        mTime -= Time.deltaTime;
        mCountDownText.text = mTime.ToString("N0");
        switch (Mathf.Ceil(mTime))
        {
            case mCountDownMaxSeconds:
                if(!mAudioSource.isPlaying)
                {
                    mAudioSource.Play();
                }
                break;
            case 1:
                mEnemysSetActive(true);
                break;
            case 0:
                mCountDownPanel.SetActive(false);
                mTime = 0;
                mIsComplitedCountDown = true;
                return true;
        }
        return false;
    }
    private void mEnemysSetActive(bool _active)
    {
        foreach (GameObject enemy in mEnemys)
        {
            enemy.SetActive(_active);
        }
    }
    private void Start()
    {
        mEnemyCount = 0;
        mIsComplitedCountDown = false;
        mEnemyMax = mEnemys.Count ;
        mEnemysSetActive(false);
        mTime = mCountDownMaxSeconds;
    }
    private void Update()
    {
        if (!mIsComplitedFadeIn)
        {
            FadeIn();
        }
        else
        {
            if (mIsComplitedCountDown)
            {
                if (mEnemyCount == mEnemyMax)
                {            
                    
                    FadeOut();
                }
                Timer();
                mEnemyCountText.text = (mEnemyMax - mEnemyCount).ToString();  

            }
            else
            {
                GameStartPreparation();
                FadeTimeInit();
            }
        }
        if(mIsComplitedFadeOut)
        {
            if (mTime > mTimeLimit)
            {
                SceneChange(mFailedSceneNum);
            }
            else
            {
                SceneChange(mDefaultNextSceneNum);
            }
        }
    }
}
