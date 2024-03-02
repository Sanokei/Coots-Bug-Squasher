using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

public class ConsolePanel : MonoBehaviour
{
    [SerializeField] ConsoleCommandPrefab _DialogueOptionPrefab;
    [SerializeField] VerticalGroupPanelPrefab _DialogueChoicePanel;

    List<ConsoleCommandPrefab> _DialogueOptions = new();
    
    public List<string> DialogueOptions
    {
        get
        {
            var t = _DialogueOptions.Select(ctx => ctx.OptionText).ToList();
            return t;
        }
        set
        {
            _DialogueChoicePanel.gameObject.SetActive(false);
            if(_DialogueOptions.Count > 0)
                foreach(var v in _DialogueOptions)
                {
                    Destroy(v.gameObject);
                }
            _DialogueOptions = new();
            if(value.Count > 0)
                _DialogueChoicePanel.gameObject.SetActive(true);

            int index = 0;
            foreach (string optionText in value)
            {
                ConsoleCommandPrefab option = _DialogueChoicePanel.Create(_DialogueOptionPrefab);
                option.OptionText = optionText;
                option.index = index;

                _DialogueOptions.Add(option);

                index++;
            }
        }
    }
}
