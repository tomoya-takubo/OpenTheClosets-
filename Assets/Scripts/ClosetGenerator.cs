using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetGenerator : MonoBehaviour
{
    public ClosetOpen closetPrefab; //プレハブ
    public int maxClosets;  //生成するクローゼットの数
    public List<Transform> closetPositionsList = new List<Transform>(); //生成するクローゼットの各位置
    public List<ClosetOpen> closetOpenedList;   // 生成したクローゼットを格納する箱

    public Canvas canvas;   // キャンバス

    public int stageNum;   // 選んだステージの番号を格納　例）３×２：0

    public GameObject titles;   // titleとボタンのUI

    // Start is called before the first frame update
    void Start()
    {
        //クローゼット生成
        // GenerateCloset(maxClosets);
    }

    /// <summary>
    /// クローゼット生成
    /// </summary>
    public void GenerateCloset(int stageNum)
    {
        // キャンバスを消す（廃止予定）
        // canvas.transform.gameObject.SetActive(false);

        // タイトルとボタンを消す
        titles.SetActive(false);

        // ステージ番号記憶
        this.stageNum = stageNum;

        // 新しいクローゼットを生成する前に直前のクローゼットをすべて削除します
        // "Closet"というタグの付いたゲームオブジェクトをすべて削除
        // List<GameObject> gmObjs = new List<GameObject>();   // 初期化
        GameObject[] gmObjs;     // 初期化
        gmObjs = GameObject.FindGameObjectsWithTag("Closet");   // "Closet"というタグのGameObjectを取得
        for(int i = 0; i < gmObjs.Length; i++)  // 取得されたGameObjectをそれぞれ削除実施
        {
            Destroy(gmObjs[i]);  // 削除
        }

        //格納リストリフレッシュ
        closetOpenedList = new List<ClosetOpen>();
        
        // 選択されたステージのデータを取得
        ClosetData dL = closetDataList[this.stageNum];

        for (int j = 0; j < dL.column; j++)
        {
            for (int i = 0; i < dL.row; i++)
            {
                // インスタンシエイト
                ClosetOpen clst
                    = Instantiate(closetPrefab
                                , new Vector3(dL.startPos.x + j * (dL.columnPitch), dL.startPos.y + i * (dL.rowPitch), 0)
                                , Quaternion.identity);

                // スケール変更
                clst.transform.localScale = new Vector3(dL.scale, dL.scale, dL.scale);

                // リストに追加
                closetOpenedList.Add(clst);
            }
        }

        //抽選
        ChooseWin();
    }

    /// <summary>
    /// あたりのクローゼットを抽選する
    /// </summary>
    void ChooseWin()
    {
        //(index)番目が当たり
        int index = Random.Range(0, closetOpenedList.Count);    //0～closetOpendList.Count-1までを抽選
        Debug.Log((index + 1) + "番目に当たりが入っています！");

        //付与
        closetOpenedList[index].win = true;
    }

    /// <summary>
    /// ステージのデータ管理クラス
    /// </summary>
    [System.Serializable]   // ←属性情報（こうすることでインスペクター上で表示される）
    public class ClosetData
    {
        // クローゼットの行列数
        public int row; // クローゼットプレハブを並べる行
        public int column;  // クローゼットプレハブを並べる列

        // 位置情報
        public Vector2 startPos;

        // クローゼットの間隔
        public float rowPitch;  // 行間隔
        public float columnPitch;   // 列間隔

        // スケール
        public float scale; // スケール
    }
    public List<ClosetData> closetDataList; // ClosetDataクラスのリスト（３×２, ４×３, ５×４のステージ情報を格納）

}
