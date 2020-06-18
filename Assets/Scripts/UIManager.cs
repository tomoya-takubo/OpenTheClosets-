using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI使用に必須

public class UIManager : MonoBehaviour
{
    public Text timeText;  // 制限時間を表示するテキスト
    public float limitTime; // 制限時間

    public Text treasureShipCounterText;    // 宝船カウンターのテキスト
    public int tresureShipCounter;  // 手に入れた宝船の格納

    // Update is called once per frame
    void Update()
    {
        // 制限時間更新
        // limitTime -= Time.deltaTime;    // フレーム分だけ時間減らす
        if((limitTime - Time.deltaTime) < 0)
        {
            // limitTimeを0に
            limitTime = 0.0f;
        }
        else
        {
            // フレームの差分だけ減らして更新
            limitTime -= Time.deltaTime;
        }

        // text更新
        timeText.text = limitTime.ToString("F2");
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
}
