using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Ship {
	void Start () {
        speed = 2f;
        reservedRadius = 40f;
	}

    public override string toString() {
        return "chaser";
    }
}
