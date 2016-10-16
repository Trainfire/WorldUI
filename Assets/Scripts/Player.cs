using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public string Name;
}

public class Player : MonoBehaviour
{
    public PlayerData Data;
}
