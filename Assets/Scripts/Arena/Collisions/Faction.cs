using UnityEngine;

public class Faction : MonoBehaviour
{
    public FactionType faction;
}

public enum FactionType
{
    Boss,
    Player,
    Other
}