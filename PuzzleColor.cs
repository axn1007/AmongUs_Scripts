using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleColor : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickColorChange()
    {
        print("클릭");
        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Ming);
        RawImage im = GetComponent<RawImage>();
        //Image im = GetComponent<Image>();
        im.color = Color.gray;

        //클릭하면 다시 못누르게
        Button btn = GetComponentInChildren <Button>();
        btn.interactable = false;

        //클릭한 값 담기
        Text tx = GetComponentInChildren<Text>();
        string ct = tx.text;
        Puzzle.instance.clickValue.Add(ct);

        
    }
}
