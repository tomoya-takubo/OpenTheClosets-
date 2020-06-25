using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureShipImageBehavor : MonoBehaviour
{
    public float posXInitial;   // 初期値X
    public float posYInitial;   // 初期値Y

    public float moveXAbs; // X方向の単位時間当たりの移動量

    public float moveX;    // 移動量管理変数

    public int treasureShipDirection;  // 船の向き

    public float timeCntr;  // 時間経過管理変数

    private RectTransform rectTF;   // ImageのRectTransform
    // private float moveX;    // 移動量管理変数
    private float goalX;    // X座標目標値管理変数
    // private int treasureShipDirection;  // 船の向き

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置設定
        rectTF = this.GetComponent<RectTransform>();
        rectTF.localPosition = new Vector2(posXInitial, posYInitial);

        //  船の向き取得
        treasureShipDirection = (int)rectTF.localScale.x;

        // ゴール値設定
        goalX = (-1) * posXInitial;

        // 移動量取得
        moveX = moveXAbs;
    }

    // Update is called once per frame
    void Update()
    {
        // 時間更新
        timeCntr += Time.deltaTime;

        if (treasureShipDirection == 1)  // 左向き
        {
            if (rectTF.localPosition.x < goalX)     // 目標値を下回ったら、
            {
                // 初期値変更
                rectTF.localPosition = new Vector2(goalX, (-1) * rectTF.localPosition.y);

                // ゴール値再設定
                goalX *= -1;

                // 宝船向き変更
                treasureShipDirection *= -1;
                // float x = rectTF.localScale.x;
                // x *= -1;
                rectTF.localScale = new Vector2((-1) * rectTF.localScale.x, rectTF.localScale.y);

                // 移動量反転
                moveX *= -1;
            }
            else
            {
                // 移動
                rectTF.localPosition = new Vector2(rectTF.localPosition.x - moveX, rectTF.localPosition.y);

                // 回転
                // rectTF.localRotation = new Vector3(0, 0, (float)(45 * (Math.Sin(timeCntr) + 1)));
                // rectTF.localRotation = new Quaternion(rectTF.localRotation.x
                //                                        , rectTF.localRotation.y
                //                                        , (float)(45 * (Math.Sin(10 * timeCntr) + 1))
                //                                        , rectTF.localRotation.w);
            }
        }  
        else if (treasureShipDirection == -1)    // 右向き
        {
            if (rectTF.localPosition.x > goalX)     // 目標値を下回ったら、
            {
                // 初期値変更
                rectTF.localPosition = new Vector2(goalX, (-1) * rectTF.localPosition.y);

                // ゴール値再設定
                goalX *= -1;

                // 宝船向き変更
                treasureShipDirection *= -1;
                // float x = rectTF.localScale.x;
                // x *= -1;
                rectTF.localScale = new Vector2((-1) * rectTF.localScale.x, rectTF.localScale.y);

                // 移動量反転
                moveX *= -1;
            }
            else
            {
                // 移動
                rectTF.localPosition = new Vector2(rectTF.localPosition.x - moveX, rectTF.localPosition.y);
            }

        }



        /*
        if(rectTF.localPosition.x < goalX)
        {
            // 初期値変更
            rectTF.localPosition = new Vector2(goalX, (-1) * posYInitial);

            // ゴール値再設定
            goalX *= -1;
        }
        else
        {
            // 移動
            rectTF.localPosition = new Vector2(rectTF.localPosition.x - moveXAbs, rectTF.localPosition.y);
        }
        */
    }
}
