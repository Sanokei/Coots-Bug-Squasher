using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PixelGame
{
    public class ConsoleCommandPrefab : MonoBehaviour
    {
        public TMP_Text CommandTextGO;
        public string CommandText
        {
            get
            {
                return CommandTextGO.text;
            }
            set
            {
                CommandTextGO.text = value;
            }
        }
    }
}