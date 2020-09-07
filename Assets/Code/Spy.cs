using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : Ship {
    void Start() {
        speed = 1.5f;
        reservedRadius = 20f;
        basicFunctions();
    }

    public override string toString() {
        return "spy";
    }
}
