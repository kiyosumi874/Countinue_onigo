using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : SceneBase
{
    [SerializeField] private int mEnemyMax;
    [SerializeField] private float mTimeLimit;
    [SerializeField] private int mFailedSceneNum;
    private int mEnemyCount;
    private float mTime;
    public void mEnemyCounter()
    {
        mEnemyCount++;
    }
    private void Timer()
    {
        Debug.Log(mTime);
        mTime += Time.deltaTime;
        if (mTime > mTimeLimit) 
        {
            SceneChange(mFailedSceneNum);
        }
    }
    private void Start()
    {
        mEnemyCount = 0;
        mTime = 0;
    }
    private void Update()
    {
        Debug.Log(mEnemyCount);
        if (mEnemyCount == mEnemyMax)
        {

            SceneChange(mNextSceneNum);
        }
        Timer();
    }
}
