using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int mushroomHealthUp {get; private set; }// every script can read the value, but only this can set the value
    public event System.Action<int> OnHealthAdded;

    public void MushroomCollected()
    {
        mushroomHealthUp += 10; // Add 10 to the health;
        OnHealthAdded?.Invoke(mushroomHealthUp);
    }
}
