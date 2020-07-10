using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh : MonoBehaviour{
    private GameObject mesh;
    private GameObject spawned;
	public void spawn(float y) {
        print("MESH");
        spawned = MonoBehaviour.Instantiate(mesh, new Vector3(0f, y, 0f), Quaternion.identity);
        spawned.transform.Rotate(90f, 0f, 0f);
    }

    public void destroy() {
        print("MESH1");
        MonoBehaviour.Destroy(spawned);
    }

    private void changePosition(float howMuch) {
        print("MESH2");
        if (Useful.abs((spawned.transform.position + new Vector3(0f, howMuch, 0f) * 3).y) <= 100)
          spawned.transform.position = spawned.transform.position + new Vector3(0f, howMuch, 0f) * 3;
    }

    private void Update() {
        if (mesh == null) {
            print("MESH3");
            mesh = GameObject.Find("siatka2");
        }
        if (spawned != null && Input.GetKey(KeyCode.LeftControl)) {
            print("MESH4");
            changePosition(Input.mouseScrollDelta.y);
        }
    }

    public float getHeight() {
        print("MESH5");
        return spawned.transform.position.y; 
    }
}
