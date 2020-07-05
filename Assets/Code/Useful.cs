using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Useful {
    public static float min(float a, float b) {
        if (a < b)
            return a;
        return b;
    }

    public static float max(float a, float b) {
        if (a < b)
            return b;
        return a;
    }

    public static float abs(float a) {
        if (a > 0)
            return a;
        return -a;
    }
}
