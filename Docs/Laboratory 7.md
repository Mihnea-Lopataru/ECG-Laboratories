
# Laboratory 7 — Lighting and Shading in Unity

  

## 1. Objectives

  

The goal of this laboratory is to explore how classical lighting models are used in computer graphics.

In the theoretical part of the chapter, lighting was studied through vectors such as the surface normal, the light direction, and the view direction. In this laboratory, students will implement simplified interactive demonstrations in Unity in order to observe these concepts directly.

  

By the end of this lab, students should be able to:

  

- Create a simple scene for studying **lighting in Unity**

- Implement a **diffuse lighting** demonstration based on the dot product

- Implement a **specular lighting** demonstration based on the reflection direction and the view direction

- Observe how **light position** and **object orientation** affect shading

- Compare at least **two different shading approaches** on multiple objects

- Display and interpret key quantities such as **N · L** and the specular response

  

------------------------------------------------------------------------

  

## 2. Scene Setup

  

Create a new Unity scene called: Lab7_Lighting

  

### Required objects

  

- A **Ground Plane**

- A **Camera**

- A **Directional Light** for general scene visibility

- A parent object called: `Lighting_Lab`

  

Inside the lab scene, create at least the following three stations:

  

-  `Station_Diffuse`

-  `Station_Specular`

-  `Station_ShadingComparison`

  

### Recommended station content

  

For the diffuse station:

- One visible object, such as a **sphere**

- One movable **point light**

  

For the specular station:

- One visible object, such as a **sphere**

- One movable **point light**

- A camera position from which highlights can be observed clearly

  

For the shading comparison station:

- At least **two spheres** placed side by side

- The same light source should influence all comparison objects

  

You may also add small emissive spheres or simple markers to indicate the position of the light sources.

  

### Recommended positioning

  

Place the stations in a straight line so they are easy to inspect one by one.

For example:

  

- Diffuse station on the left

- Specular station in the middle

- Shading comparison station on the right

  

Ensure that the camera can move through the scene or can be positioned such that all stations are visible.

  

------------------------------------------------------------------------

  

## 3. Diffuse Lighting Station

  

Create a script called: DiffuseStationController.cs

  

Attach this script to an empty object called: `Station_Diffuse`

  

The purpose of this station is to demonstrate the Lambert diffuse model.

  

### Mathematical idea

  

Diffuse illumination depends on the angle between the surface normal and the light direction:

  

$$I_d = k_d \cdot \max(0, \mathbf{N} \cdot \mathbf{L})$$

  

### Basic requirements

  

Your implementation should:

  

- Use one visible object, preferably a sphere

- Use one point light that moves or can be repositioned

- Compute a direction from the object toward the light

- Compute the dot product between a chosen normal direction and the light direction

- Use the result to control the visible brightness of the object

  

### Suggested simplified implementation

  

For this laboratory, you are not required to write a custom shader.

Instead, you may implement a simplified version in C# by:

  

- selecting a representative normal direction for the object,

- computing `Vector3.Dot(normal, lightDirection)`,

- clamping the result to the interval `[0, 1]`,

- using that value to modify the material color of the object.

  

### Hint (Unity API)

  

You will likely use:

  

-  `Transform.position`

-  `Transform.forward`

-  `Vector3.Dot()`

-  `Renderer.material.color`

-  `Mathf.Max()`

-  `Color.Lerp()`

  

### Output requirement

  

You should be able to observe that:

  

- when the object faces the light, it appears brighter,

- when it turns away from the light, it becomes darker,

- when the light moves behind the object, the diffuse contribution becomes zero.

  

------------------------------------------------------------------------

  

## 4. Visualizing the Light Source

  

In order to make the stations easier to understand, add a visible marker for each point light.

  

### Requirement

  

Create a small object, for example a sphere, as a child of the light source.

This object should:

  

- remain attached to the light,

- move together with the light,

- make the current light position easy to identify.

  

### Suggested implementation

  

- Create a child object called `LightVisual`

- Scale it down so it remains small relative to the station objects

- Assign a bright material or emission color to it

  

This is a simple but useful improvement because it makes the source of illumination immediately visible in the scene.

  

------------------------------------------------------------------------

  

## 5. Specular Lighting Station

  

Create a script called: SpecularStationController.cs

  

Attach this script to an empty object called: `Station_Specular`

  

The purpose of this station is to demonstrate specular reflection.

  

### Mathematical idea

  

Specular reflection depends on the relation between the reflected light direction and the view direction.

A simplified Phong-style term may be written as:

  

$$
I_s = k_s \cdot \max(0, \mathbf{R} \cdot \mathbf{V})^n
$$

  

where:

-  `R` is the reflection direction,

-  `V` is the view direction,

-  `n` is the shininess exponent.

  

### Basic requirements

  

Your implementation should:

  

