using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VerticalFloatingObject : MonoBehaviour
{
    public float moveTime;
    public float moveRange;

    //DOTween�̏����̑���p
    Tweener tweener;

    void Start()
    {
        //DOTween�ɂ�閽�߂����s���A�����tweener�ϐ��ɑ��
        tweener = transform.DOMoveY(transform.position.y - moveRange, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

  
    private void OnDestroy()
    {
        //DOTeen�̏�����j������(Loop��������������)
        tweener.Kill();
    }
}
