using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //キー入力用の文字列指定（InputManagerのHorizontalの入力を判定するための文字列）
    private string horizontal = "Horizontal";

    //キー入力用の文字列指定
    private string jump = "Jump";

    //コンポーネント取得用
    private Rigidbody2D rb;
    private Animator anim;

    //横方向の制限値
    private float limitPosX = 8.5f;

    //縦方向の制限値
    private float limitPosY = 4.45f;

    //初めてバルーンを生成したかを判定するための変数（後ほど外部スクリプトでも利用するためpublicで宣言する）
    public bool isFirstGenerateBallon;

    //向きの設定に利用する
    private float scale;

    //移動速度
    public float moveSpeed;

    //ジャンプ・浮遊力
    public float jumpPower;

    public bool isGrounded;

    //GameObject型の配列。インスペクターからヒエラルキーにあるBallonゲームオブジェクト２つをアサインする
    public GameObject[] ballons;

    //バルーンを生成する最大数
    public int maxBallonCount;

    //バルーンの生成位置の配列
    public Transform[] ballonTrans;

    //バルーンのプレファブ
    public GameObject BallonPrefab;

    //バルーンを生成する時間
    public float generateTime;

    //バルーン生成中かどうか判定する
    public bool isGenerating;

    //敵と接触した際に吹き飛ばされる力
    public float knockbackPower;

    [SerializeField, Header("Linecast用地面判定レイヤー")]
    private LayerMask groundLayer;

    [SerializeField]
    private StartChecker startChecker;

    void Start()
    {
        //必要なコンポーネントを取得して変数に代入
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        scale = transform.localScale.x;

        //配列の初期化（バルーンの最大生成数だけ配列の要素数を用意する）
        ballons = new GameObject[maxBallonCount];

    }

    void Update()
    {

        //地面接地 Physics2D.Linecastメソッドを実行してGroundLayerとキャラのコライダーが接触している距離かどうかを確認し
        //設置しているならtrue、設置していないならfalseを戻す
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, groundLayer);

        //SceneビューにPhysics2D.LinecastメソッドのLineを表示する
        Debug.DrawLine(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, Color.red, 1.0f);

        //Ballons変数の最初の要素が空でないなら = バルーンが１つ生成されるとこの要素に値が代入される = バルーンが１つあるなら
        //☆　条件を変更する　☆
        if (ballons[0] != null)
        {



            //ジャンプ
            if (Input.GetButtonDown(jump))
            {
                Jump();
            }

            // 接地していない（空中にいる）間で、落下中の場合
            if (isGrounded == false && rb.velocity.y < 0.15f)
            {
                //落下アニメを繰り返す
                anim.SetTrigger("Fall");
            }

            else
            if (isGrounded == true) 
            {
                anim.ResetTrigger("Fall");
            }

        }
        else
        {
            Debug.Log("バルーンがない。ジャンプ不可");
        }

        //Velocityの値が5.0fを超える場合（ジャンプを連続で押した場合）
        if(rb.velocity.y > 5.0f)
        {
            //Velocityの値に制限をかける（落下せずに上空で待機できる現象を防ぐため）
            rb.velocity = new Vector2(rb.velocity.x, 5.0f);
        }

        //地面に接触していて、バルーンを生成していない場合
        if(isGrounded == true && isGenerating == false)
        {
            //Qボタンを押したら
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //バルーンを１つ作成する
                StartCoroutine(GenerateBallon());
            }

        }


    }

    /// <summary>
    /// ジャンプと空中浮遊
    /// </summary>
    private void Jump()
    {
        //キャラの位置を上方向へ移動させる（ジャンプ・浮遊）
        rb.AddForce(transform.up * jumpPower);

        //jump(Up + Mid)アニメーションを再生する
        anim.SetTrigger("Jump");
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
            if (temp.x > 0)
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
            anim.SetBool("Idle", true);
        }

        //現在の位置情報が移動制限の制限範囲を超えていないか確認する。超えていたら範囲内に収める
        float posX = Mathf.Clamp(transform.position.x, -limitPosX, limitPosX);
        float posY = Mathf.Clamp(transform.position.y, -limitPosY, limitPosY);

        //現在の位置を更新（制限範囲を超えた場合、ここで移動の範囲を制限する）
        transform.position = new Vector2(posX, posY);

    }

  ///<summary>
  /// バルーン生成
  ///</summary>
  ///<returns></returns>
  private IEnumerator GenerateBallon()
    {
        //すべての配列の要素にバルーンが存在している場合には、バルーンを生成しない
        if(ballons[1] != null)
        {
            yield break;
        }

        //生成中状態にする
        isGenerating = true;

        //isFirstGenerateBallon変数の値がfalse、つまりゲームを開始してからまだバルーンを１回も生成していないのなら
        if(isFirstGenerateBallon  == false)
        {
            //初回バルーン生成を行ったと判断し、trueに変更する = 次回以降はバルーンを生成しても、if文の条件を満たさなくなり、この処理には入らない
            isFirstGenerateBallon = true;

            Debug.Log("初回のバルーン生成");

            //startChecker変数に代入されているStartCheckerスクリプトにアクセスして、SetInitialSpeedメソッドを実行する
            startChecker.SetInitialSpeed();
        }

        //１つ目の配列の要素が空なら
        if(ballons[0] == null){
            //１つ目のバルーン生成を生成して、１番目の配列へ代入
            ballons[0] = Instantiate(BallonPrefab, ballonTrans[0]);

            ballons[0].GetComponent<Ballon>().SetUpBallon(this);
        }
        else
        {
            //２つ目のバルーン生成を生成して、２番目の配列へ代入
            ballons[1] = Instantiate(BallonPrefab, ballonTrans[1]);

            ballons[1].GetComponent<Ballon>().SetUpBallon(this);
        }

        //生成時間分待機
        yield return new WaitForSeconds(generateTime);

        //生成中状態終了。再度生成できるようにする
        isGenerating = false;

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //接触するコライダーを持つゲームオブジェクトのTagがEnemyなら
        if(col.gameObject.tag == "Enemy")
        {
            //キャラと敵の位置から距離と方向を計算
            Vector3 direction = (transform.position - col.transform.position).normalized;

            //敵の反対側にキャラを吹き飛ばす
            transform.position += direction * knockbackPower;

        }
    }

    ///<summary>
    ///
    ///</summary>
    public void DestroyBallon()
    {
        //TODO 後ほど、バルーンが破壊される際に「割れた」ように見えるアニメ演出を追加する
        if (ballons[1] != null)
        {
            Destroy(ballons[1]);
        }
        else if (ballons[0] != null)
        {
            Destroy(ballons[0]);
        }

    }


}
