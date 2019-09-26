# Project Work fall 2019: Virtual public displays

Tähän vähän README.md!

## Git LFS setup

This repository uses Git LFS for tracking large files.

1. Download Git LFS for your operating system: https://git-lfs.github.com/
2. Set up Git LFS and its respective hooks by running:

    `git lfs install`

## Configure Unity for version control

With your project open in the Unity editor:

1. Open the editor settings window.

    `Edit > Project Settings > Editor`

2. Make .meta files visible to avoid broken object references.

    `Version Control / Mode: “Visible Meta Files”`

3. Use plain text serialization to avoid unresolvable merge conflicts.

    `Asset Serialization / Mode: “Force Text”`

4. Save your changes.

    `File > Save Project`

This will affect the following lines in your editor settings file:

    ProjectSettings/EditorSettings.asset
  
        m_ExternalVersionControlSupport: Visible Meta Files
        m_SerializationMode: 2
