using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public GameObject xrRig; 
    public SceneFader sceneFader;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == xrRig)
        {
            Debug.Log("Player entered portal trigger!");
            sceneFader.ChangeScene();
        }
    }
}
