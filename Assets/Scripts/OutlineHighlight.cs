using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineHighlight : MonoBehaviour
{
    public Renderer targetRenderer;
    private MaterialPropertyBlock propBlock;

    private int highlightID;

    void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        propBlock = new MaterialPropertyBlock();
        highlightID = Shader.PropertyToID("_IsHighlighted");

        var interactable = GetComponent<XRBaseInteractable>() ?? GetComponentInParent<XRBaseInteractable>();

        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(_ => SetHighlight(true));
            interactable.hoverExited.AddListener(_ => SetHighlight(false));
        }
    }

    void SetHighlight(bool on)
    {
        targetRenderer.GetPropertyBlock(propBlock, 1); // index 1 = second material slot
        propBlock.SetFloat(highlightID, on ? 1f : 0f);
        targetRenderer.SetPropertyBlock(propBlock, 1);
    }
}
