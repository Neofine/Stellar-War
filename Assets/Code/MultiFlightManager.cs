using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiFlightManager : MonoBehaviour {
    private List<Tuple<List<Ship>, Vector3> > concurFlights;
    private List<Tuple<Tuple<List<Ship>, Vector3>, int>> watchedFlights;

    private void Start() {
        concurFlights = new List<Tuple<List<Ship>, Vector3>>();
        watchedFlights = new List<Tuple<Tuple<List<Ship>, Vector3>, int>>();
    }

    /*private class concurrentFlight {
        private List<Ship> ships;
        private Vector3 destination;
        private bool hasLanded;
        public concurrentFlight(List<Ship> shrips, Vector3 dest) {
            ships = new List<Ship>(shrips);
            destination = dest;
            hasLanded = false;
        }
        public void hasLandedIndeed() {
            hasLanded = true;
        }

        public bool didLand() {
            return hasLanded;
        }
    }*/

    void monitor() {
        if (watchedFlights.Count == 0)
            return;
        for (int i = 0; i < watchedFlights.Count; i++) {
            Tuple<Tuple<List<Ship>, Vector3>, int> tupl = watchedFlights[i];
            foreach (Ship ship in tupl.Item1.Item1) {
                if (Game.getGraph().isBlocked(ship.gameObject.transform.position, ship)) {
                    print("CHANGED THANKS TO MONITOR");
                    Game.getMovOrg().calcRoute(ship, tupl.Item1.Item2, 50);
                }
            }
            print("HERE?");
            watchedFlights[i] = new Tuple<Tuple<List<Ship>, Vector3>, int>(new Tuple<List<Ship>, Vector3>(tupl.Item1.Item1, tupl.Item1.Item2), tupl.Item2 + 1);
            print("NOPP");
            if (watchedFlights[i].Item2 >= 10) {
                watchedFlights.Remove(watchedFlights[i]);
                i--;
            }
        }
    }

    void Update() {
        monitor();
        if (concurFlights.Count == 0)
            return;

        //Tuple<List<Ship>, Vector3> tupl;
        //foreach (Tuple<List<Ship>, Vector3> tupl in concurFlights) {
        for (int i = 0; i < concurFlights.Count; i++) { 
            Tuple<List<Ship>, Vector3> tupl = concurFlights[i];
            int amStanding = 0;
            foreach (Ship ship in concurFlights[i].Item1) {
                if (!Game.getStdMove().isShipMoving(ship)) {
                    //print("NOT ANYMORE " + ship.getObj());
                    //print("asking about " + )
                    if (Game.getGraph().isBlocked(ship.gameObject.transform.position, ship)) {
                        print("CHANGED");
                        Game.getMovOrg().calcRoute(ship, tupl.Item2, 50);
                    }
                }

                if (!Game.getStdMove().isShipMoving(ship))
                    amStanding++;
            }

            if (amStanding == tupl.Item1.Count) {
                print("DISCARDING");
                watchedFlights.Add(new Tuple<Tuple<List<Ship>, Vector3>, int>(tupl, 0));
                concurFlights.Remove(tupl);
                i--;
            }
        }
    }
    public void manage(List<Ship> ships, Vector3 destination) {
        print("MADE FLIGHT MANAGER");
        if (concurFlights.Count != 0) { 
            foreach (Tuple<List<Ship>, Vector3> tupl in concurFlights) {
                if (tupl.Item1 == ships)
                    concurFlights.Remove(tupl);
            }
        }
        concurFlights.Add(new Tuple<List<Ship>, Vector3>(ships, destination));
    }
}
