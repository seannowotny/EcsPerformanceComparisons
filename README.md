# ECS Performance Comparisons

This Unity project is dedicated to comparing data-oriented design and various Entity Component System (ECS) libraries in the context of rapid prototyping.

The implementations emphasize simplicity and productivity over highly optimized, boilerplate heavy solutions.

Rendering systems are currently GameObject-based and slow, but can be disabled by pressing the space bar.

This project spawns 1000 Entities to match my own requirements. This is why the benchmarks results of different data-oriented solutions are so similar.

With higher entity counts bigger differences should be become apparent.

## Currently Included Showcase Scenes

- DOD (inspired by [Combat Bee Benchmarks](https://github.com/maskrosen/combat-bees-benchmarks))
- Bursted DOD
- AoS DOD (array of structs architecture)
- Bursted AoS DOD
- DefaultECS
- Arch
- Unity ECS

## Video Demonstration

[![Video Demo](https://img.youtube.com/vi/rrW4-jHXLG0/0.jpg)](https://www.youtube.com/watch?v=rrW4-jHXLG0)

## Profile Analyzer Results

Below is the performance comparison table. All values are in milliseconds.

| Solution        | Median   | Mean   | Min | Max | Range |
|-----------------|---------:|-------:|---------:|----------:|-----------:|
| AoS DOD         |     0.87 |   0.98 |      0.50|      8.27  |        7.77|
| Arch            |     0.93 |   1.02 |      0.73|     11.41  |       10.68|
| Bursted AoS DOD |     0.90 |   1.02 |      0.88|     19.24  |       18.36|
| Bursted DOD     |     0.90 |   0.96 |      0.87|      2.03  |        1.16|
| DefaultECS      |     0.90 |   1.09 |      0.76|     33.35  |       32.59|
| DOD             |     0.88 |   0.93 |      0.72|      2.27  |        1.55|
| Unity ECS       |     0.90 |   0.96 |      0.84|      1.85  |        1.01|

### Conclusions?

For 1000 entities all of these solutions are viable but some are better at keeping garbage collection and consequent hangups to a minimum.
The benchmark results don't capture these hangups very well and are best observed by running a build of the project yourself.

**Test Configuration:**
- CPU: AMD Ryzen 7 5800X
- GPU: GTX 1070
- Rendering systems: Disabled
- Build: Development Build

## This project is looking for contributors!

It would be awesome if **ECS Performance Comparisons** could make a more fair and comprehensive comparison between different data-oriented game development approaches in the future.

Take a look at our [issues page](https://github.com/seannowotny/EcsPerformanceComparisons/issues) if you would like to participate in working towards this goal!
