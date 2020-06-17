using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetOpen : MonoBehaviour
{
    public SpriteRenderer closedCloset; //閉じたクローゼット
    public SpriteRenderer opendCloset;  //開いたクローゼット
    public SpriteRenderer tressureShip; //宝船

    public bool win = false;    //あたり

    public AudioClip loseSE;  //開けたときの音（はずれ）
    public AudioClip winSE;   //開けたときの音（あたり）

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
    {
        // たんすをあける操作
        this.closedCloset.enabled = false;  // 閉まってるクローゼットをOFFに
        this.opendCloset.enabled = true;    // 開いているクローゼットをONに

        //あたりなら
        if (win)
        {
            this.tressureShip.enabled = true;   // 宝船出現
            this.GetComponent<AudioSource>().PlayOneShot(winSE);    // あたり音
        }
        else
        {
            //はずれなら
            this.GetComponent<AudioSource>().PlayOneShot(loseSE);    // はずれ音
        }
    }
}
