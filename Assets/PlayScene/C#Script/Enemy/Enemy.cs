using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追いかけられる敵
/// </summary> 2022/2/26 by YanoHaruto
///　2/26 とりあえず倒される処理を作った
public class Enemy : MonoBehaviour
{
    [SerializeField] private string DefeatTag;//倒される原因のタグ名

    /// <summary>
    /// 倒される
    /// </summary>
    /// <param name="TagName">倒される原因のタグ</param>
    private void Down(string TagName)
    {
        if (TagName == DefeatTag)
        {
            this.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 3/4　何かに当たったら
    /// </summary>
    /// <param name="other"> </param>
    private void OnCollisionEnter(Collision other)
    {
        Down(other.gameObject.tag);
    }
}
