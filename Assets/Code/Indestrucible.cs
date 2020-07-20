using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indestrucible : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}
