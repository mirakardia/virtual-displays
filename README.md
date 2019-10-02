# Project Work 2019 fall: Virtual public displays

## Step-by-step guide to setup development environment

### 1. Git & Git LFS setup

This repository uses Git LFS for tracking large files.

1. Download Git & Git LFS for your operating system: https://git-lfs.github.com/
2. Set up Git LFS and its respective hooks by running:
    `git lfs install`

### 2. Git config
1. Set username:
    `git config --global user.name "FIRSTNAME LASTNAME"`
2. Set email:
    `git config --global user.email "YOUR_GITLAB_EMAIL"`
2. Some git features require text editor, you can set git to use external editor:
    `git config --global core.editor "'{path to editor}' -n -w"`

### 3. Local project setup
1. Create or navigate to folder you want to store your projects to.
2. Open git bash or terminal in that directory and clone repository:
    `git clone https://gitlab.com/Pygmicaesar/virtual-public-displays.git`

### 4. Set local dev branch
1. Navigate to git directory:
    `cd virtual-public-displays`
2. Create your own dev branch:
    `git checkout -b dev/FIRSTNAME`

### 5. Configure Unity for version control
With your project open in the Unity editor:

1. Open the editor settings window:
    `Edit > Project Settings > Editor`
2. Make .meta files visible to avoid broken object references:
    `Version Control / Mode: “Visible Meta Files”`
3. Use plain text serialization to avoid unresolvable merge conflicts:
    `Asset Serialization / Mode: “Force Text”`
4. Save your changes:
    `File > Save Project`

This will affect the following lines in your editor settings file:

    ProjectSettings/EditorSettings.asset
  
        m_ExternalVersionControlSupport: Visible Meta Files
        m_SerializationMode: 2

### 6. Committing changes and keeping your local version control up to date
1. After editing files, you can add changes to version control.
    Make sure you are working on your OWN dev branch!
    `git status`<br/>
    If response says "On branch dev/omanimi", then you can proceed to add and commit your changes:
    `git add . && git commit -m "Add wall collider."`<br/>
    If you are in wrong branch, do:
    `git checkout dev/omanimi`<br/>
    Push your changes to remote branch:
    `git push`
2. You can pull changes from remote branches to local. For example; from remote master to local master:
    `git checkout master && git pull && git checkout dev/omanimi`
    ALWAYS remember to checkout back to your OWN branch. DO NOT push to master!
3. Add changes from local master to local dev branch:
    `git rebase -i master`
