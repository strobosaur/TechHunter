using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Player player;

    // INTERACTABLES
    public List<Interactable> interactables = new List<Interactable>();
    public Interactable closest;
    public System.Action onNewInteractableInRange;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    // UPDATE
    void Update()
    {
        if (interactables.Count > 0) {
            InvokeRepeating("FindClosest", 0f, 0.1f);
            UpdateScreenMsg(closest, GameManager.instance.cam);
        } else {
            CancelInvoke();
            GameManager.instance.cam.interactableText.enabled = false;
        }
    }

    // ADD INTERACTABLE
    public void AddInteractable(Interactable i)
    {
        interactables.Add(i);
    }

    // REMOVE INTERACTABLE
    public void RemoveInteractable(Interactable i)
    {
        interactables.Remove(i);
    }

    // FIND CLOSEST
    public void FindClosest()
    {
        if (interactables.Count < 1) return;

        float distance = Mathf.Infinity;
        float tempDist;
        int index = 0;
        for (int i = 0; i < interactables.Count; i++)
        {
            tempDist = Vector2.Distance(transform.position, interactables[i].transform.position);
            if (tempDist < distance)
            {
                distance = tempDist;
                index = i;
            }
        }

        closest = interactables[index];
    }

    // INTERACT WITH CLOSEST
    public void InteractClosest()
    {
        FindClosest();
        closest.Interact();
    }

    // UPDATE SCREEN MESSAGE
    public void UpdateScreenMsg(Interactable item, CameraController cam)
    {
        if (item != null)
        {
            cam.interactableText.enabled = true;
            cam.interactableText.text = item.objectName;
        } else {
            cam.interactableText.enabled = false;
        }
    }
}
