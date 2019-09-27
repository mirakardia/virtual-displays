# Project Work 2019 fall: Virtual public displays

## Step-by-step guide to setup development environment

### 1. Git & Git LFS setup

This repository uses Git LFS for tracking large files.

1. Download Git & Git LFS for your operating system: https://git-lfs.github.com/
2. Set up Git LFS and its respective hooks by running:
    `git lfs install`

### 2. Set your git config
    `git config --global user.name "FIRSTNAME LASTNAME"`
    `git config --global user.email "YOUR_GITLAB_EMAIL"`
    
### 3. Local project setup
1. Create or navigate to folder you want to store your projects to.
2. Open git bash or terminal in that directory and clone repository.
    `git clone https://gitlab.com/Pygmicaesar/virtual-public-displays.git`

### 4. Set local dev branch
1. Navigate to git directory
    `cd virtual-public-displays`
2. Create your own dev branch
    `git checkout -b dev/FIRSTNAME`

### 5. Configure Unity for version control

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
