# fna

This is a fork of [FNA](https://github.com/FNA-XNA/FNA) that makes a few changes:

- it merges in tweaks introduced in [tModLoader/FNA](https://github.com/tModLoader/FNA),
- it removes `Vector2`, `Vector3`, `Vector4`, `Matrix`, and `Quaternion` in favor of their `System.Numerics` equivalents,
- it modifies the implementation of `Color` to use a union-based implementation (explicitly-laid-out struct with field offsets).
