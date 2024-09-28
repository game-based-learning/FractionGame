using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A short interface for objects that contain cards
/// </summary>
public interface ICardHolder
{
    void SetHeldCard(Card card);
}
