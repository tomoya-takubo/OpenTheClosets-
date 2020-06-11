using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetOpen : MonoBehaviour
{
    public SpriteRenderer closedCloset; //閉じたクローゼット
    public SpriteRenderer opendCloset;  //開いたクローゼット
    public SpriteRenderer tressureShip; //宝船

    public bool win = false;    //あたり

    /// <summary>
    /// たんすをあける操作
    /// </summary>
    public void ClosetOpening()
    {
        //たんすをあける操作
        this.closedCloset.enabled = false;  //閉まってるクローゼットをOFFに
        this.opendCloset.enabled = true;    //開いているクローゼットをONに

        //あたりなら
        if (win)
        {
            this.tressureShip.enabled = true;   //宝船出現
        }
    }
}
