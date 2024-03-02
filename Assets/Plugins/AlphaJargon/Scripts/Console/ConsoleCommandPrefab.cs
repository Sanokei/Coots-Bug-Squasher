using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsoleCommandPrefab : MonoBehaviour
{
    [HideInInspector] public int index;
    public TMP_Text OptionTextGO;
    public string OptionText
    {
        get
        {
            return OptionTextGO.text;
        }
        set
        {
            OptionTextGO.text = value;
        }
    }
}