using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private bool isInRange;
    private GameObject player;
    private bool actionsCanBeInvoked; //this is basically a dirty bit

    [SerializeField] UnityEvent interactActions;

    private void Update()
    {
        if (isInRange 
            && player.GetComponent<CharacterControllerScript>().HasPressedInteractButton()
            && actionsCanBeInvoked)
        {
            interactActions.Invoke();
            actionsCanBeInvoked = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            actionsCanBeInvoked = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            actionsCanBeInvoked = true;
            player = null;
        }
    }
}
