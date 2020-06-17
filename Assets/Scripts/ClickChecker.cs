using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChecker : MonoBehaviour
{
    // public GameMaster gM;   // ゲームマネージャー
    public ClosetGenerator clstGnrtr;   // たんすジェネレーター
    public float waitTime;  // ウェイト時間

    // Update is called once per frame
    void Update()
    {
        //クリックされたら
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameMaster.isWait)
            {
                // ClickedClosetメソッド使用
                StartCoroutine("ClickedCloset");
            }
        }
    }

    /// <summary>
    /// クローゼットをクリックした際のメソッド
    /// </summary>
    /// <returns></returns>
    private IEnumerator ClickedCloset()
    {

        //マウスポインタからレーザー飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //レーザーに当たったオブジェクト引っ張ってくる
        RaycastHit2D hit2d
            = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

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
                    clstopn.ClosetOpening();
                    // IEnumerator clstopng = clstopn.ClosetOpening();
                    // StartCoroutine(clstopng);

                    // 当たりクローゼットかどうか確認
                    if(clstopn.tressureShip.enabled == true)
                    {
                        // 時間を少し止める
                        yield return new WaitForSeconds(waitTime);

                        // 再度初期クローゼットを生成
                        clstGnrtr.GenerateCloset(clstGnrtr.stageNum);

                        // ウェイトをOFFに
                        GameMaster.isWait = false;
                    }
                }
            }
        }
    }
}
