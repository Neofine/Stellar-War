using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

public class Graph : MonoBehaviour {
    private class Point{
        public Vector3 point;
        public Point cameFrom;
        public float fromStart;
        public float toEnd;
        public Point(Vector3 point, float fromStart, float toEnd, Point cameFrom) {
            this.point = point;
            this.fromStart = fromStart;
            this.toEnd = toEnd;
            this.cameFrom = cameFrom;
        }

        
    }
    private class TupleComparer : IComparer<Tuple<float, Point>> {
        public int Compare(Tuple<float, Point> a, Tuple<float, Point> b) {
            if (a.Item1 != b.Item1)
                return a.Item1.CompareTo(b.Item1);
            if (a.Item2.point.x != b.Item2.point.x)
                return a.Item2.point.x.CompareTo(b.Item2.point.x);
            if (a.Item2.point.y != b.Item2.point.y)
                return a.Item2.point.y.CompareTo(b.Item2.point.y);
            if (a.Item2.point.z != b.Item2.point.z)
                return a.Item2.point.z.CompareTo(b.Item2.point.z);
            return 0;
        }
    }

    public bool isBlocked(Vector3 what, Ship asking = null, float distanceFromSurf = 70) {
        foreach (Planet planet in Game.getPlanets()) {
            Vector3 plnPos = planet.getObj().transform.position;
            float radius = planet.getRadPln() + distanceFromSurf;
            float xsquared = (what.x - plnPos.x) * (what.x - plnPos.x);
            float ysquared = (what.y - plnPos.y) * (what.y - plnPos.y);
            float zsquared = (what.z - plnPos.z) * (what.z - plnPos.z);
            if (xsquared + ysquared + zsquared <= radius * radius)
                return true;
        }
        foreach (Ship ship in Game.getShips()) {
            //if (asking != null)
            // print(ship.gameObject.transform.position + " " + asking.transform.position);
            if (ship.inMovement() || (asking != null && asking == ship))
                continue;
           // if (asking != null)
            //  print("Looking at it");
            Vector3 pos = ship.getObj().transform.position;
            float radius = ship.getRadius();
            float xsquared = (what.x - pos.x) * (what.x - pos.x);
            float ysquared = (what.y - pos.y) * (what.y - pos.y);
            float zsquared = (what.z - pos.z) * (what.z - pos.z);
            if (xsquared + ysquared + zsquared <= radius * radius) {
                //if (asking != null)
                //    print("BLOCKED");
                return true;
            }
                
        }
      //  if (asking != null)
          //  print("NOT BLOCKEd");
        return false;
    }

    private Vector3 point;
    private bool hasFreePoints(Vector3 center, float radius, Ship ship, float planetDist) {
        for (int i = 1; i <= 10; i++) {
            Vector3 randomPoint = VectorUtility.getRandPoint(center, radius);
            if (!isBlocked(randomPoint, ship, planetDist)) {
                point = randomPoint;
                return true;
            }
        }
        return false;
    }

    Vector3 chooseNearest(Vector3 dest, Ship ship, float planetDist) {
        if (!isBlocked(dest, ship, planetDist))
            return dest;
        float left = 5f;
        float right = 100f;
        float mid = 0;
        while (mid != (left + right) / 2) {
            mid = (left + right) / 2;    
            if (hasFreePoints(dest, mid, ship, planetDist)) {
                right = mid;
            }
            else left = mid + 1;
        }
        return point;
    }
 
    public List<Vector3> planRoute(Vector3 start, Vector3 end, float routePrecision, Ship ship, float planetDist = 70) {
        if (isBlocked(end, ship, planetDist)) {
            end = chooseNearest(end, ship, planetDist);
        }

        SortedSet<Tuple<float, Point>> queue = new SortedSet<Tuple<float, Point>>(new TupleComparer());
        Dictionary<Vector3, float> cost = new Dictionary<Vector3, float>();
        float minCost = VectorUtility.vecLength(start, end);

        queue.Add(new Tuple<float, Point> (minCost, new Point(start, 0f, minCost, null)));
        cost.Add(start, 0);
        
        while (queue.Count != 0) {
            Tuple<float, Point> examined = queue.Min;
            Vector3 inPoint = examined.Item2.point;
            queue.Remove(examined);

            if (!cost.ContainsKey(inPoint) || cost[inPoint] != examined.Item2.fromStart) {
                continue;
            }

            if (VectorUtility.vecLength(inPoint, end) <= 2 * routePrecision) {
                if (routePrecision <= 10)
                    return extractPath(start, examined.Item2);
                
                List<Vector3> now = extractPath(start, examined.Item2);
                List<Vector3> close = planRoute(inPoint, end, routePrecision / 2, ship, planetDist);
                close.AddRange(now);

                return close;
            }

            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    for (int z = -1; z <= 1; z++) {
                        Vector3 changing = new Vector3(x * routePrecision, y * routePrecision, z * routePrecision);
                        Vector3 newVector = inPoint + changing;
                        // !Game.getCompressingRoad().noObjectOnWay(newVector, inPoint, ship)
                        if (newVector == inPoint)
                            continue;
                        if (isBlocked(newVector, ship, planetDist))
                            newVector = chooseNearest(newVector, ship, planetDist);
                        
                        float toEnd = VectorUtility.vecLength(newVector, end);
                        float costToStart = examined.Item2.fromStart + VCost(changing, routePrecision);
                        Point newPoint = new Point(newVector, costToStart, toEnd, examined.Item2);

                        if (cost.ContainsKey(newVector) && cost[newVector] > costToStart) {
                            cost.Remove(newVector);
                            cost.Add(newVector, costToStart);
                            queue.Add(new Tuple<float, Point>(costToStart + toEnd, newPoint));
                        }
                        else if (!cost.ContainsKey(newVector)) {
                            cost.Add(newVector, costToStart);
                            queue.Add(new Tuple<float, Point>(costToStart + toEnd, newPoint));
                        }
                    }
                }
            }
        }
        // Surprisingly very informative comment.
        print("WTFTFTFTFTFTFTFTFT");
        return null;
    }

    private List<Vector3> extractPath(Vector3 start, Point end) {
        List<Vector3> path = new List<Vector3>();
        Point now = end;
        while (now.point != start) {
            //print("JK");
            path.Add(now.point);
            now = now.cameFrom;
        }
        path.Add(now.point);
        return path;
    }
    
    private float VCost(Vector3 what, float change) {
        if (what == Vector3.zero)
            return 0;
        if ((what.x == 0 && what.y == 0) || (what.x == 0 && what.z == 0) || (what.y == 0 && what.z == 0))
            return change;
        if (what.x == 0 || what.y == 0 || what.z == 0) {
            return change * (float)Math.Sqrt(2);
        }
        return change * (float)Math.Sqrt(3);
    }
}
