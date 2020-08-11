using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSurfaceObjects : MonoBehaviour {
    private Planet planet;
    private Vector3 planetCenter;
    private float radius;
    private List<System.Tuple<Vector3, Vector3>> forbidden;
    private int amAdded = 0;
    private float clicked;
    private float timeBetween = 0.15f;
    private void Start() {
        planet = GetComponent<Planet>();
        forbidden = new List<System.Tuple<Vector3, Vector3>>();
        planetCenter = transform.position;
        radius = planet.getRadPln();
    }

    void Update() {
        //if (Time.time - clicked > timeBetween && !Input.GetKey(KeyCode.B)) {
        if (Time.time - clicked > timeBetween && amAdded < 5) {
            clicked = Time.time;
            StartCoroutine(spawnAcc());
        }
    }

    

    private IEnumerator spawnAcc() {
        Vector3 pos = VectorUtility.getRandPoint(planetCenter, radius);

        GameObject spawned = Instantiate(GameObject.Find("BmainBase"), pos, Quaternion.identity);
        spawned.transform.parent = gameObject.transform;
        Rigidbody rgb = spawned.GetComponent<Rigidbody>();
        spawned.transform.localScale = new Vector3(0.5f / transform.localScale.x, 0.5f / transform.localScale.y, 0.5f / transform.localScale.z);
        //rgb.constraints = RigidbodyConstraints.FreezePosition;
        spawned.transform.LookAt(planetCenter);
        spawned.transform.Rotate(new Vector3(-90f, 0f, 0f), Space.Self);
        rgb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        float timer = 0.0f;
        while (timer < timeBetween) {
            timer += Time.deltaTime;
            yield return null;
        }

        if (planet.isBlocked()) {
            print("DESTRo");
            Destroy(spawned);
        }
        else {
            spawned.AddComponent<Building>();
            amAdded++;
        }
    }
}
