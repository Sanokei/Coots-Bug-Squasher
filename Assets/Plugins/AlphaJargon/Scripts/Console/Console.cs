using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

namespace PixelGame
{
    public class Console : MonoBehaviour
    {
        public static Console Instance{get;private set;}
        [SerializeField] ConsoleCommandPrefab _ConsoleCommandPrefab;
        [SerializeField] VerticalGroupPanelPrefab _ConsolePanel;

        List<ConsoleCommandPrefab> _Commands = new();
        void Awake()
        {
            if(!Instance)
                Instance = this;
            else
                Destroy(Instance);
        }
        void OnEnable()
        {
            MoonSharp.Interpreter.ScriptRuntimeException.onException += add;
        }
        void OnDisable()
        {
            MoonSharp.Interpreter.ScriptRuntimeException.onException -= add;
        }
        
        public List<string> ConsolePanel
        {
            get
            {
                var t = _Commands.Select(ctx => ctx.CommandText).ToList();
                return t;
            }
            set
            {
                removeAll();
                addAll(value);
            }
        }

        public void add(string commandText)
        {
            add(commandText, Color.red);
        }
        public void add(string commandText, Color color)
        {
            ConsoleCommandPrefab option = _ConsolePanel.Create(_ConsoleCommandPrefab);
            option.CommandText = commandText;
            option.CommandTextGO.color = color;
            _Commands.Add(option);
        }

        public void removeAll()
        {
            foreach(ConsoleCommandPrefab v in _Commands)
            {
                Destroy(v.gameObject);
            }
            _Commands = new();
        }

        public void addAll(List<string> value)
        {
            foreach (string commandText in value)
            {
                add(commandText);
            }
        }
    }
}