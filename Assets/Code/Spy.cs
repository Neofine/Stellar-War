using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : Ship {
    void Start() {
        speed = 2f;
    }

    public override string toString() {
        return "spy";
    }
}
