using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh : MonoBehaviour {
    private GameObject mesh;
    private GameObject spawned;
	public void spawn(float y) {
        spawned = MonoBehaviour.Instantiate(mesh, new Vector3(0f, y, 0f), Quaternion.identity);
    }

    public void destroy() {
        MonoBehaviour.Destroy(spawned);
    }

    private void changePosition(float howMuch) {
        if (Useful.abs((spawned.transform.position + new Vector3(0f, howMuch, 0f) * 3).y) <= 100)
          spawned.transform.position = spawned.transform.position + new Vector3(0f, howMuch, 0f) * 3;
    }

    private void Update() {
        if (mesh == null) {
            mesh = GameObject.Find("mesh");
        }
        if (spawned != null && Input.GetKey(KeyCode.LeftControl)) {
            changePosition(Input.mouseScrollDelta.y);
        }
    }

    public float getHeight() {
        return spawned.transform.position.y; 
    }
}
