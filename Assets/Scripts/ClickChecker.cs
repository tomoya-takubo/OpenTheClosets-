using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChecker : MonoBehaviour
{
    // public GameMaster gM;   // ゲームマネージャー
    public ClosetGenerator closetGenerator;   // たんすジェネレーター
    // public float waitTime;  // ウェイト時間

    // Update is called once per frame
    void Update()
    {
        // （☆）【小技】returnの壁を作って後ろの処理をさせない
        if(closetGenerator.isWait)
        {
            return;
        }

        // クリックされたら
        if(Input.GetMouseButtonDown(0))
        {
            // clickedClosetメソッド使用
            ClickedCloset();
        }

        /*
        //クリックされたら
        if (Input.GetMouseButtonDown(0))
        {
            if (!closetGenerator.isWait)
            {
                // ClickedClosetメソッド使用
                // StartCoroutine("ClickedCloset");
                // StartCoroutine(ClickedCloset());    // ←　メソッドで指定できる！
                ClickedCloset();
            }
        }
        */
    }

    /// <summary>
    /// クローゼットをクリックした際のメソッド
    /// </summary>
    /// <returns></returns>
    // private IEnumerator ClickedCloset()
    private void ClickedCloset()
    {
        //マウスポインタからレーザー飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //レーザーに当たったオブジェクト引っ張ってくる
        RaycastHit2D hit2d
            = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

        // 何にも当たらなければ終了
        if(!hit2d)
        {
            return;
        }

        // それがたんすなら、たんすをあける
        if (hit2d.transform.gameObject.TryGetComponent(out ClosetOpen clstopn))
        {
            if (!clstopn.IsOpen)
            {
                // 開いたフラグをONに
                clstopn.IsOpen = true;

                // たんすをあける
                StartCoroutine(clstopn.ClosetOpening());
            }
        }

        /*
        // 何かに当たれば
        if (hit2d)
        {
            // それがたんすなら、たんすをあける
            if (hit2d.transform.gameObject.TryGetComponent(out ClosetOpen clstopn))
            {
                if (!clstopn.IsOpen)
                {
                    // 開いたフラグをONに
                    clstopn.IsOpen = true;

                    // たんすをあける
                    // clstopn.ClosetOpening();
                    // IEnumerator clstopng = clstopn.ClosetOpening();
                    // StartCoroutine(clstopng);
                    StartCoroutine(clstopn.ClosetOpening());

                    // 当たりクローゼットかどうか確認
                    // if(clstopn.tressureShip.enabled == true)
                    // {
                    //     // 時間を少し止める
                    //     yield return new WaitForSeconds(waitTime);
                    // 
                    //     // 再度初期クローゼットを生成
                    //     closetGenerator.GenerateCloset(closetGenerator.stageNum);
                    // 
                    //     // ウェイトをOFFに
                    //     GameMaster.isWait = false;
                    // }
                }
            }
        }
        */
    }
}
