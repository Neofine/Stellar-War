using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSurfaceObjects : MonoBehaviour {
    private Planet planet;
    private Vector3 plCenter;
    private float plnRad;
    private List<System.Tuple<Vector3, Vector3>> forbidden;
    private void Start() {
        planet = this.GetComponent<Planet>();
        print(planet);
        forbidden = new List<System.Tuple<Vector3, Vector3>>();
        plCenter = transform.position;
    }
    public void forbid(Vector3 what) {
        int amount = 10;
        Vector3 topLeft = what - new Vector3(amount, amount, amount);
        Vector3 botRight = what + new Vector3(amount, amount, amount);
        forbidden.Add(new System.Tuple<Vector3, Vector3>(topLeft, botRight));
    }

    public bool isFrobidden(Vector3 where) {
        foreach (System.Tuple<Vector3, Vector3> notHere in forbidden) {
            Vector3 topLeft = notHere.Item1;
            Vector3 botRight = notHere.Item2;
            if (where.x >= topLeft.x && where.y >= topLeft.y && where.z >= topLeft.z &&
                where.x <= botRight.x && where.y <= botRight.y && where.z <= botRight.z)
                return true;
        }
        return false;
    }

    void Update() {
        //if (Input.GetKey(KeyCode.B)) {
        //    int spawningAmount = 5;
        //    int spawnedAm = 0;
        //    while (spawnedAm < spawningAmount) {
        //        if (spawnAcc())
        //            spawnedAm++;
        //    }
        //}
        if (Input.GetKey(KeyCode.B))
            spawnAcc();
    }

    private bool spawnAcc() {
        plnRad = planet.getRadPln();
        float xMin = plCenter.x - plnRad;
        float xMax = plCenter.x + plnRad;
        float xRand = Random.Range(xMin, xMax);
        float right = plnRad * plnRad - (xRand - plCenter.x) * (xRand - plCenter.x);
        print(right);
        float yDiff = Random.Range(0, Mathf.Sqrt(right));
        float ySet, zSet;
        if (Random.Range(0, 2) == 1)
            ySet = yDiff + plCenter.y;
        else
            ySet = -yDiff + plCenter.y;
        float zDiff = Mathf.Sqrt(right - yDiff * yDiff);
        print(right - yDiff * yDiff);
        if (Random.Range(0, 2) == 1)
            zSet = zDiff + plCenter.z;
        else
            zSet = -zDiff + plCenter.z;
        Vector3 pos = new Vector3(xRand, ySet, zSet);
        if (!isFrobidden(pos)) {
            forbid(pos);
            GameObject spawned = Instantiate(GameObject.Find("mainBase"), pos, Quaternion.identity, transform);
            spawned.transform.localScale = new Vector3(0.5f / transform.localScale.x, 0.5f / transform.localScale.y, 0.5f / transform.localScale.z);
            spawned.transform.LookAt(plCenter);
            spawned.transform.Rotate(new Vector3(-90f, 0f, 0f), Space.Self);
            return true;
        }
        return false;
    }
}
