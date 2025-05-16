using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PokeButton : MonoBehaviour
{
    public Transform buttonTop;
    public float maxPushDistance = 0.02f;
    public float bounceSpeed = 4f;

    private Vector3 initialLocalPos;
    private bool isPoked = false;
    private XRBaseInteractable interactable;

    private void Start()
    {
        initialLocalPos = buttonTop.localPosition;

        interactable = GetComponent<XRBaseInteractable>();
        if (interactable)
        {
            interactable.hoverEntered.AddListener(OnHoverEntered);
            interactable.hoverExited.AddListener(OnHoverExited);
        }
    }

    private void Update()
    {
        // Clamp movement so it doesn't go too far
        Vector3 localPos = buttonTop.localPosition;
        localPos.y = Mathf.Clamp(localPos.y, initialLocalPos.y - maxPushDistance, initialLocalPos.y);
        buttonTop.localPosition = localPos;

        // Return to original position if not being poked
        if (!isPoked)
        {
            buttonTop.localPosition = Vector3.Lerp(buttonTop.localPosition, initialLocalPos, Time.deltaTime * bounceSpeed);
        }

        // Reset for next frame
        isPoked = false;
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        // Only respond to poke interactor
        if (args.interactorObject is XRPokeInteractor)
        {
            isPoked = true;
        }
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        // Optional: Stop poke early, not always needed
        isPoked = false;
    }
}
