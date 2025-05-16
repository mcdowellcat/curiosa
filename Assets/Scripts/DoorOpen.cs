using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Animator door;

    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<Animator>();
    }


    public void openDoor()
    {
      
        door.Play("DoorOpener");

    }
}
