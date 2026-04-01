using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public int itemID;
    public string itemName;
    public Dialogue dialogue;
    
}
