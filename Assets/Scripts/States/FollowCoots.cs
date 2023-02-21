using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCoots : MonoBehaviour
{
    public GameObject Coots;
    void Update()
    {
        transform.position = new Vector3(Coots.transform.position.x, transform.position.y, transform.position.z);
    }
}
