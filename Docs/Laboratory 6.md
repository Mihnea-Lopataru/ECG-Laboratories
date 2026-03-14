
# Laboratory 6 — Triangle Geometry in Unity

  

## 1. Objectives

  

The goal of this laboratory is to explore how basic triangle geometry is used in computer graphics.
Triangles are the fundamental building block of most 3D models, and understanding their properties is essential for many graphics algorithms.

By the end of this lab, students should be able to:

- Create a **triangle procedurally** in Unity
- Compute the **edges and centroid** of a triangle
- Compute the **surface normal** using the cross product
- Compute the **triangle area**
- Implement **barycentric coordinates**
- Determine whether a point lies **inside or outside** a triangle
- Observe how triangle properties update when **vertices move**

------------------------------------------------------------------------

  

## 2. Scene Setup

Create a new Unity scene called: Lab6_TriangleGeometry

### Required objects

- A **Ground Plane**
- A **Camera**
- A **Directional Light**
- 
- Three objects representing triangle vertices:

-  `Vertex_A`
-  `Vertex_B`
-  `Vertex_C`

A test point:
-  `Point_P`

  

You may use **small spheres or cubes** to represent the vertices and the point.

  

### Recommended positioning

Place the vertices so they form a visible triangle on the ground plane.
Ensure the camera can clearly see the triangle from above or at an angle.

The test point `Point_P` should initially be placed somewhere inside the triangle.

------------------------------------------------------------------------

  

## 3. Procedural Triangle Mesh

Create a script called: TriangleGeometry.cs

Attach this script to an empty object called: Triangle_System

The script should generate a **triangle mesh** using the positions of the three vertices.

### Basic requirements

The triangle mesh should be created from:

- 3 vertices
- 1 triangle face

Whenever the vertices move, the mesh must **update automatically**.

### Hint (Unity API)

You will likely use:

-  `Mesh`
-  `mesh.vertices`
-  `mesh.triangles`
-  `mesh.RecalculateBounds()`

------------------------------------------------------------------------

## 4. Computing Triangle Edges

Once the triangle is created, compute the **edge vectors**.

The edges are defined as:
- AB = B - A
- AC = C - A
- BC = C - B

From these vectors, compute and display:
- Length of AB
- Length of BC
- Length of CA

### Output requirement

You may display the values using:

- Console output (`Debug.Log`)
- On-screen UI text
- Any other simple visualization

------------------------------------------------------------------------

## 5. Triangle Centroid

Compute the **centroid** (geometric center) of the triangle.
The centroid is the average of the three vertices.

G = (A + B + C) / 3

### Visualization

Create a small object or marker showing the centroid position.
When the triangle vertices move, the centroid should update in real time.

------------------------------------------------------------------------

## 6. Triangle Normal

The **surface normal** indicates the orientation of the triangle.
Compute the normal using the cross product of two edge vectors.
  
Example: normal = Cross(AB, AC)

Normalize the result to obtain a unit vector.

### Visualization

Use a **LineRenderer** or `Debug.DrawRay()` to display the normal vector starting from the triangle centroid.

Experiment by moving the vertices and observe how the normal changes.

------------------------------------------------------------------------

## 7. Triangle Area

Compute the **area of the triangle**.

The magnitude of the cross product between two edges can be used to compute the area.

**area = 0.5 * |Cross(AB, AC)|**

Display the computed area in the Console or on screen.

  

### Experiment

Move the vertices farther apart and observe how the area changes.

------------------------------------------------------------------------

## 8. Barycentric Coordinates

Add a **movable point** in the scene called: Point_P

Compute the **barycentric coordinates** of this point relative to the triangle.

These coordinates represent how the point can be expressed as a combination of the triangle vertices.

The coordinates are typically represented as: (u, v, w)

### Requirements

Your program must compute and display:
-  `u`
-  `v`
-  `w`

Verify that the values approximately satisfy: u + v + w = 1

------------------------------------------------------------------------

## 9. Inside--Outside Test

Use the barycentric coordinates to determine whether the point lies **inside or outside** the triangle.

### Rule

The point is **inside the triangle** if:
- u ≥ 0
- v ≥ 0
- w ≥ 0

Otherwise, the point lies outside the triangle.

### Output requirement

Display one of the following: **Point is inside the triangle** or **Point is outside the triangle**

Move the point across the triangle and observe how the classification changes.

------------------------------------------------------------------------

  

## 10. Experiments

Perform the following experiments and observe the results.

1. Move one vertex and observe how the **triangle area** changes.
2. Move the vertices so the triangle becomes very thin. Observe what happens to the area.
3. Move the point `P` across the triangle and watch how the **barycentric coordinates** change.
4. Place the point outside the triangle and confirm the **inside/outside test** works correctly.
5. Move the vertices and observe how the **normal direction** rotates.

------------------------------------------------------------------------

  

## 11. Questions

1. Why is the cross product useful for computing triangle normals?
2. What happens to the triangle area when the three vertices become collinear?
3. Why do barycentric coordinates always sum to approximately 1?
4. How could barycentric coordinates be used to interpolate values across a triangle surface?
