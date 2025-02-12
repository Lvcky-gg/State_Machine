using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;
    //singleton
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }
}
