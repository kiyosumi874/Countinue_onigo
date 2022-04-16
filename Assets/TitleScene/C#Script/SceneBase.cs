using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase : MonoBehaviour
{
    [SerializeField] private string mNextSceneName;
    public void SceneChange()
    {
        SceneManager.LoadScene(mNextSceneName);  
    }
}
