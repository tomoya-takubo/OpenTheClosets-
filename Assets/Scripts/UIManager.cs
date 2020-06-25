using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI使用に必須

public class UIManager : MonoBehaviour
{
    [Header("【ヒエラルキー】")]
    public Text timeText;  // 制限時間を表示するテキスト
    public GameObject treasureShipCounter;  // 宝船カウンター表示
    public GameObject gameOverPanel;    // 終了パネル表示　←（☆）パネルを画面手前に出現させることで
                                        //                          プレイヤーがそれ以上クローゼットに触れなく
                                        //                          できるようにもさせることができる
    public Text gameOverText;           // 「終わり！」の文字を表示させるText
    public GameObject RestartButton;        // ゲーム再開ボタン
    public ClosetGenerator closetGenerator; // ClosetGeneratorクラス
    public GameObject titleWithButtons;     // タイトルとボタン

    [Header("【その他】")]
    public float limitTimeMax; // 最大制限時間（←　（★）この変数は更新しない）

    public Text treasureShipCounterText;    // 宝船カウンターのテキスト
    public int tresureShipCounter;  // 手に入れた宝船の格納

    // public bool isSetUp = false;  // ゲーム開始可能フラグ

    public GAME_STATES gameStates = GAME_STATES.OPENING;  // ゲームステータス

    // private float limitTime;    // 
    public float limitTime;    // 制限時間管理変数

    // ゲーム遷移状態列挙
    public enum GAME_STATES
    {
        OPENING,    // ゲーム開始前
        PLAYING,    // ゲーム中
        ENDING      // ゲーム終了後
    }

    void Start()
    {
        // 制限時間を最大値にセット
        limitTime = limitTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(gameStates);

        // ステージ未選択時は後続の処理を却下
        // if(!isSetUp)
        if(gameStates == GAME_STATES.OPENING)
        {
            return;
        }

        // if(limitTime == 0.0f)
        if (gameStates == GAME_STATES.ENDING)
        {
            // パネルが出ていれば後続の処理を実施しない（壁）
            if(gameOverPanel.activeSelf)
            {
                return;
            }

            // パネル表示
            gameOverPanel.SetActive(true);  // パネル表示
            // timeText.gameObject.SetActive(false);   // 制限時間を表示するゲームオブジェクトを非表示
            // limitTime = limitTimeMax;   // 制限時間を最大値に回復
            // gameStates = GAME_STATES.OPENING;   // ゲームステータスをOPENINGに

            // 文字を一文字ずつ、一定の時間間隔で出力
            StartCoroutine(TextUpdate());

            // 

            Debug.Log("TextUpdateメソッドが呼ばれました");

            return;
        }

        // 制限時間カウント
        TimeCount();
    }

    /// <summary>
    /// 宝船を手に入れたときの処理
    /// </summary>
    public void GetTresureShip()
    {
        // カウンターを１増やす
        tresureShipCounter++;

        // テキスト更新
        this.treasureShipCounterText.text
             = "×　" + tresureShipCounter;
    }

    /// <summary>
    /// 制限時間をカウントするメソッド
    /// </summary>
    public void TimeCount()
    {
        // 制限時間更新
        // limitTime -= Time.deltaTime;    // フレーム分だけ時間減らす
        if ((limitTime - Time.deltaTime) < 0)
        {
            // limitTimeを0に
            limitTime = 0.0f;

            // ゲームステータスをENDINGに
            gameStates = GAME_STATES.ENDING;

        }
        else
        {
            // フレームの差分だけ時間を減らして更新
            limitTime -= Time.deltaTime;
        }

        // text更新
        timeText.text = limitTime.ToString("F2");
    }

    /// <summary>
    /// ステージ選択ボタン押下時の処理（UI系）
    /// </summary>
    public void StartGame()
    {
        // タイムカウンターUIを表示
        timeText.enabled = true;
        // 宝船カウンターUIを表示
        treasureShipCounter.SetActive(true);
        // ゲーム開始可能フラグをtrueに
        // isSetUp = true;
        // ゲームステータスをPLAYINGに
        gameStates = GAME_STATES.PLAYING;

    }

    /// <summary>
    /// ゲーム再開始
    /// </summary>
    public void Restart()
    {
        // 再スタート
        // Debug.Log("ゲームを再スタートします");

        // ゲームステータスをOPENINGに
        gameStates = GAME_STATES.OPENING;

        // インスタンシエイトしたクローゼットを初期化
        closetGenerator.CleanClosetList();

        // 「もう一回する」ボタン非表示
        RestartButton.SetActive(false);

        // GameOverPanel非表示
        gameOverPanel.SetActive(false);

        // タイムカウンター非表示
        timeText.gameObject.SetActive(false);

        // 宝船カウンター非表示
        treasureShipCounter.SetActive(false);

        // タイトルとボタン表示
        titleWithButtons.SetActive(true);

    }

    /// <summary>
    /// 文字を一文字づつ、遅らせながら表示
    /// </summary>
    private IEnumerator TextUpdate()
    {
        // テキスト初期化（前回の更新を消す）
        gameOverText.text = "";

        // 制限時間を最大値に戻す
        limitTime = limitTimeMax;

        // 変数宣言
        string finishText = "END";
        // int cnt = 0;    // 文字カウンター

        // 一文字づつ追加しながらテキストを更新
        for(int i = 0; i < finishText.Length; i++)
        {
            // iの数だけ文字表示
            gameOverText.text += finishText[i].ToString();

            // 任意の時間遅らせる
            yield return new WaitForSeconds(1.0f);
        }

        // 「もう一回する」ボタンを出す
        RestartButton.SetActive(true);

    }
}
