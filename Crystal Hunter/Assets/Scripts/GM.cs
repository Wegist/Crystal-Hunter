using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM Instance { get; private set; }
    
    public CustomCharacterController human;
    public Monster monster;

    private void Awake()
    {
        Instance = this;
    }
}
