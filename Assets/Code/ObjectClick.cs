using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour {

    private Shader outline;
    private Shader normal;
    private List<Ship> objHighlighted = new List<Ship>();
    private float meanHeight;

	void Start () {
        outline = Shader.Find("Outlined/Diffuse");
        normal = Shader.Find("Standard");
	}

    private GameObject ShootLaser(Ray ray) {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000000.0f)) {
            if (hit.transform != null) {
                return hit.transform.gameObject;
            }
        }
        return null;
    }

    private bool isEmpty() {
        return (objHighlighted.Count == 0);
    }

    private void Highglight(GameObject objClicked) {
        if (objClicked == null)
            return;
        Renderer rend = objClicked.GetComponent<MeshRenderer>();

        if (rend.material.shader != outline) {
            rend.material.shader = outline;
            if (isEmpty()) {
                meanHeight = objClicked.transform.position.y;
                Game.getMesh().spawn(meanHeight);
            }
            objHighlighted.Add(objClicked.GetComponent<Ship>());
        }
        else {
            rend.material.shader = normal;
            objHighlighted.Remove(objClicked.GetComponent<Ship>());
            if (isEmpty()) {
                Game.getMesh().destroy();
            }
        }
    }

    private void HighlightMulti(Vector3 start, Vector3 end) {
        float xSmall, xBig, ySmall, yBig;
        xSmall = Useful.min(start.x, end.x);
        xBig = Useful.max(start.x, end.x);
        ySmall = Useful.min(start.z, end.z);
        yBig = Useful.max(start.z, end.z);

        foreach (Ship ship in Game.getMovableObj()) {
            Vector3 pos = ship.getObj().transform.position;
            float xs, xb, ys, yb;
            xs = ClickCoords.getXSpec(pos, new Vector3(xSmall, 0f, yBig));
            xb = ClickCoords.getXSpec(pos, new Vector3(xBig, 0f, ySmall));
            ys = ClickCoords.getZSpec(pos, new Vector3(xBig, 0f, ySmall));
            yb = ClickCoords.getZSpec(pos, new Vector3(xSmall, 0f, yBig));
            if (pos.x <= xb && pos.x >= xs && pos.z <= yb && pos.z >= ys) {
                Highglight(ship.getObj());
            }
        }
    }

    public bool doFit(Vector3 point, MultiVarFun TL, MultiVarFun BR) {
        Vector2 TLres = TL.calculate(point.y);
        Vector2 BRres = BR.calculate(point.y);
        // here BRres.x means x coordinates, but BRres.y means <<Z COORDINATES>>
        // Same with TLres
        print("TL: " + TLres);
        print("BR: " + BRres);
        print("HERE: " + point);
        return (point.x <= BRres.x && point.x >= TLres.x && point.z <= TLres.y && point.z >= BRres.y);
    }

    private GameObject onWatch;
    private float timer;
    private Vector3 startCoursor, endCoursor, pixStart, pixEnd;
    private bool drawBox = false;

    void Update () {
        float timeBetween = Time.time - timer;
        if (Input.GetMouseButtonDown(0)) {
            timer = Time.time;
            onWatch = ShootLaser(Camera.main.ScreenPointToRay(Input.mousePosition));
            startCoursor = ClickCoords.getCords();
            pixStart = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && timeBetween >= 0.2f) {
            drawBox = true;
            endCoursor = ClickCoords.getCords();
            pixEnd = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) {
            if (timeBetween < 0.2f) {
                Highglight(onWatch);
            }
            else {
                drawBox = false;
                HighlightMulti(startCoursor, endCoursor);
            }
        }
    }

    private void OnGUI() {
        if (drawBox) {
            float  width, height;
            width = Useful.abs(pixStart.x - pixEnd.x);
            height = Useful.abs(pixStart.y - pixEnd.y);
            Rect box = new Rect(Useful.min(pixStart.x, pixEnd.x), Screen.height - Useful.max(pixStart.y, pixEnd.y), width, height);
            GUI.Box(box, "");
        }
    }

    public List<Ship> getObjHighlighted() {
        return objHighlighted;
    }
}
