using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EscapeEnemy : MonoBehaviour
{
    [SerializeField] private Transform mPursuer;//追手のトランスフォーム
    [SerializeField, Range(0.1f, 5.0f)] private float mRithtTurnPower;//回転する力
    [SerializeField] ForwardSearch mForwardSearch;//前方探索してくれるやつ
   [SerializeField] private string DefeatTag;//倒される原因のタグ名
    [SerializeField] private PlayScene mPlayScene;
    private EnemyState.EscapeEnemyState mNowEnemyState;//こいつの状態
    private NavMeshAgent mAgent; //逃げ道の設定に使う
    private string mPursuerTag;//追手のタグ
    private Animator mAnimator;
    private bool mIsAlive;

    /// <summary>
    /// 倒される
    /// </summary>
    /// <param name="TagName">倒される原因のタグ</param>
    private void Down(string TagName)
    {
        if (TagName == DefeatTag)
        {
            mAnimator.Play("FallFlat");
            mNowEnemyState = EnemyState.EscapeEnemyState.Die;
        }
    }

    /// <summary>
    /// 目的地を設定
    /// </summary>
    private void SetNav()
    {
        Vector3 FindWayOut = mForwardSearch.FindWayOut();//逃げ道をもらってくる
        FindWayOut.y = transform.position.y;
        Vector3 Direction;

        Direction=(mPursuer.transform.position - transform.position).normalized;
        
        Direction.y = transform.position.y;
        //目的地
        Vector3 Destination = transform.position - Direction + FindWayOut;
        Debug.DrawRay(transform.position, Destination, Color.red);
        mAgent.SetDestination(Destination);//目的地をNavMeshに教える
    }
    // 初期化
    void Start()
    {
        mPursuerTag = mPursuer.gameObject.tag;
        mNowEnemyState = EnemyState.EscapeEnemyState.Search;
        mAgent = this.gameObject.GetComponent<NavMeshAgent>();
        mAnimator = GetComponent<Animator>();
        mIsAlive = true;
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] clipInfo = mAnimator.GetCurrentAnimatorClipInfo(0);
        switch (mNowEnemyState)//今の状態を参照
        {
            case EnemyState.EscapeEnemyState.Search:
                if (clipInfo[0].clip.name == "Turn")//曲がろうとしてたら
                {
                    transform.Rotate(new Vector3(0.0f, mRithtTurnPower, 0.0f));
                }
                //前方に追手はいるか？
                if (mForwardSearch.ForwardRaySearchTarget(mPursuerTag))
                {
                    transform.LookAt(mPursuer.position);
                    mAnimator.Play("surprised");//今やってるアニメーション中断して発見モーションに移行
                    mNowEnemyState++;
                }
                
                break;
            case EnemyState.EscapeEnemyState.Dush:

                if (clipInfo[0].clip.name == "Run")//Run状態になってから逃げ出すよ
                {
                    SetNav();//逃げ道を設定p
                }
                break;
            case EnemyState.EscapeEnemyState.Die:
                if (clipInfo[0].clip.name == "Turn"&&mIsAlive) 
                {
                    mPlayScene.mEnemyCounter();
                    mIsAlive = false;
                }
                break;
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