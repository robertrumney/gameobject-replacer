# GameObjectReplacer for Unity

`GameObjectReplacer` is a Unity Editor tool that allows you to seamlessly replace selected objects in the hierarchy with a prefab of your choice. It provides options to match the scale, rotation, and position of the original objects to ensure a smooth transition in your scene.

## Features

- **Replace Multiple GameObjects**: Select and replace multiple GameObjects in the hierarchy in one go.
- **Match Transformations**: Choose to match the position, rotation, and scale of the original objects.
- **Non-destructive**: Uses Unity's `Undo` system to ensure the operations are reversible.
- **Edit Mode Only**: Ensures that replacements can only be done in Edit mode to prevent accidental changes during Play mode.

## Installation

1. Copy the `GameObjectReplacer.cs` script into your Unity project's `Editor` folder.
2. Once Unity compiles the script, you'll see a new menu option under `Window` titled `GameObject Replacer`.

## Usage

1. Open the tool from `Window > GameObject Replacer`.
2. Select the objects in the hierarchy that you want to replace.
3. Drag and drop the desired prefab into the "Prefab" field in the `GameObject Replacer` window.
4. Choose the transformations (position, rotation, scale) you want to match.
5. Click "Replace" to replace the selected objects with the chosen prefab.

## Contributing

Feel free to fork the repository and submit pull requests for any enhancements or bug fixes. All contributions are welcome!

## License

This project is licensed under the MIT License. See `LICENSE` for more information.
