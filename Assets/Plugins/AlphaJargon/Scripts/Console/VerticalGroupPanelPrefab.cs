using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalGroupPanelPrefab : MonoBehaviour
{
    // Made it this structure because it doesnt work with properties
    // public CLASS name{get;private set;} // Hides in Inspector panel
    // [SerializedField] Doesn't work either
    [SerializeField] VerticalLayoutGroup _VerticalGroup;
    public VerticalLayoutGroup VerticalGroup
    {
        get
        {
            return _VerticalGroup;
        }
    }
    public int Count
    {
        get
        {
            return Children.Count;
        }
    }
    public List<GameObject> Children = new();
    public T Create<T>(T prefab) where T : Component
    {
        T go = Instantiate<T>(prefab,_VerticalGroup.transform);
        Children.Add(go.gameObject);
        return go;
    }
}
