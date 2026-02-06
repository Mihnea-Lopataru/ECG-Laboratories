# Laboratory 1 — Unity Setup and First Interactive Scene

## Objectives

By the end of this laboratory, students will be able to:

- Install and configure **Unity Hub** and **Unity 6 LTS**
- Create and open a Unity project
- Understand the Unity Editor layout and basic concepts
- Load an existing Unity laboratory from GitHub
- Run and interact with a simple physics-based scene
- Observe how input, physics, and camera systems work together

This laboratory serves as a **technical onboarding** and a **testing ground** for future labs.

---

## 1. Installing Unity

### 1.1 Install Unity Hub

Unity Hub is the official application used to manage Unity versions and projects.

Download Unity Hub from:
https://unity.com/download

Install it using the default options for your operating system.

---

### 1.2 Install Unity Editor (Required Version)

This course uses a **specific Unity version** to ensure consistency.

**Required version:**
- Unity 6 LTS — **6000.3.6f1**

Steps:
1. Open **Unity Hub**
2. Go to the **Installs** tab
3. Click **Install Editor**
4. Select **Unity 6 LTS (6000.3.6f1)**
5. During installation, make sure the following modules are selected:
   - Windows Build Support (or macOS/Linux depending on your OS)
   - Visual Studio (or another C# IDE)

---

## 2. Creating Your First Unity Project

### 2.1 Create a New Project

1. Open **Unity Hub**
2. Go to the **Projects** tab
3. Click **New Project**
4. Select the **3D (URP)** template
5. Name the project:
   ```
   ECG_Lab_Test
   ```
6. Choose a suitable location and click **Create Project**

---

### 2.2 Unity Editor Overview

- **Hierarchy**: list of all objects in the scene
- **Scene View**: editor view where you build scenes
- **Game View**: what the player sees
- **Inspector**: properties of the selected object
- **Project Window**: assets (scripts, materials, scenes)

---

## 3. Cloning the Laboratory Repository

### 3.1 Install Git

Download Git from:
https://git-scm.com/downloads

---

### 3.2 Clone the Repository

```bash
git clone https://github.com/Mihnea-Lopataru/ECG-Laboratories.git
```

---

## 4. Opening the Laboratory Project in Unity

1. Open **Unity Hub**
2. Go to **Projects**
3. Click **Open**
4. Select the folder:
   ```
   ECG-Laboratories/Unity
   ```

---

## 5. Running Laboratory 1

- Open the scene located at:
  ```
  Assets/Scenes/Laboratory 1.unity
  ```

- Press **Play**
- Use **WASD** keys to move the ball

Observe physics-based movement, collisions, and camera behavior.

---

## 6. Experimentation

Students are encouraged to:
- Modify movement parameters
- Adjust camera offset
- Observe physics interactions
- Explore the scene freely

---

## 7. Conclusion

This laboratory ensures that all tools are installed and functional.
It provides a shared baseline for all future graphics laboratories.
