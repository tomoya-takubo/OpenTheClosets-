using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChecker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //クリックされたら
        if (Input.GetMouseButtonDown(0))
        {
            //マウスポインタからレーザー飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //レーザーに当たったオブジェクト引っ張ってくる
            RaycastHit2D hit2d
                = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            //当たったのが自分自身だったら
            if (hit2d)
            {
                //たんすをあける
                if (hit2d.transform.gameObject.TryGetComponent(out ClosetOpen clstopn))
                {
                    //たんすをあける
                    clstopn.ClosetOpening();
                }
            }
        }
    }
}
