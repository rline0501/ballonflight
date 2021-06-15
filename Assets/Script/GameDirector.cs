using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    //ゴール地点のプレファブをアサイン
    private GoalChecker goalHousePrefab;

    [SerializeField]
    //ヒエラルキーにあるYuko_Playerゲームオブジェクトをアサイン
    private PlayerController playerController;

    [SerializeField]
    //floorGeneratorスクリプトのアタッチされているゲームオブジェクトをアサイン
    private FloorGenerator[] floorGenerators;

    [SerializeField]
    //RandomObjectGeneratorスクリプトのアタッチされているゲームオブジェクトをアサイン
    private RandomObjectGenerator[] randomObjectGenerators;

    [SerializeField]
    //ヒエラルキーにあるAudioManagerスクリプトのアタッチされているゲームオブジェクトをアサイン
    private AudioManager audioManager;

    //ゲームの準備判定用。trueになるとゲーム開始
    private bool isSetUp;

    //ゲームの終了判定用。trueになるとゲーム終了
    private bool isGameUp;

    //空中床の生成回数
    private int generateCount;

    //generateCount変数のプロパティ
    public int GenerateCount
    {
        set
        {
            generateCount = value;

            Debug.Log("生成数 / クリア目標数：" + generateCount + " / " + clearCount);

            if (generateCount >= clearCount)
            {
                //ゴール地点を生成
                GenerateGoal();

                //ゲーム終了
                GameUp();
            }
        }
        get{ 
    return generateCount;
    }
}

//ゴール地点を生成するまでに必要な空中床の生成回数
public int clearCount;

    void Start()
    {
        //タイトル曲再生
        StartCoroutine(audioManager.PlayBGM(0));

        //ゲーム開始状態にセット
        isGameUp = false;
        isSetUp = false;

        //FloorGeneratorの準備
        SetUpFloorGenerators();

        //TODO 各ジェネレータを停止
        StopGenerators();

        Debug.Log("生成停止");
    }

    /// <summary>
    /// FloorGeneratorの準備
    /// </summary>
    private void SetUpFloorGenerators()
    {
        for (int i = 0; i < floorGenerators.Length; i++)
        {
            //FloorGeneratorの準備・初期設定を行う
            floorGenerators[i].SetUpGenerator(this);
        }
    }

    void Update()
    {
        //プレイヤーが初めてバルーンを生成したら
        if(playerController.isFirstGenerateBallon && isSetUp == false)
        {
            //準備完了
            isSetUp = true;

            //TODO 各ジェネレータを動かし始める
            ActivateGenerators();

            Debug.Log("生成スタート");

            //タイトル曲を終了し、メイン曲を再生
            StartCoroutine(audioManager.PlayBGM(1));
        }
        
    }

    /// <summary>
    /// ゴール地点の生成
    /// </summary>
    private void GenerateGoal()
    {
        //ゴール地点を生成
        GoalChecker goalHouse = Instantiate(goalHousePrefab);

        //ToDO ゴール地点の初期設定
        Debug.Log("ゴール地点、生成");

        //ゴール地点の初期設定
        goalHouse.SetUpGoalHouse(this);

    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void GameUp()
    {
        //ゲーム終了
        isGameUp = true;

    //TODO 各ジェネレータを停止
    StopGenerators();

        Debug.Log("生成停止");
    }

    /// <summary>
    /// 各ジェネレータを停止する
    /// </summary>
    private void StopGenerators()
    {
        for(int i = 0; i < floorGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(false);
        }

        for(int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(false);
        }
    }

    /// <summary>
    /// 各ジェネレータを動かし始める
    /// </summary>
    private void ActivateGenerators()
    {
        for (int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(true);
        }

        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(true);
        }
    }

    /// <summary>
    /// ゴール到着
    /// </summary>
    public void GoalClear()
    {
        //クリアの曲再生
        StartCoroutine(audioManager.PlayBGM(2));
    }

}
