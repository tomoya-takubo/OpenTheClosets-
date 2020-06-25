using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetGenerator : MonoBehaviour
{
    [Header("【ヒエラルキー】")]  // ←　インスペクター上で題目として表示（ヒエラルキー上にあるGameObjectを参照しているということがわかるように）
    public UIManager uiManager; // UIManager
    // public Canvas canvas;   // キャンバス

    [Header("【変数】")]
    public ClosetOpen closetPrefab; // プレハブ
    public int maxClosets;  // 生成するクローゼットの数
    // public List<Transform> closetPositionsList = new List<Transform>(); // （廃止）生成するクローゼットの各位置
    public List<ClosetOpen> closetList;   // 生成したクローゼットを格納する箱


    public int stageNum;   // 選んだステージの番号を格納　例）３×２：0

    public GameObject titles;   // titleとボタンのUI

    public bool isWait = false;  // ウェイトフラグ（宝船を引き当てる～次のクローゼットセットまでの待ち時間）

    public float waitTime;  // ウェイトタイム

    // Start is called before the first frame update
    void Start()
    {
        // クローゼット生成
        // GenerateCloset(maxClosets);
    }

    /// <summary>
    /// クローゼット生成
    /// </summary>
    public void GenerateCloset(int stageNum)
    {
        // キャンバスを消す（廃止予定）
        // canvas.transform.gameObject.SetActive(false);

        // ウェイトフラグOFF
        isWait = false;

        // タイトルとボタンを消す
        titles.SetActive(false);

        // ステージ番号記憶
        this.stageNum = stageNum;

        // クローゼットリスト初期化
        CleanClosetList();
        
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

                // （☆）インスタンシエイトしたときはその生成したGameObjectに何かやるチャンス！
                // この例では、生成したプレハブにこのClosetGeneratorクラスを受け渡し、
                // 後の当たりプレハブ後の再生成する際に使います
                // clst.SetUp(this, this.stageNum, this.isWait);
                clst.SetUp(this, this.stageNum, this.waitTime);

                // リストに追加
                closetList.Add(clst);
            }
        }

        // 抽選
        ChooseWin();

        // UI呼び出し
        if (uiManager.gameStates == UIManager.GAME_STATES.OPENING) // 初回であれば
        {
            // StartGameメソッド
            uiManager.StartGame();
            // Debug.Log("GameStartメソッドが呼ばれました");
        }

    }

    /// <summary>
    /// あたりのクローゼットを抽選する
    /// </summary>
    void ChooseWin()
    {
        // (index)番目が当たり
        int index = Random.Range(0, closetList.Count);    // 0～closetOpendList.Count-1までを抽選
        // Debug.Log((index + 1) + "番目に当たりが入っています！");

        // 付与
        closetList[index].win = true;
    }

    /// <summary>
    /// ステージのデータ管理クラス
    /// </summary>
    [System.Serializable]   // ←（★）属性情報（こうすることでインスペクター上で表示される）
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

    /// <summary>
    /// インスタンシエイトしたクローゼットの管理リストを初期化する
    /// </summary>
    public void CleanClosetList()
    {
        // （★）新しいクローゼットを生成する前に直前のクローゼットをすべて削除します
        // "Closet"というタグの付いたゲームオブジェクトをすべて削除
        // List<GameObject> gmObjs = new List<GameObject>();   // 初期化
        // GameObject[] gmObjs;     // 削除オブジェクト格納用配列
        // gmObjs = GameObject.FindGameObjectsWithTag("Closet");   // "Closet"というタグのGameObjectを取得
        // for(int i = 0; i < gmObjs.Length; i++)  // 取得されたGameObjectをそれぞれ削除実施
        // {
        //     Destroy(gmObjs[i]);  // 削除
        // }

        // closetOpenedListに含まれるGameObjectをすべて削除する
        // （２回目以降のクローゼット生成の直前で効果発揮）
        for (int i = 0; i < closetList.Count; i++)
        {
            // 削除
            Destroy(closetList[i].gameObject);
        }

        // 格納リストリフレッシュ
        closetList = new List<ClosetOpen>();
    }

}
