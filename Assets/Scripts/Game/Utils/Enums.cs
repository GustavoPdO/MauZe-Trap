using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
    public enum GameState {
        Menu = 0,
        Levels = 1
    }

    public enum WallType {
        None = 0,
        Concrete = 1,
        Glass = 2,
        Trap = 3
    }

    public enum Direction {
        North = 180,
        East = 270,
        South = 0,
        West = 90
    }

    public enum Rotation {
        Clockwise = 90,
        CounterClockwise = -90
    }
}
