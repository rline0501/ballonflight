using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //キー入力用の文字列指定（InputManagerのHorizontalの入力を判定するための文字列）
    private string horizontal = "Horizontal";

    //コンポーネント取得用
    private Rigidbody2D rb;

    private Animator anim;

    //向きの設定に利用する
    private float scale;

    //移動速度
    public float moveSpeed;
   
    void Start()
    {
        //必要なコンポーネントを取得して変数に代入
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        scale = transform.localScale.x;

    }

    void FixedUpdate()
    {
        //移動
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        //水平（横）方向への入力受付
        //InputManagerのHorizontalに登録されているキーの入力があるかどうかの確認を行う
        float x = Input.GetAxis(horizontal);

        //xの値が０でない場合　＝　キー入力がある場合
        if (x != 0)
        {
            //velocity（速度）に新しい値を代入して移動
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

            //temp変数に現在のlocalScale値を代入
            Vector3 temp = transform.localScale;

            //現在のキー入力値xをtemp.xに代入
            temp.x = x;
            
            //向きが変わるときに小数になるとキャラが縮んで見えてしまうので整数値にする
            if(temp.x > 0)
            {
                //数字が０より大きければすべて１にする
                temp.x = scale;
            }
            else
            {
                //数字が０よりも小さければすべてー１にする
                temp.x = -scale;

            }

            //キャラの向きを移動方向に合わせる
            transform.localScale = temp;


            //待機状態のアニメの再生を止めて、走るアニメの再生への遷移を行う
            
            //☆Idleアニメーションをfalseにして、待機アニメーションを停止する
            anim.SetBool("Idle", false);
            
            //☆Runアニメーションに対して、0.5fの値を情報として渡す
            //遷移条件がgreater 0.1なので0.1以上の値を渡すと条件が成立してRunアニメーションが再生される
            anim.SetFloat("Run", 0.5f);

        }
        else
        {
            //左右の入力がなかったら横移動の速度を０にしてピタッと止まるようにする
            rb.velocity = new Vector2(0, rb.velocity.y);


            //走るアニメの再生を止めて待機状態のアニメの再生への遷移を行う
            
            //☆Runアニメーションに対して、0.fの値を情報として渡す。
            //遷移条件がless 0.1なので、0.1以下の値を渡すと条件が成立してRunアニメーションが停止される
            anim.SetFloat("Run", 0.0f);

            //☆Idleアニメーションをtrueにして、待機アニメーションを再生する
            anim.SetBool("Idol", true);
        }
    }


}
