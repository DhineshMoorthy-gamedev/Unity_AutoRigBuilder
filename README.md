\# Unity\_AutoRigBuilder



🚀 \*\*Unity Auto Rig Builder\*\* is an editor tool that helps you quickly generate a basic humanoid rig setup using Unity’s Animation Rigging package.



The goal of this tool is to \*\*reduce repetitive rig setup work\*\* by automatically creating:

\- Rig hierarchy

\- Controls (Head, Arms, Legs, etc.)

\- IK constraints

\- Clean editor-friendly structure



This tool is especially useful for \*\*prototyping, gameplay rigs, and procedural characters\*\*.



---



\## ✨ Features



\- 🦴 Automatically creates a \*\*Rig GameObject\*\*

\- 🎯 Generates \*\*IK targets \& hints\*\*

\- 🧠 Supports:

&nbsp; - Head rotation control

&nbsp; - Two-Bone IK for arms and legs

\- 🛠️ Editor Window for quick setup

\- 📦 Distributed as a \*\*Unity Package (UPM-ready)\*\*



> ⚠️ Note: This is an \*\*editor utility\*\*, not a replacement for full DCC rigging tools like Blender or Maya.



---



\## 📦 Requirements



\- Unity \*\*2021.3 LTS or newer\*\* (recommended)

\- \*\*Animation Rigging\*\* package  

&nbsp; Install via Package Manager: com.unity.animation.rigging



---



\## 🔧 Installation (UPM)



You can install this package directly from GitHub:



1\. Open \*\*Unity → Package Manager\*\*

2\. Click \*\*+ → Add package from git URL\*\*

3\. Paste: https://github.com/DhineshMoorthy-gamedev/Unity\_AutoRigBuilder.git





---



\## 🚀 Usage



1\. Open Unity

2\. Go to: Tools → Auto Rig Builder

3\. Assign:

\- Animator

\- Required bones (hips, spine, head, arms, legs)

4\. Click \*\*Build Rig\*\*

5\. Adjust IK targets in Scene View



---



\## 🧩 How It Works (High Level)



\- Uses Unity’s \*\*Animation Rigging\*\* constraints

\- Creates a central `Rig` GameObject

\- Adds:

\- `TwoBoneIKConstraint` for limbs

\- Rotation control for head

\- Targets and hints are placed relative to the character proportions



---



\## 🛣️ Roadmap



Planned improvements:

\- \[ ] Hip / body control with leg bending

\- \[ ] Spine \& chest controls

\- \[ ] Mirror support (Left ↔ Right)

\- \[ ] Custom control shapes

\- \[ ] Save / Load rig presets

\- \[ ] Runtime-safe optional version



---



\## 🐛 Known Limitations



\- Works best with \*\*humanoid-like skeletons\*\*

\- No automatic bone detection (manual assignment required)

\- Not intended for final animation production rigs



---



\## 🤝 Contributing



Contributions, ideas, and feedback are welcome!



\- Fork the repo

\- Create a feature branch

\- Submit a Pull Request



---



\## 📄 License



MIT License — free to use, modify, and distribute.



---



\## 👤 Author



\*\*Dhinesh Moorthy\*\*  

Unity Developer | VR | Tools \& Systems  

GitHub: https://github.com/DhineshMoorthy-gamedev