- Use one sphere or another smooth object

- Use one point light

- Use the camera position as the viewer position

- Compute a reflection direction based on the chosen surface normal and the light direction

- Compute the specular response using a shininess parameter

- Use the result to produce a visible highlight effect

  

### Suggested step-by-step approach

  

1. Compute the light direction from the object toward the light.

2. Compute the view direction from the object toward the camera.

3. Compute the reflection direction using Unity utilities or vector formulas.

4. Compute the dot product between the reflection direction and the view direction.

5. Clamp the result to the interval `[0, 1]`.

6. Raise the result to a power in order to control highlight sharpness.

7. Use the final value to brighten the object or its highlight color.

  

### Hint (Unity API)

  

You will likely use:

  

-  `Camera.main.transform.position`

-  `Vector3.Reflect()`

-  `Vector3.Dot()`

-  `Mathf.Pow()`

- material color adjustment in C#

  

### Output requirement

  

You should be able to observe that:

  

- the highlight changes when the light moves,

- the highlight also changes when the camera moves,

- increasing the shininess exponent produces a smaller and sharper highlight.

  

------------------------------------------------------------------------

  

## 6. Shading Comparison Station

  

Create a script called: ShadingComparisonController.cs

  

Attach this script to an empty object called: `Station_ShadingComparison`

  

The goal of this station is to compare how different shading strategies affect the appearance of similar objects.

  

### Minimum requirement

  

Implement at least **two** of the following:

  

- Flat-like shading

- Gouraud-like shading

- Phong-like shading

  

A stronger implementation may include all three.

  

### Suggested student version

  

A good student-friendly version is:

  

- One sphere using a simpler, more faceted appearance

- One sphere using smoother diffuse/specular color transitions

- One sphere using a more pronounced specular response

  

Since this laboratory uses C# rather than custom shaders, the comparison may be implemented as an approximation rather than as a fully shader-based pipeline.

  

### Step-by-step recommendation

  

#### Option A: Flat-like vs Smooth comparison

  

- Use two spheres or low-poly objects

- On one object, keep the effect visually simple and uniform

- On the second object, update brightness more smoothly and use a more continuous visual response

  

#### Option B: Diffuse-only vs Diffuse+Specular comparison

  

- First object: apply only the diffuse term

- Second object: apply diffuse plus specular

- Third object (optional): use a stronger shininess value to emphasize the highlight

  

### Observation goal

  

Students should clearly identify that:

  

- one object appears more faceted,

- another appears smoother,

- specular highlights make the surface appear shinier and more realistic.

  

### Important note

  

For this laboratory, the comparison does not need to be physically perfect.

The main objective is to make the difference between basic shading strategies visible and understandable.

  

------------------------------------------------------------------------

  

## 7. Motion and Interaction

  

To make the stations easier to observe, add simple motion to the light sources.

  

### Requirement

  

At least one of the following should be implemented:

  

- the light moves automatically around the object,

- the light moves left to right,

- the object rotates slowly,

- the user can move the camera and inspect the effect interactively.

  

### Recommendation

  

For the diffuse and specular stations, automatic light movement is recommended because it makes the changes in intensity easy to observe.

  

For the shading comparison station, a shared light moving left to right is recommended because all objects can then be compared under the same illumination conditions.

  

------------------------------------------------------------------------

  

## 8. Displaying Numerical Values

  

For at least one of the stations, display the numerical values involved in the lighting computation.

  

### Suggested values

  

For the diffuse station:

-  `N · L`

- diffuse intensity

  

For the specular station:

-  `R · V`

- shininess

- specular intensity

  

### Output requirement

  

You may display the values using:

  

-  `Debug.Log()` in the Console

- simple UI text

- TextMeshPro labels

- any other clear visualization method

  

The purpose of this requirement is to connect the visual result with the mathematical formula.

  

------------------------------------------------------------------------

  

## 9. Experiments

  

Perform the following experiments and observe the results.

  

1. Move the light around the diffuse station and observe how the object brightness changes.

2. Rotate the diffuse object and observe how the value of **N · L** changes.

3. Move the camera around the specular station and observe how the highlight changes.

4. Increase the shininess exponent and compare the size of the specular highlight.

5. Compare the appearance of the objects in the shading comparison station and describe the main visual differences.

6. Place the light behind the diffuse object and confirm that the diffuse contribution becomes zero or almost zero.

  

------------------------------------------------------------------------

  

## 10. Questions

  

1. Why does diffuse lighting depend on the dot product between the normal and the light direction?

2. Why does specular lighting depend on the viewer position, while diffuse lighting does not?

3. What is the effect of increasing the shininess exponent in a Phong-style specular model?

4. Why is it useful to compare multiple shading approaches using the same light source?

5. In your own words, what is the visual difference between a diffuse-only object and an object that also includes a specular term?
