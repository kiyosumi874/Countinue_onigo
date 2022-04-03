using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 3/10前方を探索
/// </summary>
public class ForwardSearch : MonoBehaviour
{
    [SerializeField] int mOddRayNum;               //光線の数　奇数にしてね
    [SerializeField, Range(1.0f, 45.0f)] float mRayDeg;
    [SerializeField,Range(1.0f,10.0f)] int mDownPercentage;
    [SerializeField,Range(1.0f,100.0f)] private float mSearchRange;//探索距離   
    //[SerializeField, Range(1.0f, 19.0f)] private int mCurvePower;
    private List<Ray> mRays = new List<Ray>();   
    private RaycastHit mHit;                     //Rayに当たった対象

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
            if (mHit.collider.gameObject.CompareTag(_TargetTag))//ターゲットに当たったみたい
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// メンバーであるmRaysに追加する.
    /// </summary>
    /// <param name="_origin">Rayの出す位置</param>
    /// <param name="_direction">Rayが出る方角</param>
    private void AddRay(Vector3 _origin,Vector3 _direction)
    {
        Ray ray;
        //光線の初期化
        ray = Camera.main.ScreenPointToRay(transform.position);//ようわかっとらんのでカメラ位置を代入
        ray.origin = _origin;
        ray.direction = _direction;
        Debug.DrawRay(transform.position, ray.direction * mSearchRange, Color.red, 0.01f);//可視化
        mRays.Add(ray);
    }
    /// <summary>
    /// 前方にRayを作るよ
    /// </summary>
    /// <param name="_num">Rayの作る数</param>
    /// <param name="_yVec">上とか下方向にある程度向けれる</param>
    private void CreateForwardRay(int _num,Vector3 _yVec)
    {
        int n = 0;//
        for (int i = 0; i < _num; i++) 
        {
            Vector3 vec1 = transform.forward;
            Vector3 vec2;
            if (i % 2 == 0) 
            {
                vec2 = Quaternion.Euler(0, mRayDeg * n, 0) * vec1;//前方ベクトルをmRayDeg分回転させる
                AddRay(transform.position, vec2 + _yVec);//右方向Ray追加
            }
            else
            {
                n++;
                vec2 = Quaternion.Euler(0, -mRayDeg * n, 0) * vec1;
                AddRay(transform.position, vec2 + _yVec);//左方向Ray追加
            }
        }

    }
    
    /// <summary>
    /// 前方下探索して逃げ道を探す
    /// </summary>
    /// <returns>大まかな逃げ道の方角</returns>
    public Vector3 FindWayOut()
    {      
        Vector3 WayOutDirection = new Vector3(0,0,0);//逃げる方向
        Vector3 Down = -transform.up / mDownPercentage;//下向き
        bool IsDetectionGround = false;//自分が歩ける地面を見つけているか
        CreateForwardRay(mOddRayNum,Down);//前方下向きに探索させる
        for (int i = 0; i < mRays.Count; i++)
        {
            Debug.DrawRay(transform.position, mRays[i].direction * 100, Color.black, 0.01f);//Rayの可視化
            if (SearchTarget(mRays[i], "Ground"))//地面に当たった？
            {
                Debug.Log("Ground");
                IsDetectionGround = true;//逃げ道をとりあえず見つけた
                WayOutDirection = mRays[i].direction - Down;//逃げ道の大まかな方角を更新

            }
            else
            {        
                if (!IsDetectionGround)//逃げ道を見つけれてないなら
                {
                    //右方向に旋回
                    WayOutDirection = transform.right;
                }

            }
        }
        
        mRays.Clear();
        return WayOutDirection;
    }
    /// <summary>
    /// 前方にターゲットはいるか？
    /// </summary>
    /// <param name="_TargetTag">ターゲットのタグ</param>
    /// <returns>ターゲットがいた？</returns>
    public bool ForwardRaySearchTarget(string _TargetTag)
    {
        CreateForwardRay(mOddRayNum,new Vector3(0,0,0));

        for (int i = 0; i < mRays.Count; i++)
        {
            if (SearchTarget(mRays[i], _TargetTag))//TargetTagの敵がRay上にいるか
            {
                mRays.Clear();//居たなら光線を消すよ
                return true; //居た
            }
        }
        mRays.Clear();
        return false;//居なかった
    }

    // Start is called before the first frame update
    void Start()
    {
    }
}