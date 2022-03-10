using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 3/10前方を探索
/// </summary>
public class ForwardSearch : MonoBehaviour
{
    [SerializeField] private string mTargetName;
    [SerializeField]private int mDesiredDirectionX;//右なら1,左は-1
    [SerializeField,Range(1.0f,20.0f)] private float mDesiredPower;//引き寄せられる力
    [SerializeField,Range(1.0f,100.0f)] private float mSearchRange;//探索距離
    [SerializeField] int mRayNum;//光線の数は？
    private Rays mRays;
    private RaycastHit mHit;
    private bool mIsDetectionGround;              //地面は見つかった？
    //逃げ道を探す
    private Vector3 FindWayOut(List<Ray> _rays)
    {
        Vector3 DesiredDirection;
        DesiredDirection = new Vector3(0,0,0);
        for (int i = 0; i < _rays.Count; i++)
        {
            Ray DownDirectionRay = _rays[i];//斜め下にの光線
            DownDirectionRay.direction -= transform.up;
            if (SearchTarget(DownDirectionRay,"Ground"))//地面に当たらなかった？
            {
                if (!mIsDetectionGround)//逃げ道を見つけれてないなら
                {
                    //向きたい方向に旋回
                    DesiredDirection.x = mDesiredDirectionX;
                }
                return DesiredDirection;
            }
            else
            {
                mIsDetectionGround = true;
                DesiredDirection = _rays[i].direction;//逃げ道の大まかな方角を更新
                Debug.DrawRay(transform.position, DesiredDirection * 100, Color.green, 0.01f);//Rayの可視化
            }
        }
        return DesiredDirection;
    }
    
    public bool SearchTarget(Ray _rays, string _name)
    {
        if (Physics.Raycast(_rays, out mHit, mSearchRange))//なんか当たった？
        {
            if (mHit.collider.gameObject.tag == _name)
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
        mRays = this.gameObject.AddComponent<Rays>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
