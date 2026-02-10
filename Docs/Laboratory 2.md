# Laboratory 2 — Vectors and Dot Product in Unity

## Objectives

By the end of this laboratory, students will be able to:

- Use vectors to control movement in a 3D scene
- Distinguish between **points** and **direction vectors**
- Apply **vector normalization** to ensure consistent movement speed
- Understand and use the **dot product** to test directional alignment
- Visualize vector-based detection using a simple traffic light indicator

This laboratory applies the concepts introduced in **Chapter 2 — Coordinate Systems and Vectors**.

---

## 1. Input System Configuration (Important)

Unity supports two input systems:
- The classic **Input Manager** (`Input.GetKey`)
- The **New Input System** (Input Actions)

For clarity and to focus on vector mathematics, this laboratory uses **direct keyboard input**.

### Required Setting

1. Go to **Edit → Project Settings → Player**
2. Open **Other Settings**
3. Set **Active Input Handling** to:

```
Both
```

4. Restart Unity if prompted

This allows the project to use both input systems.

---

## 2. Scene Setup

### 2.1 Create a New Scene

- Create a new scene named:
  ```
  Laboratory_2
  ```

### 2.2 Environment

- Add a **Plane**
  - Name it `Ground`
  - Scale it to create enough space for movement

### 2.3 Drone Object

- Create a **3D Object → Capsule** (or Cube)
- Name it `Drone`
- Position it slightly above the ground (e.g. Y = 0.5)

#### Physics Setup

Add the following components to `Drone`:

- **BoxCollider** (or CapsuleCollider)
- **Rigidbody**
  - Use Gravity: OFF
  - Is Kinematic: ON
  - Interpolate: Interpolate
  - Collision Detection: Continuous
  - Constraints: Freeze Rotation (X, Y, Z)

This allows the drone to collide with obstacles while remaining stable.

### 2.4 Obstacles

- Add a few **Cube** objects to act as walls
- Ensure each wall has a **Collider**
- Do NOT enable *Is Trigger*

---

## 3. Drone Movement Using Vectors

### Controls

- **W / S** — forward / backward
- **A / D** — left / right
- **Left Shift** — up
- **Left Ctrl** — down

### Key Idea

Movement is computed using:
```
displacement = direction × speed × time
```

### Script: DroneUserFlightRB.cs

Attach the following script to the `Drone` object:

```csharp
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneUserFlightRB : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        moveInput = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveInput += transform.forward;
        if (Input.GetKey(KeyCode.S)) moveInput -= transform.forward;
        if (Input.GetKey(KeyCode.D)) moveInput += transform.right;
        if (Input.GetKey(KeyCode.A)) moveInput -= transform.right;

        if (Input.GetKey(KeyCode.LeftShift)) moveInput += transform.up;
        if (Input.GetKey(KeyCode.LeftControl)) moveInput -= transform.up;

        if (moveInput.sqrMagnitude > 0.0001f)
            moveInput = moveInput.normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }
}
```

### Observe

- Movement is relative to the drone’s local coordinate system
- Diagonal movement is not faster (normalization)
- The drone collides with walls

---

## 4. Dot Product Detection — Traffic Light Sensor

### Goal

Use the **dot product** to determine whether the drone is:
- Far away
- Near
- Directly in front of a sensor

### 4.1 Traffic Light Object

Create an empty GameObject named `TrafficLight`.

As children, create:
- `RedLight`
- `YellowLight`
- `GreenLight`

These can be lights or colored meshes.
Only one should be active at a time.

---

### 4.2 Detection Logic

The detection uses:
- Displacement vector:
  ```
  toDrone = dronePosition − sensorPosition
  ```
- Normalized direction vector
- Dot product:
  ```
  dot = forward · directionToDrone
  ```

Interpretation:
- dot ≈ 1 → in front
- dot ≈ 0 → side
- dot < 0 → behind

---

### Script: DroneTrafficLightDetector.cs

Attach this script to `TrafficLight`:

```csharp
using UnityEngine;

public class DroneTrafficLightDetector : MonoBehaviour
{
    [SerializeField] private Transform drone;
    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject yellowLight;
    [SerializeField] private GameObject greenLight;

    [SerializeField] private float farDistance = 12f;
    [SerializeField] private float nearDistance = 6f;
    [SerializeField] private float fovDegrees = 60f;

    private void Update()
    {
        Vector3 toDrone = drone.position - transform.position;
        float distance = toDrone.magnitude;

        if (distance > farDistance)
        {
            SetRed();
            return;
        }

        Vector3 dir = toDrone.normalized;
        float dot = Vector3.Dot(transform.forward, dir);
        float threshold = Mathf.Cos(0.5f * fovDegrees * Mathf.Deg2Rad);

        if (distance <= nearDistance && dot >= threshold)
            SetGreen();
        else
            SetYellow();
    }

    private void SetRed()
    {
        redLight.SetActive(true);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);
    }

    private void SetYellow()
    {
        redLight.SetActive(false);
        yellowLight.SetActive(true);
        greenLight.SetActive(false);
    }

    private void SetGreen()
    {
        redLight.SetActive(false);
        yellowLight.SetActive(false);
        greenLight.SetActive(true);
    }
}
```

---

## 5. Questions

1. Why must direction vectors be normalized before using the dot product?
2. What does a negative dot product indicate?
3. Why is normalization required for diagonal movement?
4. How does changing the field-of-view angle affect detection?

---

## 6. Conclusion

This laboratory demonstrates how fundamental vector operations—addition, normalization, and dot product—are used in practical computer graphics scenarios. These concepts form the basis for camera control, lighting, and visibility testing in later chapters.
