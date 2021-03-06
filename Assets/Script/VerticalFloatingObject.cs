using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VerticalFloatingObject : MonoBehaviour
{
    public float moveTime;
    public float moveRange;

    //DOTweenの処理の代入用
    Tweener tweener;

    void Start()
    {
        //DOTweenによる命令を実行し、それをtweener変数に代入
        tweener = transform.DOMoveY(transform.position.y - moveRange, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

  
    private void OnDestroy()
    {
        //DOTeenの処理を破棄する(Loop処理を解消する)
        tweener.Kill();
    }
}
