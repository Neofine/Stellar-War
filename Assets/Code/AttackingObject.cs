using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingObject : MonoBehaviour {

    private List<Bullet> toMove;
    private float bulletSpeed = 5f;

    private void Start() {
        toMove = new List<Bullet>();
    }

    private class Bullet {
        public GameObject obj;
        public GameObject to;
        public Bullet(GameObject obj, GameObject to) {
            this.obj = obj;
            this.to = to;
        }
    }

    public void shootBullet(Vector3 from, GameObject to) {
        //toMove.Add
        GameObject spawned = Instantiate(GameObject.Find("Bullet"), from, Quaternion.identity);
        toMove.Add(new Bullet(spawned, to));
    }

    private void Update() {
        //foreach(Bullet bullet in toMove) {
        for (int i = 0; i < toMove.Count; i++) {
            Bullet bullet = toMove[i];
            if (bullet.to == null) {
                toMove.Remove(bullet);
                Destroy(bullet.obj);
                i--;
                continue;
            }
            Vector3 to = bullet.to.transform.position;
            GameObject obj = bullet.obj;
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, to, bulletSpeed);
            if (VectorUtility.vecLength(obj.transform.position, to) < 2) {
                toMove.Remove(bullet);
                Destroy(obj);
                check(bullet.to);
                i--;
            }
        }
    }

    void check(GameObject underSiege) {
        if (underSiege == null)
            return;
        if (underSiege.GetComponent<Clickable>().isShip()) {
            Ship ship = underSiege.GetComponent<Ship>();
            ship.changeHealthBy(-10);
        }
        else if (underSiege.GetComponent<Clickable>().isBuilding()) {
            Building building = underSiege.GetComponent<Building>();
            building.changeHealthBy(-10);
        }
        else {
            print("IT'S A TRAP!");
        }
    }

}
