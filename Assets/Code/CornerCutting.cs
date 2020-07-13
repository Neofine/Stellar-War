using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerCutting : MonoBehaviour {
    public int closeness = 4;
    public float singleVecLength = 1f;
    public List<Vector3> smoothPath(List<Vector3> path, int howSmooth, Ship ship) {
        if (howSmooth == 0)
            return path;
        List<Tuple<Vector3, Vector3>> added = new List<Tuple<Vector3, Vector3>>();

        for (int i = 0; i < path.Count - 2; i++) {
            print("CORNERCUT");
            Vector3 first = path[i];
            Vector3 second = path[i + 1];
            Vector3 third = path[i + 2];
            int tmpCloseness = closeness;
            Vector3 thrQuarters, quarter2;
            do {
                print("CORNERCUT1");
                Vector3 diff = second - first;
                thrQuarters = second - diff / tmpCloseness;

                Vector3 diff2 = third - second;
                quarter2 = second + diff2 / tmpCloseness;
                tmpCloseness++;
            } while (!Game.getCompressingRoad().noObjectOnWay(thrQuarters, quarter2, ship));
            added.Add(new Tuple<Vector3, Vector3>(thrQuarters, quarter2));
        }

        int addIdx = 0, cutIdx = 0;

        while(addIdx != added.Count) {
            print("CORNERCUT2");
            cutIdx++;
            if (Game.getCompressingRoad().noObjectOnWay(added[addIdx].Item1, added[addIdx].Item2, ship) && Game.getGraph().vecLength(added[addIdx].Item1, added[addIdx].Item2) > singleVecLength) {
                path.RemoveAt(cutIdx);
                path.Insert(cutIdx, added[addIdx].Item2);
                path.Insert(cutIdx, added[addIdx].Item1);
                cutIdx++;
            }
            addIdx++;
        }
        return smoothPath(path, howSmooth - 1, ship);
    }
}
