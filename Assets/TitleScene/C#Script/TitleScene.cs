﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SceneBase
{

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            SceneChange(mNextSceneNum);
        }
    }
}
