using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNameChange : MonoBehaviour
{
    public GameObject gameObj;
    public void Awake()
    {
        gameObj.name = "Player";
    }

}
