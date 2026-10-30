using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Drives an analog wall clock from the real system time.
/// Each frame it reads DateTime.Now, converts hours, minutes and seconds
/// into angles and rotates the three hands around the clock face's axis.
/// It also writes the same time to a digital display.
/// No animation clips are used - the rotations are calculated in code.
/// </summary>
public class WallClock : MonoBehaviour
{
    [Header("Analog hands (rotate around their local Z axis)")]
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    [Tooltip("Sweep the second hand smoothly instead of ticking once per second")]
    public bool smoothSecondHand = false;

    [Header("Digital display (optional)")]
    public TMP_Text digitalTime;
    public TMP_Text digitalDate;

    // Rotation of each hand at 12 o'clock, captured at start
    private Quaternion hourStart, minuteStart, secondStart;

    private void Awake()
    {
        hourStart = hourHand.localRotation;
        minuteStart = minuteHand.localRotation;
        secondStart = secondHand.localRotation;
    }

    private void Update()
    {
        DateTime now = DateTime.Now;

        // Fractional values so the minute and hour hands move continuously
        float seconds = now.Second + (smoothSecondHand ? now.Millisecond / 1000f : 0f);
        float minutes = now.Minute + seconds / 60f;
        float hours = (now.Hour % 12) + minutes / 60f;

        // 360 degrees / 60 = 6 degrees per second and per minute,
        // 360 degrees / 12 = 30 degrees per hour.
        // The face looks down the model's -Z axis, so a positive angle
        // around Z turns the hands clockwise as seen by the player.
        secondHand.localRotation = secondStart * Quaternion.Euler(0f, 0f, seconds * 6f);
        minuteHand.localRotation = minuteStart * Quaternion.Euler(0f, 0f, minutes * 6f);
        hourHand.localRotation = hourStart * Quaternion.Euler(0f, 0f, hours * 30f);

        if (digitalTime != null)
            digitalTime.text = now.ToString("HH:mm:ss");
        if (digitalDate != null)
            digitalDate.text = now.ToString("ddd dd MMM yyyy").ToUpperInvariant();
    }
}
