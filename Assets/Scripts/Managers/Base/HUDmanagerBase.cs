using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDmanagerBase : MonoBehaviour
{
    public TMP_Text scrapsText, techText;

    void Awake()
    {

    }

    void Start()
    {
        scrapsText.text = Inventory.instance.scraps.ToString();
        techText.text = Inventory.instance.techPieces.ToString();
    }
    
}
