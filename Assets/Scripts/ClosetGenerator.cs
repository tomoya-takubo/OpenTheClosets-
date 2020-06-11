using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetGenerator : MonoBehaviour
{
    public ClosetOpen closetPrefab; //プレハブ
    public int maxClosets;  //生成するクローゼットの数
    public List<Transform> closetPositionsList = new List<Transform>(); //生成するクローゼットの各位置
    public List<ClosetOpen> closetOpenedList;

    // Start is called before the first frame update
    void Start()
    {
        //クローゼット生成
        GenerateCloset(maxClosets);
    }

    /// <summary>
    /// クローゼット生成
    /// </summary>
    private void GenerateCloset(int generateNum)
    {
        //格納リストリフレッシュ
        closetOpenedList = new List<ClosetOpen>();

        for(int i = 0; i < generateNum; i++)
        {
            //インスタンシエイト
            ClosetOpen clst = Instantiate(closetPrefab, closetPositionsList[i]);

            //リストに追加
            closetOpenedList.Add(clst);
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

        //付与
        closetOpenedList[index].win = true;
    }

}
