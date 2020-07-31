using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSurfaceObjects : MonoBehaviour {
    private Planet planet;
    private Vector3 plCenter;
    private float plnRad;
    private List<System.Tuple<Vector3, Vector3>> forbidden;
    private int amAdded = 0;
    private float clicked;
    private float timeBetween = 0.15f;
    private void Start() {
        planet = this.GetComponent<Planet>();
        print(planet);
        forbidden = new List<System.Tuple<Vector3, Vector3>>();
        plCenter = transform.position;
    }

    void Update() {
        print(amAdded);
       
        if (Time.time - clicked > timeBetween && !Input.GetKey(KeyCode.B)) {
            clicked = Time.time;
            StartCoroutine(spawnAcc());
        }
            

        //while (amAdded < 5)
        //    StartCoroutine(spawnAcc());
    }

    private IEnumerator spawnAcc() {
        plnRad = planet.getRadPln();
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
        GameObject spawned = Instantiate(GameObject.Find("BmainBase"), pos, Quaternion.identity);
        spawned.transform.parent = gameObject.transform;
        //spawned.AddComponent<Building>();
        spawned.transform.localScale = new Vector3(0.5f / transform.localScale.x, 0.5f / transform.localScale.y, 0.5f / transform.localScale.z);
        spawned.transform.LookAt(plCenter);
        spawned.transform.Rotate(new Vector3(-90f, 0f, 0f), Space.Self);
        float timer = 0.0f;
        while (timer < timeBetween) {
            timer += Time.deltaTime;
            yield return null;
        }
        if (planet.isBlocked()) {
            Destroy(spawned);
            //planet.unBlockAll();
            print("BLOCKED");
            //return false;
        }
        else {
            print("UNBLOCKED");
            spawned.AddComponent<Building>();
            amAdded++;
        }
        //return true;
    }
}
