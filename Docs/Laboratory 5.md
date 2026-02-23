# Laboratory 5 — The MVP Pipeline in Unity (Model, View, Projection)

## 1. Objectives

The purpose of this laboratory is to connect the mathematical theory from Chapter 5 to a practical Unity implementation of the graphics pipeline.

By the end of this lab, students should be able to:

- Identify and extract the **Model (M)**, **View (V)**, and **Projection (P)** matrices in Unity
- Compose the **MVP matrix** as \(PVM\)
- Transform a point from **object space → clip space**
- Perform the **perspective divide** to obtain **NDC**
- Determine whether a point is **inside** or **outside** the canonical cube \([-1,1]^3\)
- Compare **Perspective** vs **Orthographic** projection behavior (especially the \(w\) coordinate)

------------------------------------------------------------------------

## 2. Scene Setup

Create a new Unity scene called:

    Lab5_MVP

### Required objects

- A ground plane (Plane)
- A tracked object (Cube is sufficient, but you may use any mesh)
- A camera (use Main Camera)
- A directional light (default is fine)

### Recommended positioning

- Place the tracked object in front of the camera so it is clearly visible.
- Set a reasonable camera clipping range:
  - Near: 0.3
  - Far: 100–300 (depending on scene scale)

------------------------------------------------------------------------

## 3. Camera Controller (Movement + Mouse Look)

Implement a simple fly/FPS camera controller so you can observe how the **View** matrix changes as the camera moves.

### Controls (recommended)

- **Hold Right Mouse Button**: enable mouse look
- **Mouse**: rotate camera (yaw/pitch)
- **W/A/S/D**: move relative to camera orientation (\(W\) must always move forward in the direction you look)
- **Q/E** (or Ctrl/Space): down/up
- **Left Shift**: fast movement

> Important: Movement must be applied using the camera’s local axes (`transform.forward`, `transform.right`, `transform.up`).

------------------------------------------------------------------------

## 4. Extracting M, V, P Matrices

Create a script called:

    MVPStudent.cs

Attach it to an empty object called:

    MVP_System

In `Update()`, extract:

- **Model matrix** \(M\): from the tracked object transform
- **View matrix** \(V\): from the camera
- **Projection matrix** \(P\): from the camera

### Hints (Unity API)

- Model: `trackedObject.localToWorldMatrix`
- View: `camera.worldToCameraMatrix`
- Projection: `camera.projectionMatrix`

You must compute:
$$MVP=PVM$$

------------------------------------------------------------------------

## 5. Transform a Point Through the Pipeline

Choose a test point in **object space**.

### Required test point

Use the object pivot (local origin):

$$p_{obj} = (0,0,0,1)^T$$

### Steps

1. Compute clip-space coordinates:

$$p_{clip} = MVP \cdot p_{obj}$$

2. Compute NDC coordinates (perspective divide):

$$p_{ndc} =\left(\frac{x_c}{w_c},\frac{y_c}{w_c},\frac{z_c}{w_c}\right)$$

> If \(w_c\) is very close to 0, you must not divide (avoid division by zero).

### Output requirement

At minimum, print the following to the Console (or any simple on-screen text):

- `p_clip`
- `w`
- `p_ndc`

------------------------------------------------------------------------

## 6. Clipping Test in NDC

A point is inside the canonical cube if:

$$-1 \le x_{ndc} \le 1,\quad-1 \le y_{ndc} \le 1,\quad-1 \le z_{ndc} \le 1$$

Implement a boolean check:

- `inside = true` if all three coordinates are within \([-1,1]\)
- otherwise `inside = false`

### Experiment requirement

Move the camera (or the object) until the point becomes outside the frustum.

Observe how `p_ndc` changes and when `inside` flips to false.

------------------------------------------------------------------------

## 7. Perspective vs Orthographic Comparison

Add a simple way to toggle projection mode:

- Press a key (e.g., `P`) to switch between:
  - Perspective projection
  - Orthographic projection

### What to observe

When you switch modes, compare:

- The clip-space values
- The \(w\) coordinate
- The NDC values
- Whether the point is considered inside/outside

> Key observation: In orthographic projection, \(w\) typically remains constant (often 1), while in perspective projection, \(w\) depends on depth.

------------------------------------------------------------------------

## 8. Experiments

Perform the following experiments and record your observations:

1. **Move forward/backward** with the camera (W/S) and monitor how \(w\) and \(z_{ndc}\) change.
2. Rotate the camera and confirm that **W always moves forward** in the direction the camera faces.
3. Toggle Perspective/Orthographic and compare the changes in \(w\).
4. Force clipping:
   - Move the camera so the tracked object goes out of view
   - Confirm that the NDC test indicates `inside = false`.

------------------------------------------------------------------------

## 9. Questions

1. What does the \(w\) coordinate represent in perspective projection?
2. Why does perspective projection require division by \(w\) but orthographic does not?
3. What does it mean for a point to be outside the NDC cube?
4. In your experiment, which coordinate typically causes clipping first (x, y, or z)? Why?
