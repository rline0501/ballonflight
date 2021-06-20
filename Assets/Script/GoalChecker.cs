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

    private GameDirector gameDirector;

    [SerializeField]
    //新しく作成したGround_Set_Screenゲームオブジェクトを捜査するための変数
    private GameObject secretfloorObj;

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

            //ゴール到着
            gameDirector.GoalClear();

            //落下防止の床を表示
            secretfloorObj.SetActive(true);

            //落下防止の床を画面下からアニメさせて表示
            secretfloorObj.transform.DOLocalMoveY(0.45f, 2.5f).SetEase(Ease.Linear).SetRelative();
        
        
        }


    }

    public void SetUpGoalHouse(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;

        //TODO 他に初期設定が必要な場合はここに追加する

        //落下防止の床を非表示
        secretfloorObj.SetActive(false);
    }

}
