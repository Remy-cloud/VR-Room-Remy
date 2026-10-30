using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Animates a hand model from controller input.
/// Reads the analog grip and trigger values (0 to 1) and passes them to
/// the Animator's "Grip" and "Trigger" parameters:
///  - Grip blends middle, ring, pinky and thumb from open to fist (base layer)
///  - Trigger (or grip) blends the index finger (masked layer)
/// </summary>
[RequireComponent(typeof(Animator))]
public class HandAnimator : MonoBehaviour
{
    [Tooltip("Analog grip value, e.g. XRI Right Interaction/Select Value")]
    public InputActionProperty gripAction;

    [Tooltip("Analog trigger value, e.g. XRI Right Interaction/Activate Value")]
    public InputActionProperty triggerAction;

    [Tooltip("How quickly the fingers follow the input (higher = snappier)")]
    public float responsiveness = 20f;

    private static readonly int GripId = Animator.StringToHash("Grip");
    private static readonly int TriggerId = Animator.StringToHash("Trigger");

    private Animator animator;
    private float grip;
    private float trigger;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gripAction.action?.Enable();
        triggerAction.action?.Enable();
    }

    private void Update()
    {
        float targetGrip = gripAction.action != null ? gripAction.action.ReadValue<float>() : 0f;
        float targetTrigger = triggerAction.action != null ? triggerAction.action.ReadValue<float>() : 0f;

        // Frame-rate independent smoothing so fingers move naturally
        float t = 1f - Mathf.Exp(-responsiveness * Time.deltaTime);
        grip = Mathf.Lerp(grip, targetGrip, t);
        trigger = Mathf.Lerp(trigger, targetTrigger, t);

        animator.SetFloat(GripId, grip);
        // The index finger follows the trigger, but also closes with the grip,
        // so squeezing grip makes a full fist instead of a pointing hand.
        animator.SetFloat(TriggerId, Mathf.Max(grip, trigger));
    }
}
