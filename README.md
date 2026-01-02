# Unity Auto Rig Builder

🚀 **Unity Auto Rig Builder** is an editor tool that helps you quickly generate a basic humanoid rig setup using Unity's Animation Rigging package.

The goal of this tool is to **reduce repetitive rig setup work** by automatically creating:
- Rig hierarchy
- Controls (Head, Arms, Legs, etc.)
- IK constraints
- Clean editor-friendly structure

This tool is especially useful for **prototyping, gameplay rigs, and procedural characters**.

---

## ✨ Features

- 🦴 Automatically creates a **Rig GameObject**
- 🎯 Generates **IK targets & hints**
- 🧠 Supports:
  - Head rotation control
  - Two-Bone IK for arms and legs
- 🛠️ Editor Window for quick setup
- 📦 Distributed as a **Unity Package (UPM-ready)**

> ⚠️ **Note:** This is an **editor utility**, not a replacement for full DCC rigging tools like Blender or Maya.

---

## 📦 Requirements

- Unity **2021.3 LTS or newer** (recommended)
- **Animation Rigging** package  
  Install via Package Manager: `com.unity.animation.rigging`

---

## 🔧 Installation

You can install this package directly from GitHub using Unity Package Manager:

1. Open **Unity → Window → Package Manager**
2. Click **+ → Add package from git URL**
3. Paste the following URL:
   ```
   https://github.com/DhineshMoorthy-gamedev/Unity_AutoRigBuilder.git
   ```
4. Click **Add**

---

## 🚀 Usage

1. Open Unity
2. Navigate to: **Tools → Auto Rig Builder**
3. In the editor window, assign:
   - Animator component
   - Required bones (hips, spine, head, arms, legs)
4. Click **Build Rig**
5. Adjust IK targets in the Scene View as needed

---

## 🧩 How It Works

- Uses Unity's **Animation Rigging** package constraints
- Creates a central `Rig` GameObject structure
- Automatically adds:
  - `TwoBoneIKConstraint` for limbs (arms and legs)
  - Rotation control for head
  - Targets and hints positioned relative to character proportions

---

## 🛣️ Roadmap

Planned improvements and features:

- [ ] Hip/body control with leg bending
- [ ] Spine & chest controls
- [ ] Mirror support (Left ↔ Right)
- [ ] Custom control shapes
- [ ] Save/Load rig presets
- [ ] Runtime-safe optional version

---

## 🐛 Known Limitations

- Works best with **humanoid-like skeletons**
- No automatic bone detection (manual assignment required)
- Not intended for final animation production rigs
- Best suited for prototyping and gameplay purposes

---

## 🤝 Contributing

Contributions, ideas, and feedback are welcome!

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License - free to use, modify, and distribute.

---

## 👤 Author

**Dhinesh Moorthy**  
Unity Developer | VR | Tools & Systems  
🔗 [GitHub Profile](https://github.com/DhineshMoorthy-gamedev)

---

## 📞 Support

If you encounter any issues or have questions:
- Open an [Issue](https://github.com/DhineshMoorthy-gamedev/Unity_AutoRigBuilder/issues)
- Check existing discussions and documentation

---

**⭐ If you find this tool helpful, please consider giving it a star!**