using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : SceneBase
{
    [SerializeField] private int mEnemyMax;
    [SerializeField] private float mTimeLimit;
    private int mEnemyCount;
    private float mTime;
    public void mEnemyCounter()
    {
        mEnemyCount++;
    }
    private void Timer()
    {
        mTime += Time.deltaTime;
        if (mTime > mTimeLimit) 
        {
            SceneChange();
        }
    }
    private void Start()
    {
        mEnemyCount = 0;
        mTime = 0;
    }
    private void Update()
    {
        if(mEnemyCount==mEnemyMax)
        {
            SceneChange();
        }
        Timer();
    }
}
