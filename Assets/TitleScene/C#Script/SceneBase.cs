using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase : MonoBehaviour
{
    [SerializeField] private List<string> mNextSceneName;
    [SerializeField] protected int mNextSceneNum;
    public void OnClick()
    {
        SceneChange(mNextSceneNum);
    }
    public void SceneChange(int _NextSceneNum)
    {
        SceneManager.LoadScene(mNextSceneName[_NextSceneNum]);  
    }
}
