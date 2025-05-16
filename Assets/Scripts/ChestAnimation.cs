using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimation : MonoBehaviour
{
    Animator chestLid;

    // Start is called before the first frame update
    void Start()
    {
        chestLid = GetComponent<Animator>();
    }

    public void openLid()
    {
      
        chestLid.Play("LidOpen");

        if(Input.GetKeyDown(KeyCode.C))
        {
            chestLid.Play("LidClose");
        }
    }

    public void closeLid()
    {

        chestLid.Play("LidClose");

    }
}
