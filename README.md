# Remy's VR Room

A Meta Quest VR project built in Unity 6 for the **Enhanced Create with VR Assignment** (AR/VR Development with Unity II, ALU).
It contains two connected scenes:

- **Remy Room**: the finished project from Unity Learn's *Create with VR* course (Units 1–3, Challenges 1 and 2).
- **VR Room**: a new interactive scene built from the VR Room assets provided on the ALU Intranet.

Demo video: _add link here_

## Features

| Feature | Where | How it works |
| --- | --- | --- |
| Object grabbing | VR Room storage area, Remy Room | XR Grab Interactable (Velocity Tracking) + Rigidbody + colliders |
| Sockets (3) | Chessboard (white knight, black rook), lower bunk (pillow) | XR Socket Interactor filtered by Interaction Layers `Knight`, `Rook`, `Pillow` |
| Physics door | Glass door near the start | Door: Rigidbody + Hinge Joint (−90° to 90°). Door Handle: Rigidbody + Fixed Joint + XR Grab Interactable |
| Start UI + in-project UI | Start panel, Y menu on the left hand | World-space canvases usable with the ray or by poking |
| Scene navigation | Y menu in both scenes | Buttons call the course `LoadScene` script; Restart Room reloads the current scene |
| Analog + digital wall clock | Wall beside the glass door | `WallClock.cs` rotates the hands from `DateTime.Now`; TextMeshPro shows time and date |
| Animated hands | Both controllers, both scenes | Rigged hand models; `HandAnimator.cs` drives Grip and Trigger blend trees |
| Locomotion | Both scenes | Continuous move, turning, Teleportation Area and Anchor |

**Extras:** tidy-the-room objective (`RoomTidyGoal.cs`), background music, socket snap sounds, door latch sound, polished start panel and menus.

## Requirements

- Unity **6000.4.7f1** with Android Build Support
- Meta Quest headset with developer mode enabled
- Packages: XR Interaction Toolkit 3.1.3, OpenXR, URP

## How to run

1. Clone the repository and open the folder in Unity Hub with Unity 6000.4.7f1.
2. Open **File → Build Profiles**, select **Android**, and check the scene list is `Remy Room` (0) then `VR Room` (1).
3. Connect the Quest by USB, allow USB debugging in the headset, then click **Build And Run**.
4. The app installs as **Remy's VR Room** and starts in Remy Room.

## Controls

| Action | Control |
| --- | --- |
| Walk | Left thumbstick |
| Turn | Right thumbstick |
| Teleport | Hold A or X, aim at the floor, release |
| Grab / release | Grip button |
| Point / use | Trigger |
| Press UI buttons | Point and pull the trigger, or poke |
| Open the room menu | Y (left controller) |

## Project structure

```
Assets/
  Scenes/                Remy Room.unity, VR Room.unity
  Scripts/               WallClock.cs, HandAnimator.cs, RoomTidyGoal.cs
  Prefabs/               VR Hand Left / Right, Wall Clock
  Models/                VR Hands Rigged (model, poses, animator), Wall Clock
  Challenges/            Course challenge scenes
  _Course Library/       Create with VR course assets and scripts
  spe_arvr_resources-main/  VR Room assets from the ALU Intranet
```

## Known limitations

- The door handle can only be grabbed up close by hand, not with the ray.
- The hands are visual only and don't push objects.
- Music, the objective and socket sounds are in the VR Room only.
- Built and tested for Meta Quest only.

## Credits

- Unity Learn, *Create with VR* course assets and scripts
- ALU Intranet, VR Room assets
- Free rigged VR hands model pack
- Course library classical music (Bach, Goldberg Variations)
