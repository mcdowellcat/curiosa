using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedbackHelper : MonoBehaviour
{
    [Header("Short Haptic Settings")]
    public float shortAmplitude = 0.2f;
    public float shortDuration = 0.05f;

    [Header("Long Haptic Settings")]
    public float longAmplitude = 0.8f;
    public float longDuration = 0.2f;

    public void SendShortHaptic(BaseInteractionEventArgs args)
    {
        SendHapticFromInteractor(args.interactorObject, shortAmplitude, shortDuration);
    }

    public void SendLongHaptic(BaseInteractionEventArgs args)
    {
        SendHapticFromInteractor(args.interactorObject, longAmplitude, longDuration);
    }

    private void SendHapticFromInteractor(IXRInteractor interactor, float amplitude, float duration)
    {
        // Covers both Direct and Ray interactors
        if (interactor is XRBaseControllerInteractor baseControllerInteractor)
        {
            if (baseControllerInteractor.xrController != null)
            {
                baseControllerInteractor.xrController.SendHapticImpulse(amplitude, duration);
                return;
            }
        }

        // As fallback, try getting the controller from components (works if your Direct Interactor uses XRController-based setup)
        if (interactor is MonoBehaviour interactorComponent)
        {
            var controller = interactorComponent.GetComponent<XRBaseController>();
            if (controller != null)
            {
                controller.SendHapticImpulse(amplitude, duration);
            }
        }
    }
}
