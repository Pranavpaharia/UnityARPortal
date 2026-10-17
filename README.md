# UnityARPortal

AR **portal** system for **Unity + ARCore** — open a virtual window into another world while standing in the real one.

Using ARCore plane detection, the scene tracks a real surface and renders a portal frame. Looking through it reveals a separate virtual environment rendered in real time, with correct occlusion around the frame.

## Features
- ARCore plane detection for portal placement
- Secondary virtual scene rendered through the portal
- Occlusion illusion: pass-through view around the frame

## Requirements
- Unity 2019.x (or compatible) with ARCore support
- Android device with ARCore (or Unity Remote / Playground preview)

## How to run
1. Open the project in Unity.
2. Build and deploy to an ARCore-capable Android device.
3. Point the camera at a flat surface, tap to place the portal.

## Notes
Built as a spatial-computing experiment — expects a device, not the editor Game view.

## License
MIT — see [LICENSE](LICENSE).