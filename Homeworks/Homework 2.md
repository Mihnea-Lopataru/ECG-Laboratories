
# Homework 2 - Interactive Lighting and Projection Analysis Room

  

## General Information

  

- Duration: 3 weeks

- Final grade cap: 100 points

- Maximum earnable points: 110 points

  

This is the second major homework assignment. It integrates concepts from:

  

- Chapter 5 - Camera and projections

- Chapter 6 - 3D geometry and surface normals

- Chapter 7 - Lighting and shading

  

The focus of this assignment is mathematical correctness, correct use of coordinate spaces, and a clear visual demonstration of how lighting behaves on different objects.

  

---

  

# 1. Scenario

  

You must build an interactive **lighting analysis room** in Unity.

  

The scene must contain multiple objects illuminated by a movable light source. The user must be able to observe how the same scene changes depending on:

  

- object geometry,

- surface normals,

- light position,

- camera projection mode,

- diffuse and specular lighting terms,

- distance-based attenuation.

  

The purpose of the homework is **not** to build a photorealistic renderer. The goal is to implement a mathematically correct educational scene that clearly demonstrates the concepts studied in Chapters 5, 6, and 7.

  

---

  

# 2. Scene Requirements

  

Create a scene named:

  

```bash

Homework2_LightingRoom

```

  

The scene must contain the following mandatory elements:

  

- A floor plane or simple room

- One controllable camera

- One movable point light source

- At least **3 analysis objects**

- A simple on-screen UI or text panel that displays mathematical values in real time

  

## 2.1 Required Analysis Objects

  

The scene must contain **exactly these three minimum objects**:

  

1.  **Diffuse Sphere**

- Used to demonstrate Lambert diffuse lighting

2.  **Specular Sphere**

- Used to demonstrate diffuse + Phong specular lighting

3.  **Triangle Panel**

- A triangle defined by **3 explicit vertices**

- Used to demonstrate normal computation from geometry

  

You may add more decorative objects if you want, but these three analysis objects are mandatory.

  

## 2.2 Suggested Layout

  

A recommended layout is:

  

- Diffuse Sphere on the left

- Specular Sphere in the center

- Triangle Panel on the right

- Light source above and slightly in front of the objects

- Camera facing the three objects from a comfortable viewing angle

  

The scene must be organized clearly in the Hierarchy.

  

Example structure:

  

```text

Homework2_LightingRoom
├── Main Camera
├── Point Light
│ └── LightVisual
├── Environment
│ ├── Floor
│ └── Walls (optional)
├── DiffuseSphere
├── SpecularSphere
├── TrianglePanel
│ ├── VertexA
│ ├── VertexB
│ ├── VertexC
│ └── TriangleMesh / TriangleVisual
└── UI

```

  

---

  

# 3. Camera and Projection Requirements (Chapter 5)

  

This homework must explicitly use ideas from Chapter 5. Therefore, the camera must not be left as a passive default object.

  

## 3.1 Camera Setup

  

You must create a controllable camera that allows the user to inspect the three analysis objects.

  

Minimum requirements:

  

- The camera must be able to move or orbit around the scene

- The camera must always allow the user to inspect all three objects clearly

- The camera must not clip through the floor or objects excessively

  

You may choose one of the following approaches:

  

-  **Free movement camera** (WASD + mouse look)

-  **Orbit camera** around a central pivot

  

Choose one method and implement it correctly.

  

## 3.2 Projection Mode Toggle

  

The camera must support **both**:

  

- Perspective projection

- Orthographic projection

  

The user must be able to switch between them using a key or a UI button.

  

Minimum requirements:

  

- The active projection mode must be visible on screen

- When switching to orthographic mode, the change in appearance must be noticeable

- When switching back to perspective mode, the original perspective behavior must return correctly

  

## 3.3 What Students Must Understand

  

This part of the homework is meant to show that:

  

- the camera defines the observer,

- the image depends on the projection mode,

- perspective projection changes perceived depth and size,

- orthographic projection removes perspective shrinkage.

  

---

  

# 4. Geometry Requirements (Chapter 6)

  

This homework must also use concepts from Chapter 6, especially triangle geometry and surface normals.

  

## 4.1 Triangle Panel

  

You must create a triangle using **three explicit points**:

  

-  `VertexA`

-  `VertexB`

-  `VertexC`

  

The triangle may be built using:

  

- a procedurally generated mesh, or

- a simple triangle visualization built from code

  

The implementation must allow you to clearly identify the three triangle vertices.

  

## 4.2 Normal Computation

  

You must compute the triangle normal in code using the edge vectors:

  

```text

u = B - A

v = C - A

n = normalize(cross(u, v))

```

  

The normal must not be hardcoded.

  

It must be computed from the vertex positions.

  

