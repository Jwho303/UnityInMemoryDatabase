# Unity In-Memory Database

This codebase aims to aid developers in making games using a Data-Orientated approach to store, manage and serve data tables from memory.

## Why use this?

MonoBehaviours in Unity have been used as a one-stop shop to house data and behaviour. While they are suitable for most projects they don't scale well unless well wrangled. This module seeks to separate data, behaviour and the viewable game object into their own processing steps, hopefully enabling your projects to scale to 100s and 1000s of on-screen game objects.

## Key Classes

### TableEntry

The base class from which all data models need to inherit. It provides a Guid for any child classes.

### Table

This is a data set class which holds all the instances of a class that inherits from `TableEntry` in a list.

### Database

This abstract class holds all the `Table` classes in a dictionary, using the `TableEntry` type as a key. It has several easy-to-use methods to store and retrieve data from the various tables.

## Usage

Create all the data models you would like to use for your project and inherit them from `TableEntry`.

Create a child `Database` class, this should use the initialize method the create all the tables on the database on awake.

To store data, use this `Database` instance to save new entries using `Insert`, and to retrieve this, use `Get` or `Find`.

## Additional systems

Inspired by ECS, I made a few other systems on top of the **In-Memory Database**.

### Entity Pool

This is an object pool pattern that uses a `GameEntity<T>`, a generic base class, to drive it. This base class type has to match a `TableEntry` class.

### Entity System

A base system that should be used to drive all the behaviour for a type of `TableEntry`.

### Entity Pool System

A child of `EntitySystem` which marries in an `EntityPool`. This ensures that there is an instantiated object for a new database entry and vice versa.

## Example Project

[https://jwho303.itch.io/inmemorydatabase](https://jwho303.itch.io/inmemorydatabase)

In the example project, there are 3 `TableEntry` types

- Ball
- Cube
- ColorEntry

2 types of `EntityPoolSystem` which spawn, despawn and update any objects/entries of their corresponding type.

- CubeSystem
- BallSystem

These are created by the `GameManager` class, which links any MonoBehaviour classes with c# classes as well as instantiates the `Database`.

### BallSystem

This system drives all the balls in play towards a target cube. When a ball reaches its cube it is assigned the color of the cube and a new random cube Id is assigned. No balls start in play.

### CubeSystem

Conversely, a few cubes start in play. This is to demo that already existing GameObject can be added to the `Database` on start and behave as expected. Clicking a cube randomizes its color, which is read by the balls.

### ColorEntry

This table has no system and only serves as a data set to load data from. It is populated on start from a scriptable object which hold all the initial color values.

### Performance

Tested on my MacBook Pro (15-inch, 2018)

- 2,6 GHz 6-Core Intel Core i7
- 16 GB 2400 MHz DDR4
- Radeon Pro 560X 4 GB

While stress testing I found that the framerate starts to dip from 60 fps after adding 1000 balls to the scene. However, this is due to each ball having its own material instance.
If all balls are using the same material and no color is being set then the system seems to start to dip after 3000 balls at 50 fps and the 40 fps at 5000 balls.
If there are no meshes on screen then the fps starts to diminish at 5000 balls and only goes below 50 fps after 7000 balls.