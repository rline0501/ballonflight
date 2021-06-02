using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VerticalFloatingObject : MonoBehaviour
{
    public float moveTime;
    public float moveRange;

    //DOTween‚Ìˆ—‚Ì‘ã“ü—p
    Tweener tweener;

    void Start()
    {
        //DOTween‚É‚æ‚é–½—ß‚ğÀs‚µA‚»‚ê‚ğtweener•Ï”‚É‘ã“ü
        tweener = transform.DOMoveY(transform.position.y - moveRange, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

  
    private void OnDestroy()
    {
        //DOTeen‚Ìˆ—‚ğ”jŠü‚·‚é(Loopˆ—‚ğ‰ğÁ‚·‚é)
        tweener.Kill();
    }
}
