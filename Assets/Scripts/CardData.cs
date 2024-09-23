using UnityEngine;

/// <summary>
/// This class is purely a data class. It holds the sprite to be used in-game on cards and the number value held
/// on a card. When manipulating number values this is the class that should be referenced, MoveableCard and
/// CardSlot are mostly UI classes.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Objects/Card")]
public class CardData : ScriptableObject
{
    // For Scriptable Objects it's okay to make these fields public since they can't be edited at runtime anyways
    public Sprite sprite;
    public int number;
}
