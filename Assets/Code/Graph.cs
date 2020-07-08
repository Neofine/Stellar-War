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

    private List<Vector3> forbidden;

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
            // TODO: Handle x or y being null, or them not having names
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

    void Start () {
		
	}

    // giving only from and to makes forbidden area cuboid
    public void forbid(Vector3 from, Vector3 to) {

    }

    private bool isBlocked(Vector3 what) {
        if (what.x * what.x + what.y * what.y + what.z * what.z <= 70 * 70)
            return true;
        return false;
    }

    float change = 20f;

    public bool isThisEnd(Vector3 here, Vector3 end) {
        Vector3 TopLeftBack = end - new Vector3(change, change, change);
        Vector3 BotRightFront = end + new Vector3(change, change, change);
        return (here.x >= TopLeftBack.x && here.x <= BotRightFront.x &&
                here.y <= TopLeftBack.y && here.y >= BotRightFront.y &&
                here.z <= TopLeftBack.z && here.z >= BotRightFront.z);
    }

    public List<Vector3> planRoute(Vector3 start, Vector3 end) {
        if (isBlocked(end) || isBlocked(start))
            return null;
        SortedSet<Tuple<float, Point>> queue = new SortedSet<Tuple<float, Point>>(new TupleComparer());
        Dictionary<Vector3, float> cost = new Dictionary<Vector3, float>();
        float minCost = minPath(start, end);

        queue.Add(new Tuple<float, Point> (minCost, new Point(start, 0f, minCost, null)));
        cost.Add(start, minCost);

        while (queue.Count != 0) {
                
            Tuple<float, Point> examined = queue.Min;
            Vector3 inPoint = examined.Item2.point;
            //print("IM IN " + inPoint + " SCORE : " + examined.Item1);
            queue.Remove(examined);

            if (isThisEnd(inPoint, end) || minPath(inPoint, end) <= change) {
                //print("END WITH " + iterations + " ITERATIONS");
                return extractPath(start, examined.Item2);
            }
                

            if (cost.ContainsKey(inPoint) && cost[inPoint] != examined.Item1)
                continue;

            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    for (int z = -1; z <= 1; z++) {
                        Vector3 changing = new Vector3(x * change, y * change, z * change);
                        Vector3 newVector = inPoint + changing;
                        //print("CHECKING " + newVector);
                        if (isBlocked(newVector) || newVector == inPoint)
                            continue;
                        float toEnd = minPath(newVector, end);
                        float costToStart = examined.Item2.fromStart + VCost(changing);
                        Point newPoint = new Point(newVector, costToStart, toEnd, examined.Item2);

                        try {
                            cost.Remove(newVector);
                            cost.Add(newVector, costToStart + toEnd);
                        } catch (ArgumentException) {
                            //print("IT ALREADY IS ON THE LIST");
                        }
                        queue.Add(new Tuple<float, Point>(costToStart + toEnd, newPoint));
                    }
                }
            }
        }
        return null;
    }

    private List<Vector3> extractPath(Vector3 start, Point end) {
        List<Vector3> path = new List<Vector3>();
        Point now = end;
        while (now.point != start) {
            path.Add(now.point);
            now = now.cameFrom;
        }
        path.Add(now.point);
        return path;
    }

    private float minPath(Vector3 start, Vector3 end) {
        return (float)Math.Sqrt((start.x - end.x) * (start.x - end.x) + (start.y - end.y) * (start.y - end.y) + (start.z - end.z) * (start.z - end.z));
    }

    private float VCost(Vector3 what) {
        if (what == Vector3.zero)
            return 0;
        if ((what.x == 0 && what.y == 0) || (what.x == 0 && what.z == 0) || (what.y == 0 && what.z == 0))
            return change;
        if (what.x == 0 || what.y == 0 || what.z == 0) {
            return change * (float)Math.Sqrt(2);
        }
        return change * (float)Math.Sqrt(3);
    }

    /*private float minPath(Vector3 start, Vector3 end) {
        Vector3 position = start;
        float cost = 0;
        while (position != end) {
            Vector3 applying = mostOptimal(position, end);
            cost += VCost(applying);
            start += applying;
        }
        return cost;
    }

    private Vector3 mostOptimal(Vector3 position, Vector3 end) {
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                for (int z = -1; z <= 1; z++) {
                    if (compare(x, position.x, end.x) && compare(y, position.y, end.y) && compare(z, position.z, end.z))
                        return new Vector3(x * change, y * change, z * change);
                }
            }
        }
        return Vector3.zero;
    }

    private bool compare(int sign, float a, float b) {
        if ((sign == -1 && a < b) || (sign == 0 && a == b) || (sign == 1 && a > b))
            return true;
        return false;
    }

    */
}
