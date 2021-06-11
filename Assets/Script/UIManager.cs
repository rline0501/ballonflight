using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    //txtScoreゲームオブジェクトの持つTextコンポーネントをインスペクターからアサインする
    private Text txtScore;

    [SerializeField]
    private Text txtInfo;

    [SerializeField]
    private CanvasGroup canvasGroupInfo;

    [SerializeField]
    private ResultPopUp resultPopUpPrefab;

    [SerializeField]
    private Transform canvasTran;

   ///<summary>
   ///スコア表示を更新
   /// </summary>
   ///<param name="score"></param>param>
   public void UpdateDisplayScore(int score)
    {
        txtScore.text = score.ToString();
    }

    ///<summary>
    ///ゲームオーバー表示
    /// </summary>
    public void DisplayGameOverInfo()
    {
        //InfoBackGroundゲームオブジェクトの持つanvasGroupコンポーネントのAlphaの値を
        //１秒かけて１に変更して、背景と文字が画面に見えるようにする。
        canvasGroupInfo.DOFade(1.0f, 1.0f);

        //文字列をアニメーションにさせて表示
        txtInfo.DOText("Game Over…", 1.0f);

    }


    public void GenerateResultPopUp(int score)
    {
        //ResultPopUpを生成
        ResultPopUp resultPopUp = Instantiate(resultPopUpPrefab, canvasTran, false);

        //ResultPopUpの設定を行う
        resultPopUp.SetUpResultPopUp(score);
    }

}
