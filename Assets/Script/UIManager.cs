using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    //txtScoreゲームオブジェクトの持つTextコンポーネントをインスペクターからアサインする
    private Text txtScore;

   ///<summary>
   ///スコア表示を更新
   /// </summary>
   ///<param name="score"></param>param>
   public void UpdateDisplayScore(int score)
    {
        txtScore.text = score.ToString();
    }
}
