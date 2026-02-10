# Laboratory 3 — Hierarchical Transformations and Orbits in Unity

## Objectives

By the end of this laboratory, students will be able to:

- Understand and use **hierarchical transformations** in Unity
- Implement **orbital motion** using rotation around a pivot
- Distinguish between **self-rotation** and **revolution**
- Apply **local-space transformations** correctly
- Observe the effect of **parent–child relationships** on motion

This laboratory applies the concepts introduced in **Chapter 3 — Geometric Transformations and Coordinate Spaces**.

---

## 1. Scene Setup

### 1.1 Create a New Scene

Create a new scene named:

```
Laboratory_3
```

---

### 1.2 Objects in the Scene

This laboratory uses **simple geometric primitives only**.

#### Sun
- **3D Object → Sphere**
- Name: `Sun`
- Position: `(0, 0, 0)`
- Scale: `(2, 2, 2)`

#### Earth
- **3D Object → Sphere**
- Name: `Earth`
- Scale: `(0.7, 0.7, 0.7)`

#### Moon
- **3D Object → Sphere**
- Name: `Moon`
- Scale: `(0.3, 0.3, 0.3)`

No materials or textures are required.

---

## 2. Hierarchy Configuration

To correctly represent orbital motion, **pivot objects** must be used.

### 2.1 Earth Orbit Pivot

1. Create an empty GameObject
   - Name: `EarthOrbitPivot`
   - Position: `(0, 0, 0)`

2. Make `Earth` a **child** of `EarthOrbitPivot`

---

### 2.2 Moon Orbit Pivot

1. Create an empty GameObject
   - Name: `MoonOrbitPivot`
   - Position: `(0, 0, 0)`

2. Make `MoonOrbitPivot` a **child** of `Earth`
3. Make `Moon` a **child** of `MoonOrbitPivot`

The final hierarchy should look like this:

```
Sun
EarthOrbitPivot
└── Earth
    └── MoonOrbitPivot
        └── Moon
```

---

## 3. Orbit Radius (Translation)

### Goal

Place each celestial body at a fixed distance from its pivot using a local-space translation.

### Script: `OrbitRadius.cs`

Attach this script to:
- `Earth`
- `Moon`

```csharp
using UnityEngine;

public class OrbitRadius : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private Vector3 localDirection = Vector3.right;

    private void Start()
    {
        Vector3 dir = localDirection.sqrMagnitude > 0.0001f
            ? localDirection.normalized
            : Vector3.right;

        transform.localPosition = dir * radius;
    }
}
```

### Suggested Values

| Object | Radius |
|------|--------|
| Earth | 6 |
| Moon  | 2 |

---

## 4. Orbital Motion (Revolution)

### Goal

Create orbital motion by rotating the **pivot**, not the object itself.

### Script: `OrbitPivot.cs`

Attach this script to:
- `EarthOrbitPivot`
- `MoonOrbitPivot`

```csharp
using UnityEngine;

public class OrbitPivot : MonoBehaviour
{
    [SerializeField] private float angularSpeedDegPerSec = 15f;
    [SerializeField] private Vector3 localAxis = Vector3.up;

    private Vector3 axisN;

    private void Awake()
    {
        axisN = localAxis.sqrMagnitude > 0.0001f
            ? localAxis.normalized
            : Vector3.up;
    }

    private void Update()
    {
        transform.localRotation *=
            Quaternion.AngleAxis(
                angularSpeedDegPerSec * Time.deltaTime,
                axisN
            );
    }
}
```

### Suggested Speeds

| Pivot | Speed (deg/s) |
|-----|---------------|
| EarthOrbitPivot | 3 |
| MoonOrbitPivot  | 40 |

---

## 5. Self-Rotation (Spin)

### Goal

Rotate each object around its **own local axis**, independently of its orbit.

### Script: `SelfSpin.cs`

Attach this script to:
- `Sun`
- `Earth`
- `Moon`

```csharp
using UnityEngine;

public class SelfSpin : MonoBehaviour
{
    [SerializeField] private float angularSpeedDegPerSec = 30f;
    [SerializeField] private Vector3 localAxis = Vector3.up;

    private Vector3 axisN;

    private void Awake()
    {
        axisN = localAxis.sqrMagnitude > 0.0001f
            ? localAxis.normalized
            : Vector3.up;
    }

    private void Update()
    {
        transform.localRotation *=
            Quaternion.AngleAxis(
                angularSpeedDegPerSec * Time.deltaTime,
                axisN
            );
    }
}
```

### Suggested Spin Speeds

| Object | Speed (deg/s) |
|------|---------------|
| Sun   | 5 |
| Earth | 60 |
| Moon  | 40 |

---

## 6. Observations

After running the scene, observe the following:

- Earth revolves around the Sun
- Moon revolves around the Earth
- The Moon automatically follows the Earth due to hierarchy
- Each object rotates around its own axis independently
- Changing the pivot rotation affects all child objects

---

## 7. Questions

1. Why is orbital motion implemented by rotating the pivot and not the planet itself?
2. What happens if the Earth is removed from the hierarchy?
3. Why must self-rotation be applied in local space?
4. How does the order of transformations affect the final motion?

## 9. Conclusion

This laboratory demonstrates how hierarchical transformations and local-space rotations can be combined to create complex motion from simple components. These principles are fundamental to animation systems, camera rigs, and scene graphs used in modern computer graphics engines.
