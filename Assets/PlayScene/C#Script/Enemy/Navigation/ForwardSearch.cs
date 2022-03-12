﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 3/10前方を探索
/// </summary>
public class ForwardSearch : MonoBehaviour
{
    [SerializeField] private string mTargetName;
    [SerializeField,Range(1.0f,100.0f)] private float mSearchRange;//探索距離
    [SerializeField] int mRayNum;//光線の数は？
    private List<Ray> mRays = new List<Ray>();
    private RaycastHit mHit;
    public bool mIsDetectionGround;              //地面は見つかった？
    /// <summary>
    /// 前方にRayを作るよ
    /// </summary>
    /// <param name="_num">Rayの作る数</param>
    public void CreateForwardRay(int _num)
    {
        for (int i = 0; i < _num; i++)
        {
            Ray ray1;
            //光線の初期化
            ray1 = Camera.main.ScreenPointToRay(transform.position);
            ray1.origin = this.gameObject.transform.position;
            //光線の方向
            Vector3 Vec = transform.forward;//真正面を代入

            if (i + 1 == _num)//最後の一本なら真正面
            {
                mRays.Add(ray1);
                break;
            }
            switch (i % 2)
            {
                case 1:
                    Vec += transform.right / (_num / 2);
                    break;
                case 0:
                    Vec -= transform.right / (_num / 2);
                    break;
            }
            ray1.direction = Vec;
            mRays.Add(ray1);
        }
    }
    /// <summary>
    /// 逃げ道を探す
    /// </summary>
    /// <returns>大まかな逃げ道の方角</returns>
    public Vector3 FindWayOut()
    {
        mIsDetectionGround = false;//逃げ道を探す前
        CreateForwardRay(mRayNum);
        Vector3 DesiredDirection;//行き止まりがあった場合の逃げ道
        DesiredDirection = new Vector3(0,0,0);
        for (int i = 0; i < mRays.Count; i++)
        {
            Ray DownDirectionRay = mRays[i];//斜め下の光線
            DownDirectionRay.direction -= transform.up;
            if (!SearchTarget(DownDirectionRay,"Ground"))//地面に当たらなかった？
            {

                mRays.Clear();
                return DesiredDirection;
            }
            else
            {
                mIsDetectionGround = true;//逃げ道をとりあえず見つけた
                DesiredDirection = mRays[i].direction;//逃げ道の大まかな方角を更新
                Debug.DrawRay(transform.position, DesiredDirection * 100, Color.green, 0.01f);//Rayの可視化
            }
        }
        mRays.Clear();
        return DesiredDirection;
    }
    /// <summary>
    /// 前方にターゲットはいるか？
    /// </summary>
    /// <param name="_TargetTag">ターゲットのタグ</param>
    /// <returns></returns>
    public bool ForwardRaySearchTarget(string _TargetTag)
    {
        CreateForwardRay(mRayNum);
        for (int i = 0; i < mRays.Count; i++)
        {
            if (SearchTarget(mRays[i], _TargetTag))
            { 
                return true; 
            }
        }
        mRays.Clear();
        return false;
    }
    /// <summary>
    /// Rayが対象に当たったかを調べる
    /// </summary>
    /// <param name="_Ray">調べるRay</param>
    /// <param name="_TargetTag">当たったか調べるタグ</param>
    /// <returns>対象に当たったならTrue</returns>
    private bool SearchTarget(Ray _Ray, string _TargetTag)
    {
        if (Physics.Raycast(_Ray, out mHit, mSearchRange))//なんか当たった？
        {
            if (mHit.collider.gameObject.tag == _TargetTag)
            {
                return true;
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        mIsDetectionGround = false;
      
    }
}
