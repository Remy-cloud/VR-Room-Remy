using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

/// <summary>
/// Small objective for the VR Room: put things back where they belong.
/// Counts how many of the listed sockets are filled, shows the progress
/// on a text label and plays a sound once everything is in place.
/// </summary>
public class RoomTidyGoal : MonoBehaviour
{
    [Tooltip("Sockets that must all be filled (knight, rook, pillow)")]
    public XRSocketInteractor[] sockets;

    [Tooltip("Label that shows the progress, e.g. in the wrist menu")]
    public TMP_Text progressText;

    [Tooltip("Played once when every socket is filled")]
    public AudioSource completeSound;

    public string label = "Tidy the room";
    public Color progressColor = new Color(0.56f, 0.65f, 0.75f);
    public Color doneColor = new Color(0.45f, 0.9f, 0.55f);

    private bool isComplete;
    private bool needsUpdate = true;

    private void OnEnable()
    {
        foreach (var socket in sockets)
        {
            socket.selectEntered.AddListener(OnPlaced);
            socket.selectExited.AddListener(OnRemoved);
        }
        needsUpdate = true;
    }

    private void OnDisable()
    {
        foreach (var socket in sockets)
        {
            socket.selectEntered.RemoveListener(OnPlaced);
            socket.selectExited.RemoveListener(OnRemoved);
        }
    }

    private void OnPlaced(SelectEnterEventArgs args) => needsUpdate = true;
    private void OnRemoved(SelectExitEventArgs args) => needsUpdate = true;

    // Update after the socket has finished changing its selection
    private void LateUpdate()
    {
        if (!needsUpdate) return;
        needsUpdate = false;

        int filled = 0;
        foreach (var socket in sockets)
            if (socket.hasSelection) filled++;

        bool allFilled = filled == sockets.Length;

        if (progressText != null)
        {
            progressText.text = allFilled ? "Room tidy! Well done." : $"{label}:  {filled} / {sockets.Length}";
            progressText.color = allFilled ? doneColor : progressColor;
        }

        if (allFilled && !isComplete && completeSound != null)
            completeSound.Play();

        isComplete = allFilled;
    }
}
