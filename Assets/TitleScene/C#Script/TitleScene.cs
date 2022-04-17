using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SceneBase
{
    private bool mIsStartFadeOut;
    private void Start()
    {
        mIsStartFadeOut = false;
    }
    private void Update()
    {
        if (mIsComplitedFadeIn)
        {
            SetActiveFadeInFadeOutPanel(false);
            if(mIsStartFadeOut)
            {
                SetActiveFadeInFadeOutPanel(true);
               FadeOut();
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                mIsStartFadeOut = true;
                FadeTimeInit();
            }
        }
        else { FadeIn(); }
        if (mIsOnClick)
        {
            FadeOut();
        }
        if (mIsComplitedFadeOut)
        {
            SceneChange(mDefaultNextSceneNum);
        }
    }
}
