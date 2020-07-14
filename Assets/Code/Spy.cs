using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : Ship {
    void Start() {
        speed = 1.5f;
    }

    public override string toString() {
        return "spy";
    }
}
