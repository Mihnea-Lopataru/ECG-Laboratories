# Laboratory 4 — Rotations in Unity (Euler vs Quaternion)

## 1. Objectives

The purpose of this laboratory is to explore rotation systems in Unity
and understand the mathematical differences between:

-   Euler angle rotations
-   Quaternion-based rotations
-   Axis-Angle representation

By the end of this lab, students should:

-   Implement a controllable drone object
-   Apply rotations using Euler accumulation
-   Apply rotations using Quaternion composition
-   Observe real-time changes in Euler, Quaternion and Axis-Angle
    representations
-   Understand practically what gimbal lock means

------------------------------------------------------------------------

## 2. Scene Setup

Create a new Unity scene called:

    Lab4_Rotations

### Required objects:

-   Plane (Ground)
-   Drone object (Cube is sufficient)
-   Rigidbody component on Drone
-   BoxCollider on Drone
-   Collider on Ground

### Rigidbody settings:

-   Use Gravity: OFF
-   Is Kinematic: OFF
-   Interpolate: Interpolate
-   Collision Detection: Continuous
-   Freeze Rotation X, Y, Z

This ensures collisions are handled by physics while rotation is
controlled manually.

------------------------------------------------------------------------

## 3. Movement Implementation

Students must implement movement using Rigidbody velocity.

### Controls

-   W / S → Forward / Backward
-   A / D → Left / Right
-   Left Shift → Up
-   Left Ctrl → Down

### Core Idea

Movement must be applied relative to the drone's local axes:

-   Forward movement → transform.forward
-   Right movement → transform.right
-   Vertical movement → Vector3.up

Velocity must be set in FixedUpdate().

------------------------------------------------------------------------

## 4. Rotation System - Two Modes

Students must implement two rotation systems and allow switching between
them.

### Mode 1 - Euler Accumulation

Maintain a Vector3 eulerState.

Each frame:

-   Update pitch, yaw, roll
-   Convert using Quaternion.Euler(eulerState)
-   Apply rotation using MoveRotation()

Controls:

-   Q / E → Yaw
-   Up / Down → Pitch
-   Z / C → Roll

### Mode 2 - Quaternion Composition

Instead of storing Euler angles:

-   Compute incremental rotations with Quaternion.AngleAxis
-   Compose rotations using quaternion multiplication
-   Apply using MoveRotation()

Important:

Rotation must be performed around local axes.

------------------------------------------------------------------------

## 5. Real-Time Data Visualization (Optional)

Display in the UI:

-   Position (world space)
-   Euler angles (degrees)
-   Quaternion components (x, y, z, w)
-   Axis--Angle representation

Axis--Angle must be computed using:

    transform.rotation.ToAngleAxis(out angle, out axis);

------------------------------------------------------------------------

## 6. Observations

Students should observe:

1.  Euler and Quaternion represent the same orientation differently.
2.  Euler values may appear unstable near ±90° pitch.
3.  Quaternion values remain smooth and normalized.
4.  Axis--Angle shows invariant rotation axis during pure yaw.

------------------------------------------------------------------------

## 7. Experiments

Perform the following tests:

1.  Rotate pitch close to ±90° in Euler mode.
2.  Attempt yaw and roll - observe coupling behavior.
3.  Switch to Quaternion mode and repeat.
4.  Compare behavior carefully.

------------------------------------------------------------------------

## 8. Questions

1.  What is gimbal lock?
2.  Why does Quaternion mode not suffer from gimbal lock?
3.  Why are quaternions better for interpolation?
4.  What is the geometric meaning of Axis-Angle representation?