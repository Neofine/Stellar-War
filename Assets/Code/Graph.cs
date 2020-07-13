﻿using System;
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

    public void forbid(Vector3 from, Vector3 to) {

    }

    private bool isBlocked(Vector3 what) {
        return (what.x * what.x + what.y * what.y + what.z * what.z <= 70 * 70);
    }
 
    public List<Vector3> planRoute(Vector3 start, Vector3 end, float routePrecision, Ship ship) {
        if (isBlocked(end) || isBlocked(start))
            return null;
        SortedSet<Tuple<float, Point>> queue = new SortedSet<Tuple<float, Point>>(new TupleComparer());
        Dictionary<Vector3, float> cost = new Dictionary<Vector3, float>();
        float minCost = vecLength(start, end);

        queue.Add(new Tuple<float, Point> (minCost, new Point(start, 0f, minCost, null)));
        cost.Add(start, 0);
        int iteration = 0;
        while (queue.Count != 0) {
            print("ITERATION NR + " + (++iteration));
            //print("JUST CHILLIN");
            Tuple<float, Point> examined = queue.Min;
            Vector3 inPoint = examined.Item2.point;
            queue.Remove(examined);

            if (!cost.ContainsKey(inPoint) || cost[inPoint] != examined.Item2.fromStart) {
                print("OLOL");
                continue;
            }

            if (vecLength(inPoint, end) <= routePrecision) {

                if (routePrecision <= 1)
                    return extractPath(start, examined.Item2);
                List<Vector3> now = extractPath(start, examined.Item2);
                List<Vector3> close = planRoute(inPoint, end, routePrecision / 2, ship);
                if (close == null)
                    return null;
                close.AddRange(now);

                return close;
            }

            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    for (int z = -1; z <= 1; z++) {
                        Vector3 changing = new Vector3(x * routePrecision, y * routePrecision, z * routePrecision);
                        Vector3 newVector = inPoint + changing;
                        
                        if (isBlocked(newVector) || newVector == inPoint)
                            continue;
                        float toEnd = vecLength(newVector, end);
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

    public float vecLength(Vector3 start, Vector3 end) {
        return (float)Math.Sqrt((start.x - end.x) * (start.x - end.x) + (start.y - end.y) * (start.y - end.y) + (start.z - end.z) * (start.z - end.z));
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
