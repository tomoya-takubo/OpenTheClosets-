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

    // public ClosetGenerator clstGnrtr;   // クローゼットジェネレータ―

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
    public void ClosetOpening()
    // public IEnumerator ClosetOpening()
    {
        // たんすをあける操作
        this.closedCloset.enabled = false;  // 閉まってるクローゼットをOFFに
        this.opendCloset.enabled = true;    // 開いているクローゼットをONに

        // あたりなら
        if (win)
        {
            this.tressureShip.enabled = true;   // 宝船出現
            this.GetComponent<AudioSource>().PlayOneShot(winSE);    // あたり音
            GameMaster.isWait = true; // ウェイトをON
            // uiManager.GetTresureShip(); // GetTresureShipメソッドで宝船１こゲット

            // 宝船１こ手に入ったことを知らせる
            GameObject uiObj = GameObject.Find("UIManager");    // ヒエラルキー内で「UIManager」と名前の一致するものを取得
            uiObj.GetComponent<UIManager>().GetTresureShip();   // GetTreasureメソッド

        }
        // はずれなら
        else
        {
            this.GetComponent<AudioSource>().PlayOneShot(loseSE);    // はずれ音
        }
    }
}
