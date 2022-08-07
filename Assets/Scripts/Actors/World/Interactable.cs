using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2f;
    bool isInteractable = false;
    public string objectName;

    public void OnTriggerEnter2D(Collider2D coll)
    {
        Player player = coll.GetComponent<Player>();
        if (player != null){
            Debug.Log("Player at shop");
            player.interaction.AddInteractable(this);
            isInteractable = true;
        }
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        Player player = coll.GetComponent<Player>();
        if (player != null){
            Debug.Log("Player left shop");
            player.interaction.RemoveInteractable(this);
            isInteractable = false;
        }
    }

    public virtual void Interact()
    {
    }
}
