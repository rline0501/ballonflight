using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoalChecker : MonoBehaviour
{
    //移動速度
    public float moveSpeed = 0.01f;

    //停止地点。画面の右端でストップさせる。
    private float stopPos = 6.5f;

    //ゴールの重複判定防止用。一度ゴール判定したらtrueにして、ゴール判定は１回だけしか行わないようにする。
    private bool isGoal;

    void Update()
    {
        //停止地点に到着するまで移動する
        if (transform.position.x > stopPos)
        {
            transform.position += new Vector3(-moveSpeed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player" && isGoal == false)
        {
            //２回目以降はゴール判定を行わないようにするために、trueに変更する
            isGoal = true;

            Debug.Log("ゲームクリア");

            //PlayerControllerの情報を取得
            PlayerController playerController = col.gameObject.GetComponent<PlayerController>();

            //PlayerControllerの持つUIManagerの変数を利用して、GenerateResultPopUpメソッドを呼び出す
            playerController.uiManager.GenerateResultPopUp(playerController.coinPoint);
        }


    }
}
