using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public List<AudioClip> audioClipList;

    enum BGMLists
    {
        BGM,
        WHISTLE
    }

    // Start is called before the first frame update
    void Start()
    {
        // BGMを流す
        // this.GetComponent<AudioSource>().PlayOneShot(audioClipList[(int)BGMLists.BGM]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
