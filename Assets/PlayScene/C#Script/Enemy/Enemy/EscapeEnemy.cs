using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EscapeEnemy : Enemy
{
    [SerializeField] private Transform mPursuer;//追手のトランスフォーム
    [SerializeField, Range(0.0f, 1.0f)] float RotatePowerY;//回転の力
    [SerializeField, Range(1.0f, 20.0f)] private float mDesiredPower;//引き寄せられる力
    private string mPursuerTag;//追手のタグ
    private ForwardSearch mForwardSearch;
    private EnemyState.EscapeEnemyState mEnemyState;
    private NavMeshAgent mAgent;
    /// <summary>
    /// 目的地を設定
    /// </summary>
    private void SetNav()
    {
        //逃げ道
        Vector3 Direction = mPursuer.transform.position - transform.position;
        Direction += mForwardSearch.FindWayOut();
        if (!mForwardSearch.mIsDetectionGround)//逃げ道を見つけれてないなら
        {
            //右方向に旋回
            Direction.x = 1;
            Direction *= mDesiredPower;
        }
        Direction.y = transform.position.y;
        //目的地
        var Destination = transform.position - Direction;
        mAgent.SetDestination(Destination);//目的地をNavMeshに教える
    }
    ///ヨー回転させる
    private void RotateY()
    {
        transform.Rotate(0.0f,RotatePowerY, 0.0f);
    }
    // 初期化
    void Start()
    {
        mForwardSearch = this.gameObject.GetComponent<ForwardSearch>();
        mPursuerTag = mPursuer.tag;
        mEnemyState = EnemyState.EscapeEnemyState.Search;
        mAgent = this.gameObject.GetComponent<NavMeshAgent>();
        
    }
    // Update is called once per frame
    void Update()
    {
        switch (mEnemyState)
        {
            case EnemyState.EscapeEnemyState.Search:
                RotateY();
                if (mForwardSearch.ForwardRaySearchTarget(mPursuerTag))//前方に追手はいるか？
                {
                    mEnemyState++;
                }
                break;
            case EnemyState.EscapeEnemyState.Dush:
                SetNav();//逃げ道を設定p
                
                break;
        }

    }
}
