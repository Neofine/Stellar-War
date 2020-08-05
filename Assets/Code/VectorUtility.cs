using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtility {
    public static Vector3 getRandPoint(Vector3 plCenter, float plnRad) {
        float xMin = plCenter.x - plnRad;
        float xMax = plCenter.x + plnRad;
        float xRand = Random.Range(xMin, xMax);
        float right = plnRad * plnRad - (xRand - plCenter.x) * (xRand - plCenter.x);
        float yDiff = Random.Range(0, Mathf.Sqrt(right));
        float ySet, zSet;

        if (Random.Range(0, 2) == 1)
            ySet = yDiff + plCenter.y;
        else
            ySet = -yDiff + plCenter.y;

        float zDiff = Mathf.Sqrt(right - yDiff * yDiff);
        if (Random.Range(0, 2) == 1)
            zSet = zDiff + plCenter.z;
        else
            zSet = -zDiff + plCenter.z;

        Vector3 pos = new Vector3(xRand, ySet, zSet);
        return pos;
    }
}
