using UnityEngine;

[System.Serializable]
public class DiceData
{
    [SerializeField] private Sprite spriteDice;
    [SerializeField] private int number;

    public Sprite DiceSprite => spriteDice;
    public int Number => number;
}
