
# Laboratory 8 — Texture Mapping and Sampling in Unity

## 1. Objectives

The goal of this laboratory is to explore how textures are sampled and used in real-time rendering.

In the theoretical part of the chapter, students studied texture mapping, filtering, mipmaps, and normal mapping. In this laboratory, students will build simplified interactive demonstrations in Unity to observe these concepts visually.

By the end of this lab, students should be able to:

- Understand how **texture sampling** works in practice
- Compare different **texture filtering modes**
- Observe **aliasing artifacts** and understand their cause
- Understand the role of **mipmaps** in reducing aliasing
- Understand how **normal maps** affect lighting without modifying geometry

------------------------------------------------------------------------

## 2. Scene Setup

Create a new Unity scene called: Lab8_Textures

### Required objects

- A **Ground Plane**
- A **Camera**
- A **Directional Light**
- A parent object called: `Texture_Lab`

Inside the lab scene, create at least the following two stations:

- `Station_Sampling`
- `Station_NormalMapping`

### Recommended positioning

Place the stations side by side so they can be explored easily:

- Sampling station on the left
- Normal mapping station on the right

Ensure the camera can move freely through the scene.

------------------------------------------------------------------------

## 3. Sampling and Filtering Station

The purpose of this station is to demonstrate how textures are sampled and how filtering affects the final image.

### Mathematical idea

Texture mapping is defined as a function:

$$
C(x, y) = T(u, v)
$$

where:

- $(x, y)$ are screen coordinates,
- $(u, v)$ are texture coordinates,
- $T(u, v)$ is the sampled texture color.

### Basic requirements

Your implementation should include:

- At least **three objects** (e.g., planes or quads)
- Each object uses the **same texture**
- Each object uses a different **filtering mode**:
  - Point (Nearest)
  - Bilinear
  - Trilinear

### Suggested implementation

- Create three planes placed side by side
- Assign a high-frequency texture (e.g., checkerboard or grid)
- Set the filtering mode in the texture import settings:
  - Point → no interpolation
  - Bilinear → smooth interpolation
  - Trilinear → interpolation + mipmaps

### Output requirement

You should clearly observe:

- Point filtering produces sharp but blocky results
- Bilinear filtering produces smoother transitions
- Trilinear filtering produces the smoothest result, especially at distance

------------------------------------------------------------------------

## 4. Aliasing and Mipmaps

In addition to filtering, this station should demonstrate **aliasing** and the role of **mipmaps**.

### Mathematical idea

Aliasing occurs when many texels map to a single pixel:

$$
\text{many texels} \rightarrow \text{one pixel}
$$

This produces unstable and incorrect results.

Mipmaps solve this by using precomputed lower-resolution textures.

### Basic requirements

- Use one plane with a **high-frequency texture**
- Tilt the plane so that part of it is far from the camera
- Create two versions of the texture:
  - with mipmaps
  - without mipmaps

### Suggested implementation

- Duplicate the texture asset:
  - one with **Generate Mip Maps = ON**
  - one with **Generate Mip Maps = OFF**
- Use a script or simple interaction to switch between the two

### Output requirement

You should observe:

- Without mipmaps:
  - noisy patterns
  - shimmering when moving
- With mipmaps:
  - smoother and more stable image

------------------------------------------------------------------------

## 5. Normal Mapping Station

The purpose of this station is to demonstrate how surface detail can be simulated without changing geometry.

### Mathematical idea

Lighting depends on surface normals:

$$
I \propto \mathbf{N} \cdot \mathbf{L}
$$

Normal mapping modifies the normal vector $\mathbf{N}$ without modifying the actual geometry.

### Basic requirements

Your implementation should include:

- Two identical objects (e.g., planes or spheres)
- Both use the same base texture
- Only one object uses a **normal map**

### Suggested implementation

- Create two objects placed side by side
- Create two materials:
  - one without normal map
  - one with normal map
- Assign the same albedo texture to both
- Assign a normal map only to one material

### Lighting setup

- Use a directional or point light
- Place the light at an angle to the objects

### Output requirement

You should observe:

- The object without normal mapping appears flat
- The object with normal mapping appears to have surface detail
- The geometry is identical, but the shading differs

------------------------------------------------------------------------

## 6. Motion and Interaction

To better observe the effects, add simple interaction or motion.

### Requirement

At least one of the following should be implemented:

- the camera can move freely
- the light rotates or moves
- the user can toggle mipmaps

### Recommendation

For the sampling station:
- allow the user to toggle mipmaps

For the normal mapping station:
- use a moving or angled light to emphasize the effect

------------------------------------------------------------------------

## 7. Experiments

Perform the following experiments and observe the results:

1. Compare the three filtering modes and describe their visual differences.
2. Move closer and farther from the textured objects and observe how filtering behaves.
3. Toggle mipmaps and observe the effect on distant surfaces.
4. Move the camera slightly and observe aliasing artifacts when mipmaps are disabled.
5. Compare the two objects in the normal mapping station and describe the differences.
6. Change the light direction and observe how the normal-mapped object reacts.

------------------------------------------------------------------------

## 8. Questions

1. What is the difference between point, bilinear, and trilinear filtering?
2. Why does aliasing occur when textures are viewed at a distance or angle?
3. How do mipmaps reduce aliasing?
4. Why does normal mapping change the appearance of a surface without changing its geometry?
5. In your own words, explain why texture sampling is important in real-time rendering.
