# Unity Code Snippets

Collection code examples and testing in Unity 

## TODO

- Localization (Localization package, Google sheet as json)
- Particles
- Unity Analytics Manager
- Networking for Game objects
- Player Movement (2d platformer) See [Tarodev 2D Platformer Controller](https://www.youtube.com/watch?v=3sWTzMsmdx8)

## Content creators

- [NightRunStudio](https://www.youtube.com/@NightRunStudio)
- [Tarodev](https://www.youtube.com/@Tarodev)
- [git-amend](https://www.youtube.com/@git-amend)
- [Catlikecoding](https://catlikecoding.com)
- [Brackeys](https://www.youtube.com/@Brackeys)
- [Warped Imagination](https://www.youtube.com/@WarpedImagination)
- [Code Monkey](https://www.youtube.com/@CodeMonkeyUnity)
- [Jason Weimann](https://www.youtube.com/@Unity3dCollege)
- [Sebastian Lague](https://www.youtube.com/c/SebastianLague)
- [Daniel Ilett](https://www.youtube.com/c/DanielIlett)
- [Infallible Code](https://www.youtube.com/c/InfallibleCode)
- [GameDev.tv](https://www.youtube.com/@Gdevtv)
- [The GameDev Guru](https://www.youtube.com/@TheGameDevGuru)
- [Sykoo](https://www.youtube.com/@sykoo)
- [Gabriel Aguiar](https://www.youtube.com/c/GabrielAguiarProd)
- [Synty Studios](https://www.youtube.com/playlist?list=PL2QPFqe01WRkGN7J8X0Okgq7R3REbCt48)
- [Coco Code](https://www.youtube.com/@CocoCode)
- [Dilmer Valecillos](https://www.youtube.com/@dilmerv)

## Websites

- [spriters-resource.com](https://www.spriters-resource.com)
- [itch.io/game-assets](https://itch.io/game-assets)

## Tools

- Cartoony or hand-drawn looking diagrams [Excalidraw](https://excalidraw.com)
- Ask ChatGPT to create plant uml for some code, then paste at [PlantUML.com](https://plantuml.com)

## Visual Studio Shortcuts

- CTRL+K then CTRL+E: Check imports and format code.

# Building for Android

## Developer mode for phone

- Put your phone in developer mode;
- Enable USB Debugging;
- Enable Install via USB;
- Disable Verify apps over USB;

## Connecting phone to PC

Use Photo transfer mode.

## Unity settings

- Make sure your Unity install has Android SDK, NDK and OpenJDK modules installed;
- Player settings: Choose the correct minimal API level for your phone;
- Player settings: For Scripting Backend choose IL2CPP;
- Player settings: Enable ARM64 target architecture;

## Building

In the build settings screen make sure your device is available in the Run device dropdown.

## Gitignore

If you want to ignore already tracked folders.

```
git rm -r --cached "Assets/TextMesh Pro"
git commit -m "Stop tracking TextMesh Pro auto-generated files"
```