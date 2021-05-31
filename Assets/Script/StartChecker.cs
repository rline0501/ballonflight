using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChecker : MonoBehaviour
{
    //MoveObject宿里応￥￥ぷとを取得した際に代入するための準備
    private MoveObject moveObject;

    void Start()
    {
        //このスクリプトがアタッチされているゲームオブジェクトの持つ、MoveObjectスクリプトを探して取得し、moveObject変数に代入
        moveObject = GetComponent<MoveObject>();
    }

    ///<summary>
    ///空中床に移動速度を与える
    ///</summary>

    public void SetInitialSpeed()
    {
        ///アサインしているゲームオブジェクトの持つMoveObjectスクリプトのmoveSpeed変数にアクセスして、右辺の値を代入する
        moveObject.moveSpeed = 0.005f;

    }
}
