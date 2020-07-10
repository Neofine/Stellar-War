using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MultiVarFun {
    public float ax, az, bx, bz;
    public MultiVarFun(float ax, float bx, float az, float bz) {
        this.ax = ax;
        this.az = az;
        this.bx = bx;
        this.bz = bz;
    }

    public MultiVarFun(Vector3 start, Vector3 end) {
        // assuming end.y == 0
        bx = end.x;
        bz = end.z;
        ax = (end.x - start.x) / start.y;
        az = (end.z - start.z) / start.y;
    }

    // given y returns x and z of functions
    public Vector2 calculate(float point) {
        MonoBehaviour.print("FUN1");
        return new Vector2(ax * point + bx, az * point + bz);
    }
    
}
