using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerCutting : MonoBehaviour {
    public int closeness = 4;
    public List<Vector3> smoothPath(List<Vector3> path, int howSmooth, Ship ship) {
        if (howSmooth == 0)
            return path;
        List<Tuple<Vector3, Vector3>> added = new List<Tuple<Vector3, Vector3>>();

        for (int i = 0; i < path.Count - 2; i++) {
            Vector3 first = path[i];
            Vector3 second = path[i + 1];
            Vector3 third = path[i + 2];
            int tmpCloseness = closeness;
            Vector3 thrQuarters = Vector3.zero, thrQuarters2 = Vector3.zero, quarter = Vector3.zero, quarter2 = Vector3.zero;
            print(i);
            while ((thrQuarters == Vector3.zero && thrQuarters2 == Vector3.zero) || !Game.getCompressingRoad().noObjectOnWay(thrQuarters, quarter2, ship)) {
                print("making");
                MultiVarFun fun = new MultiVarFun(first, second);
                float ySum = first.y + second.y;
                float part = ySum / closeness;
                Vector2 cordsFst = fun.calculate(part);
                quarter = new Vector3(cordsFst.x, part, cordsFst.y);

                Vector2 cordsSnd = fun.calculate(ySum - part);
                thrQuarters = new Vector3(cordsSnd.x, ySum - part, cordsSnd.y);

                MultiVarFun fun2 = new MultiVarFun(second, third);
                float ySum2 = second.y + third.y;
                float part2 = ySum2 / closeness;
                Vector2 cordsFst2 = fun2.calculate(part2);
                quarter2 = new Vector3(cordsFst2.x, part2, cordsFst2.y);

                Vector2 cordsSnd2 = fun.calculate(ySum2 - part2);
                thrQuarters2 = new Vector3(cordsSnd2.x, ySum2 - part2, cordsSnd2.y);
                tmpCloseness++;
            }
            //added.Add(new Tuple<Vector3, Vector3>(quarter, thrQuarters));
            //added.Add(new Tuple<Vector3, Vector3>(quarter2, thrQuarters2));
            added.Add(new Tuple<Vector3, Vector3>(thrQuarters, quarter2));
        }

        int lastPathCount = path.Count - 1;

        for (int i = 0; i < lastPathCount - 1; i++) {
            print("IM IN " + i);
            Vector3 first = path[i];
            Vector3 second = path[i + 1];
            if (Game.getCompressingRoad().noObjectOnWay(added[i].Item1, added[i].Item2, ship)) {
                path.RemoveAt(i + 1);
                path.Insert(i + 1, added[i].Item2);
                path.Insert(i + 1, added[i].Item1);
            }
            
        }
        return smoothPath(path, howSmooth - 1, ship);
    }
}