## 4.3 Normal Visualization

  

The triangle normal must be visible in the scene using one of the following:

  

-  `Debug.DrawRay`

-  `Gizmos.DrawLine`

-  `LineRenderer`

- an arrow object

  

The normal must originate from either:

  

- the triangle centroid, or

- one chosen triangle point

  

The visualized normal must update correctly if the triangle vertices are moved.

  

## 4.4 Triangle Lighting Usage

  

The computed triangle normal must be used in the lighting calculation for the triangle panel.

  

This requirement is mandatory.

  

The triangle panel must not be lit using only Unity's default lighting without your own mathematical computation.

  

---

  

# 5. Lighting Requirements (Chapter 7)

  

The homework must demonstrate three distinct lighting ideas:

  

1. Diffuse reflection

2. Specular reflection

3. Distance attenuation

  

You do **not** need to implement six different shading systems like in the full laboratory. This homework must stay smaller and clearer.

  

## 5.1 Movable Point Light

  

The scene must contain **one movable point light source**.

  

The user must be able to move the light during runtime.

  

Suggested controls:

  

-  `I / K` - forward / backward

-  `J / L` - left / right

-  `U / O` - up / down

  

You may use different keys, but they must be documented clearly in the scene or in the README/report.

  

The light source must also have a visible marker, for example:

  

- a small emissive sphere,

- a light bulb icon,

- or another clear visual indicator.

  

## 5.2 Diffuse Sphere - Lambert Lighting

  

The **Diffuse Sphere** must implement Lambert diffuse lighting.

  

Use the formula:

  

$$
I_d = k_d \cdot  \max(0, \mathbf{N} \cdot  \mathbf{L})
$$

  

Minimum requirements:

  

- The sphere must become brighter when the surface faces the light

- The sphere must become darker when the light moves to the side

- The side opposite to the light must be visibly darker

- The diffuse effect must change continuously as the light moves

  

You may implement this using:

  

- C# scripts controlling material color, or

- a simple custom shader

  

For this homework, a **C# script approach is acceptable and recommended** if done correctly.

  

## 5.3 Specular Sphere - Diffuse + Phong Specular

  

The **Specular Sphere** must implement:

  

- diffuse lighting, and

- Phong specular reflection

  

Use the specular formula:

  

$$
I_s = k_s \cdot  \max(0, \mathbf{R} \cdot  \mathbf{V})^n
$$

  

Minimum requirements:

  

- A visible highlight must appear on the sphere

- The highlight must change when the light moves

- The highlight must also change when the camera position changes

- The highlight must become tighter or wider depending on the shininess exponent `n`

  

You may expose `n` in the Inspector or by using keyboard controls.

  

## 5.4 Attenuation

  

The point light must use distance attenuation.

  

Use the formula:

  

$$
f_{att}(d) = \frac{1}{a + b d + c d^2}
$$

  

where:

  

-  `d` is the distance between the light and the shaded point

-  `a`, `b`, `c` are attenuation coefficients

  

Minimum requirements:

  

- When the light gets closer, the illuminated object becomes brighter

- When the light moves farther away, the illuminated object becomes darker

- The attenuation effect must be clearly observable on at least **one object**

  

Recommended: apply attenuation to both the Diffuse Sphere and the Specular Sphere.

  

## 5.5 Triangle Panel Lighting

  

The **Triangle Panel** must also respond to the movable light source.

  

Its illumination must use the computed normal from Section 4.

  

Minimum requirements:

  

- If the triangle faces the light, it becomes brighter

- If the triangle faces away from the light, it becomes darker

- Flipping the vertex order must flip the normal direction and affect the lighting

  

You are not required to create a UI toggle for winding order, but doing so is a good extension.

  

---

  

# 6. Mathematical Display Requirements

  

This homework must not be purely visual. The user must also see the numerical values involved in the lighting computation.

  

A small on-screen text panel is sufficient.

  

## 6.1 Mandatory Values to Display

  

At runtime, the interface must display at least the following:

  

- Active camera projection mode (`Perspective` or `Orthographic`)

- Light position in world coordinates

- For the Diffuse Sphere:

-  `N · L`

- clamped diffuse term

- For the Specular Sphere:

-  `R · V`

- specular term

- shininess exponent `n`

- For attenuation:

- distance `d`

- attenuation factor

- For the Triangle Panel:

- computed normal vector

  

## 6.2 Presentation Rules

  

The mathematical display must be:

  

- readable during Play Mode,

- updated in real time,

- clearly labeled,

- placed so that it does not completely block the view.

  

The purpose of this requirement is to connect the visual result to the mathematical formula.

  

---

  

# 7. Interaction Requirements

  

Your homework must be interactive.

  

Minimum interaction requirements:

  

