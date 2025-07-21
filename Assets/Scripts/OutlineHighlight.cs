using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineHighlight : MonoBehaviour
{
    public Renderer targetRenderer;
    private MaterialPropertyBlock propBlock;

    private int highlightID;
    private bool isGrabbed = false;

    void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        propBlock = new MaterialPropertyBlock();
        highlightID = Shader.PropertyToID("_IsHighlighted");

        var interactable = GetComponent<XRBaseInteractable>() ?? GetComponentInParent<XRBaseInteractable>();

        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(_ => OnHoverEntered());
            interactable.hoverExited.AddListener(_ => OnHoverExited());
            interactable.selectEntered.AddListener(_ => OnGrab());
            interactable.selectExited.AddListener(_ => OnRelease());
        }
    }

    void OnHoverEntered()
    {
        if (!isGrabbed)
            SetHighlight(true);
    }

    void OnHoverExited()
    {
        SetHighlight(false);
    }

    void OnGrab()
    {
        isGrabbed = true;
        SetHighlight(false); // turn off glow immediately when grabbed
    }

    void SetHighlight(bool on)
    {
        targetRenderer.GetPropertyBlock(propBlock, 1); // index 1 = second material slot
        propBlock.SetFloat(highlightID, on ? 1f : 0f);
        targetRenderer.SetPropertyBlock(propBlock, 1);
    }

    void OnRelease()
    {
        isGrabbed = false;
    }
}
