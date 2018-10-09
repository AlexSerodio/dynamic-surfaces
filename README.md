# Dynamic Surfaces - Performance Test
An Unity project made to compare the performance of two different surface components provided by Unity, the [Terrain Component](https://docs.unity3d.com/Manual/script-Terrain.html) and the [Mesh Component](https://docs.unity3d.com/ScriptReference/Mesh.html).

## Getting Started
To run the project you must have [Unity 3D](https://unity3d.com/) installed. This project was developed with Unity 2017.3.0 so it might not work for older versions.

## Understanding the project
The main goal here is test which surface component is better for dynamic modifications made at runtime. The surfaces used were the Terrain and Mesh components. Both have the same resolution, width, length, height and are submitted to the same functions. The only differences are how to access the surfaces height values (since Terrain uses a two-dimensional array to store the height values whereas Mesh uses a simple one-dimensional array) and how to update their colors (since Mesh Component can work with solid colors whereas Terrain seems to work only with alphamaps).

## Results
Here are some images showing the results achieved until now (using both static and dynamic texture). The yellow numbers on top indicate the frame rate of the scene. 

![prints](https://user-images.githubusercontent.com/23726229/35636641-f754a544-0698-11e8-9fae-e51cb311062f.jpg)

The images can't demonstrate the changes that occur in the frame rate during execution. So for more accurate results, you can download the project and run it to see the changes yourself.
