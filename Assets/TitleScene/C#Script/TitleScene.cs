using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SceneBase
{
    public void OnClick()
    {
        SceneChange();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            SceneChange();
        }
    }
}