- Move the light source during runtime

- Switch between perspective and orthographic projection

- Move or orbit the camera

  

Optional interaction:

  

- Change shininess exponent using keys

- Toggle attenuation on/off

- Toggle normal visualization on/off

  

Optional interactions are not mandatory unless you explicitly decide to implement them for bonus points.

  

---

  

# 8. Implementation Guidance

  

This section explains **what you should do step by step**, without giving you the full solution.

  

## 8.1 Suggested Development Order

  

Implement the homework in the following order:

  

1. Create the scene and place the 3 required analysis objects

2. Set up the camera and projection toggle

3. Add the movable point light and visible marker

4. Implement diffuse lighting on the Diffuse Sphere

5. Implement specular lighting on the Specular Sphere

6. Add attenuation based on light distance

7. Create the Triangle Panel from 3 points

8. Compute the triangle normal using the cross product

9. Use the triangle normal in the triangle lighting computation

10. Add the mathematical UI display

11. Test all systems together

  

## 8.2 Important Mathematical Rules

  

During implementation, make sure that:

  

- all direction vectors are normalized before dot products are used,

- the triangle normal is computed from geometry and not hardcoded,

- the view vector points from the shaded point toward the camera,

- the reflection vector is computed correctly from the light vector and normal,

- attenuation uses the actual distance between light and object,

- the same coordinate space is used for all vectors involved in the same formula.

  

## 8.3 Minimum Acceptable Version

  

To receive a passing homework, the project must at least contain:

  

- all 3 required objects,

- a movable light,

- projection toggle,

- correct diffuse lighting,

- correct specular lighting,

- correct attenuation,

- correct triangle normal computation,

- visible real-time mathematical display.

  

If one of these elements is missing, the homework is incomplete.

  

---

  

# 9. Report Requirements (Optional)

  

You may submit an optional short PDF report (1--2 pages).

  

The report may include:

  

1. The formulas you used for diffuse, specular, and attenuation

2. How the triangle normal was computed

3. The difference between perspective and orthographic projection

4. A short explanation of why normalization is necessary

5. Screenshots of the final scene

  

This report is optional and can contribute only through the bonus category defined below.

  

---

  

# 10. Grading Table

  

The raw grading scale goes up to **110 points**, but the final recorded grade is capped at **100**.

  

This means students may compensate for weaker parts of the homework by performing very well in other parts.

  

| Category | Requirement | Points |
|----------|-------------|--------|
| Scene Setup | Correct scene structure and required objects | 5 |
| Camera Control | Camera can inspect the scene properly | 10 |
| Projection Toggle | Working perspective / orthographic switch | 10 |
| Light Movement | Movable light source implemented correctly | 10 |
| Diffuse Lighting | Correct Lambert implementation on Diffuse Sphere | 15 |
| Specular Lighting | Correct Phong implementation on Specular Sphere | 15 |
| Attenuation | Correct distance-based falloff | 10 |
| Triangle Geometry | Triangle created from 3 explicit vertices | 5 |
| Triangle Normal | Correct cross-product normal computation | 10 |
| Triangle Lighting | Computed normal used correctly in lighting | 5 |
| Mathematical Display | Required values shown clearly in real time | 10 |
| Code Quality | Code organization, naming, comments | 5 |
| **Raw Total** | | **110** |
| **Final Grade Cap** | | **100** |

  

## 10.1 Interpretation of the Final Grade

  

Examples:

  

- Raw score = 84 → Final score = 84

- Raw score = 97 → Final score = 97

- Raw score = 103 → Final score = 100

- Raw score = 110 → Final score = 100

  

# 11. Submission Rules

  

- Deadline: **3 weeks from the assignment date**

- Submit the Unity project archive or repository as instructed

- Remove `Library`, `Temp`, and `Builds` folders before submission

- The project must open without compile errors

- The main scene must be named exactly:

  

```bash

Homework2_LightingRoom

```

  

- All required scripts must be included

- The project must run in Play Mode without missing references

- The controls used in the project must be documented in a short text file or in the README

  

---

  

# 12. Final Checklist

  

Before submitting, make sure that:

  

- [ ] The scene name is correct

- [ ] The camera can inspect the entire setup

- [ ] Perspective and orthographic projection both work

- [ ] The light source can be moved during runtime

- [ ] The Diffuse Sphere shows Lambert shading

- [ ] The Specular Sphere shows a visible Phong highlight

- [ ] Attenuation is visible when distance changes

- [ ] The Triangle Panel is built from 3 points

- [ ] The triangle normal is computed using the cross product

- [ ] The triangle normal is visualized

- [ ] The triangle lighting uses the computed normal

- [ ] The mathematical values are displayed in real time

- [ ] The project opens without errors
