using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rays : MonoBehaviour
{
    public List<Ray> mRays = new List<Ray>();

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

            if(i + 1 == _num)//最後の一本なら真正面
            {
                mRays.Add(ray1);
                break;
            }
            switch(i % 2)
            {
                case 1:
                    Vec += transform.right / (_num / 2) ;
                    break;
                case 0:
                    Vec -= transform.right / (_num / 2) ;
                    break;
            }
            ray1.direction = Vec;
            mRays.Add(ray1);
        }
    }
}
