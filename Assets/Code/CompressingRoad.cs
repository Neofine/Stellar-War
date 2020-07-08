using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CompressingRoad : MonoBehaviour{
    public List<Vector3> compress(Ship ship, List<Vector3> road) {
        //return road;
        UnityEngine.Mesh mesh = ship.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        
        //print(mesh.bounds.size.x * ship.transform.localScale.x);

        List<Vector3> answer = new List<Vector3>();
        Vector3 now = ship.gameObject.transform.position;
        //Debug.DrawLine(now, road[road.Count - 1], Color.white, 5f);
        RaycastHit hit;
        //print(Physics.Linecast(new Vector3(70, 10, -42), new Vector3(90, 10, -22), out hit));
        //print(hit.collider.gameObject.name);
        int idx = 1;
        print("START SHORTCUT");
        while (now != road[road.Count - 1]) {
            while (idx <= road.Count - 2 && noObjectOnWay(now, road[idx], mesh, ship)) {
                idx++;
            }
            now = road[idx - 1];
            answer.Add(road[idx - 1]);
            print(road[idx - 1]);
            idx++;
        }
        print("END SHORTCUT");
        return answer;
    }

    private bool noObjectOnWay(Vector3 start, Vector3 end, UnityEngine.Mesh mesh, Ship ship) {
        float width = mesh.bounds.size.x * ship.transform.localScale.x;
        float height = mesh.bounds.size.y * ship.transform.localScale.y;
        //print(height + " " + width);
        //Debug.DrawLine(start, end, Color.red);
        //SleepTimeout(500);
        return !Physics.Linecast(start, end) && !Physics.Linecast(start - new Vector3(width / 1, 0, 0), end - new Vector3(width / 1, 0, 0)) &&
            !Physics.Linecast(start + new Vector3(width / 1, 0, 0), end + new Vector3(width / 1, 0, 0)) &&
            !Physics.Linecast(start - new Vector3(0, height / 1, 0), end - new Vector3(0, height / 1, 0)) &&
            !Physics.Linecast(start + new Vector3(0, height / 1, 0), end + new Vector3(0, height / 1, 0));
    }
}
