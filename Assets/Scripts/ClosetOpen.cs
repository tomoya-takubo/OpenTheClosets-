using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetOpen : MonoBehaviour
{
    // public GameMaster gM;   // ゲームマスター

    public SpriteRenderer closedCloset; // 閉じたクローゼット
    public SpriteRenderer opendCloset;  // 開いたクローゼット
    public SpriteRenderer tressureShip; // 宝船

    public bool win = false;    // 当たり有無

    public AudioClip loseSE;  // 開けたときの音（はずれ）
    public AudioClip winSE;   // 開けたときの音（あたり）

    // public UIManager uiManager; // UIマネージャー

    // public ClosetGenerator closetGenerator;   // クローゼットジェネレータ―

    // public float waitTime;  // ウェイト時間
    private float waitTime;  // ウェイト時間

    public ClosetGenerator closetGenerator; // ClosetGeneratorクラス

    public int stageNum;    // ステージ

    // public bool isWait; // ウェイトフラグ（←　ミス、これは値型）

    private bool isOpen = false;    // 開いているか（デフォルトは閉じている）

    /// <summary>
    /// クローゼット開閉プロパティ
    /// </summary>
    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
        set
        {
            isOpen = true;
        }
    }

    /// <summary>
    /// たんすをあける操作
    /// </summary>
    // public void ClosetOpening()
    public IEnumerator ClosetOpening()
    {
        // たんすをあける操作
        this.closedCloset.enabled = false;  // 閉まってるクローゼットをOFFに
        this.opendCloset.enabled = true;    // 開いているクローゼットをONに

        // あたりなら
        if (win)
        {
            this.tressureShip.enabled = true;   // 宝船出現
            this.GetComponent<AudioSource>().PlayOneShot(winSE);    // あたり音
            // this.isWait = true; // ウェイトをON
            closetGenerator.isWait = true;  // ウェイトをON
            
            // 宝船１こ手に入ったことを知らせる
            GameObject uiObj = GameObject.Find("UIManager");    // ヒエラルキー内で「UIManager」と名前の一致するものを取得
            uiObj.GetComponent<UIManager>().GetTresureShip();   // GetTreasureメソッド

            // 時間を少し止める
            yield return new WaitForSeconds(waitTime);

            // 再度初期クローゼットを生成
            closetGenerator.GenerateCloset(this.stageNum);

            // ウェイトをOFFに
            // this.isWait = false;
            closetGenerator.isWait = false;

        }
        // はずれなら
        else
        {
            this.GetComponent<AudioSource>().PlayOneShot(loseSE);    // はずれ音
        }
    }

    /// <summary>
    /// インスタンシエイト時の各値セットアップ
    /// </summary>
    /// <param name="closetGenerator"></param>
    /// <param name="stageNum"></param>
    // public void SetUp(ClosetGenerator closetGenerator, int stageNum, bool isWait)
    public void SetUp(ClosetGenerator closetGenerator, int stageNum, float waitTime)
    {
        this.closetGenerator = closetGenerator; // ClosetGeneratorクラス格納
        this.stageNum = stageNum;   // ステージ格納
        this.waitTime = waitTime;   // ウェイトタイム
        // this.isWait = isWait;   // ウェイトフラグ
    }
}
