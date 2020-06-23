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

    [Header("【その他】")]
    public float limitTime; // 制限時間

    public Text treasureShipCounterText;    // 宝船カウンターのテキスト
    public int tresureShipCounter;  // 手に入れた宝船の格納

    // public bool isSetUp = false;  // ゲーム開始可能フラグ

    public GAME_STATES gameStates = GAME_STATES.OPENING;  // ゲームステータス

    // ゲーム遷移状態列挙
    public enum GAME_STATES
    {
        OPENING,    // ゲーム開始前
        PLAYING,    // ゲーム中
        ENDING      // ゲーム終了後
    }

    // Update is called once per frame
    void Update()
    {
        // ステージ未選択時は後続の処理を却下
        // if(!isSetUp)
        if(gameStates == GAME_STATES.OPENING)
        {
            return;
        }

        // if(limitTime == 0.0f)
        if (gameStates == GAME_STATES.ENDING)
        {
            // パネル表示
            gameOverPanel.SetActive(true);  // パネル表示
            gameStates = GAME_STATES.OPENING;   // ゲームステータスをOPENINGに
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
}
