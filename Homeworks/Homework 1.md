
# Homework 1 - Drone Docking in a Rotating Space Station

## General Information

- Duration: 3 weeks
- Total Points: 100
- Base Points (granted automatically): 10
- Maximum Earnable Points: 90

This is the first major homework assignment. It integrates concepts from:

- Laboratory 1 - Unity setup and scene organization
- Laboratory 2 - Vector movement and dot product detection
- Laboratory 3 - Hierarchical transformations
- Laboratory 4 - Euler and Quaternion rotations

The focus of this assignment is mathematical correctness and proper implementation of rotation systems.

---

# 1. Scenario

You must implement a controllable drone that navigates and docks at multiple docking ports mounted on a rotating space station.

The space station must use hierarchical transforms (parent--child pivots). Each docking port must evaluate alignment using the dot product between its forward direction and the direction toward the drone.

The drone must support two rotation control modes:
- Euler-based accumulation
- Quaternion-based incremental composition

The user must be able to toggle between the two modes using a single UI button.

---

# 2. Scene Requirements

Create a scene named:

```bash
Homework1_Docking
```

The scene must contain:
- A ground plane (or space platform)
- A drone object (cube or simple model)
- A rotating space station built using hierarchy
- At least 5 obstacles (with colliders)

---

# 3. Drone Requirements

  

## 3.1 Rigidbody Setup

The drone must:
- Have a Rigidbody
- Have a Collider
- Not pass through objects
- Use continuous collision detection

Recommended Rigidbody settings:
- Use Gravity: OFF
- Is Kinematic: OFF
- Interpolation: Interpolate
- Freeze Rotation X, Y, Z

Rotation must be controlled manually through code.

---

## 3.2 Movement (Vector-Based)

Implement movement using vector mathematics.

Controls:
- W / S - Forward / Backward
- A / D - Left / Right
- Left Shift - Up
- Left Ctrl - Down

Movement must be relative to the drone's local coordinate system:

- Forward → transform.forward

- Right → transform.right

- Up → Vector3.up

Diagonal movement must be normalized to avoid increased speed.

---

# 4. Space Station (Hierarchical Transforms)

The station must be constructed using parent-child relationships.

Example structure:
```text
StationRoot
├── RingPivot
│ └── RingMesh
│ ├── Port_1
│ ├── Port_2
│ └── Port_3
└── AntennaPivot
└── AntennaMesh
```

Requirements:
- RingPivot must rotate continuously.
- AntennaPivot must rotate independently (different speed and/or axis).
- Docking ports must move because of parent rotation (not independent animation).

---

# 5. Docking Port Alignment System (Dot Product)

Each docking port must detect alignment using vector mathematics.

## 5.1 Alignment Computation

Let:
- p_drone = drone position
- p_port = port position

Compute:

d = (p_drone − p_port) / ||p_drone − p_port||

Let f_port be the port's forward direction.

Compute dot product:

s = f_port · d


Use a threshold derived from field-of-view angle:

s >= cos(FOV / 2)


Docking is valid only if:
- Distance to port ≤ dockRadius
- Alignment condition is satisfied

---

## 5.2 Visual Feedback

Each port must display one of three states:
- Red → Too far
- Yellow → Near but misaligned
- Green → Properly aligned and within radius

Simple material color changes are sufficient.

---

# 6. Rotation System (Two Modes)

## 6.1 Euler Mode

Maintain a Vector3 eulerState.
Each frame:
- Update pitch, yaw, roll
- Convert using Quaternion.Euler(eulerState)
- Apply rotation

Students should observe potential gimbal lock behavior near ±90° pitch.

---

## 6.2 Quaternion Mode

Rotation must be composed incrementally using:
- Quaternion.AngleAxis(angle, axis)
- Apply rotation using quaternion multiplication.
- Rotation must occur around local axes.

---

## 6.3 Mode Toggle

A single UI button must:
- Toggle between Euler and Quaternion modes
- Update its label text accordingly

---

# 7. Real-Time Mathematical Display

The interface must display:
- Drone position (world coordinates)
- Euler angles (degrees)
- Quaternion components (x, y, z, w)
- Axis--Angle representation

Axis--Angle must be computed using: 
```c#
transform.rotation.ToAngleAxis(out angle, out axis);
```

---

# 8. Docking Task Logic

Minimum requirements:
- At least 3 docking ports
- Drone must dock at all ports
- Docking requires proper alignment and proximity
- Display a counter: Docked k/3

Docking must require either:
- Holding alignment for at least 1 second OR
- Pressing a specific key while aligned

Choose one method and document it in your report.

---

# 9. Report Requirements (PDF Submission / Word)

Students must submit a short PDF (1--2 pages) explaining:
1. The mathematical formula used for dot product alignment.
2. How the threshold was computed from FOV.
3. The difference between Euler and Quaternion rotation in your implementation.
4. What gimbal lock is and how it was observed.
5. Why quaternion multiplication represents composition of rotations.

---

# 10. Grading Table

| Category | Requirement | Points |
|----------|-------------|--------|
| Scene Setup | Proper scene structure and functionality | 5 |
| Drone Movement | Local-space vector movement with normalization | 10 |
| Collision Handling | No clipping through ground or obstacles | 5 |
| Hierarchical Station | Correct parent-child rotation implementation | 10 |
| Port Hierarchy | Ports move because of parent transforms | 5 |
| Dot Product Alignment | Correct mathematical implementation | 15 |
| Port Feedback States | Correct red/yellow/green logic | 5 |
| Two Rotation Modes | Proper Euler and Quaternion implementations | 15 |
| UI Toggle | Single button correctly toggles modes | 5 |
| Real-Time Math Display | Position, Euler, Quaternion, Axis–Angle | 10 |
| Docking Gameplay Logic | All ports dockable and tracked | 5 |
| PDF Report | Clear mathematical explanations | 5 |
| **Total Earnable** |  | **90** |
| Base Points | Automatically granted | **10** |
| **Grand Total** |  | **100** |


------------------------------------------------------------------------

  

# 11. Submission Rules

- Remove Library, Temp, Builds folders before submission.
- Project must open without errors.
- Code must be clearly structured and commented.
- External assets are optional but not required.
