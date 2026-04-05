# Laboratory 9 — Rasterization, Depth and Transparency in Unity

## 1. Objectives

The goal of this laboratory is to explore how the rendering pipeline determines which objects are visible on the screen and how transparency behaves differently from opaque rendering.

In the theoretical part of the chapter, students studied rasterization, depth testing, and transparency. In this laboratory, students will build simplified interactive demonstrations in Unity to observe these concepts visually.

By the end of this lab, students should be able to:

- Understand how the **depth buffer (Z-buffer)** determines visibility
- Observe how objects are correctly rendered based on their distance to the camera
- Understand why **transparent objects** behave differently
- Observe how rendering order affects transparent objects
- Identify common visual artifacts related to depth and transparency

------------------------------------------------------------------------

## 2. Scene Setup

Create a new Unity scene called: Lab9_Rasterization

### Required objects

- A **Ground Plane**
- A **Camera**
- A **Directional Light**
- A parent object called: `Rasterization_Lab`

Inside the lab scene, create at least the following two stations:

- `Station_Depth`
- `Station_Transparency`

### Recommended positioning

Place the stations side by side:

- Depth station on the left
- Transparency station on the right

Ensure the camera can move freely or is positioned so both stations are visible.

------------------------------------------------------------------------

## 3. Depth Testing Station

The purpose of this station is to demonstrate how the depth buffer determines which objects are visible.

### Mathematical idea

During rendering, each pixel stores a depth value:

z_stored = min(z_current, z_new)

Only the closest surface to the camera is displayed.

---

### Basic requirements

Your implementation should include:

- At least **three opaque objects** (e.g., cubes)
- Objects placed at **different depths**
- Objects should **overlap visually** from the camera’s point of view

---

### Suggested implementation

- Create three cubes placed along the Z axis
- Slightly offset them on the X axis so they overlap
- Use **different colors** for each cube
- Ensure all materials are **opaque**

Add simple interaction:

- Allow one object (e.g., the middle cube) to move forward and backward along the Z axis

---

### Output requirement

You should clearly observe:

- Objects closer to the camera **hide** objects behind them
- Moving an object forward makes it appear in front
- Moving it backward makes it disappear behind others

------------------------------------------------------------------------

## 4. Transparency and Sorting Station

The purpose of this station is to demonstrate how transparent objects are rendered differently.

### Conceptual idea

Transparent objects are not handled only by depth testing. Instead, rendering depends on **drawing order**.

Unlike opaque objects, transparency requires sorting from back to front.

---

### Basic requirements

Your implementation should include:

- At least **two transparent objects** (e.g., quads or planes)
- Objects should **intersect or overlap**
- Materials must use **transparent rendering**

---

### Suggested implementation

- Create two quads facing the camera
- Apply semi-transparent materials (alpha < 1)
- Place them so they intersect visually
- Use different colors (e.g., red and blue)

Add simple interaction:

- Rotate the objects or their parent
- Optionally swap rendering order

---

### Output requirement

You should observe:

- The rendering result changes depending on the view angle
- One object may incorrectly appear in front of another
- Transparency does not behave like opaque depth testing

------------------------------------------------------------------------

## 5. Motion and Interaction

To better observe the effects, add simple interaction.

### Requirement

At least one of the following should be implemented for each station:

- Objects can move (depth station)
- Objects can rotate (transparency station)
- The user can trigger changes through UI or input

---

### Recommendation

For the depth station:
- Move one cube forward/backward

For the transparency station:
- Rotate the objects to observe sorting issues

------------------------------------------------------------------------

## 6. Experiments

Perform the following experiments and observe the results:

1. Move the middle cube forward and backward and observe how visibility changes.
2. Position the cube so that it is exactly between the other two and observe the result.
3. Rotate the transparent objects and observe how their appearance changes.
4. Try to determine which object is rendered first in the transparency station.
5. Observe cases where the transparency result looks incorrect or inconsistent.
6. Compare the behavior of opaque objects versus transparent objects.

------------------------------------------------------------------------

## 7. Questions

1. What is the role of the depth buffer in rendering?
2. Why do opaque objects render correctly regardless of order?
3. Why do transparent objects require sorting?
4. What kind of visual artifacts appear when transparency is not handled correctly?
5. In your own words, explain the difference between depth testing and transparency rendering.
