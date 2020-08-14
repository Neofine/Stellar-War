using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ObjectClick : MonoBehaviour {

    private Shader outline;
    private Shader normal;
    private List<Ship> objHighlighted = new List<Ship>();
    private float meanHeight;
    private GameObject buildingClicked;
    private float timeBuildClicked;

	void Start () {
        outline = Shader.Find("Outlined/Diffuse");
        normal = Shader.Find("Standard");
	}

    private GameObject ShootLaser(Ray ray) {
        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (Physics.Raycast(ray, out hit, 10000000.0f, layerMask)) {
            if (hit.transform != null) {
                return hit.transform.gameObject;
            }
        }
        return null;
    }

    private bool isEmpty() {
        return (objHighlighted.Count == 0);
    }

    public void unHighlightAll() {
        Game.getMesh().destroy();
        foreach (Ship highlighted in objHighlighted) {
            foreach (Renderer r in highlighted.getObj().GetComponentsInChildren<Renderer>()) {
                r.material.shader = normal;
            }
        }
        objHighlighted = null;
    }

    private void highlight(GameObject objClicked, bool isShip) {
        Renderer rend = objClicked.GetComponent<MeshRenderer>();

        if (rend.material.shader != outline) {
            foreach (Renderer r in objClicked.GetComponentsInChildren<Renderer>()) {
                r.material.shader = outline;
            }
            if (isEmpty() && isShip) {
                meanHeight = objClicked.transform.position.y;
                print(Game.getMesh());
                Game.getMesh().spawn(meanHeight);
            }
            if (isShip)
              objHighlighted.Add(objClicked.GetComponent<Ship>());
        }
        else {
            foreach (Renderer r in objClicked.GetComponentsInChildren<Renderer>()) {
                r.material.shader = normal;
            }
            if (isShip)
               objHighlighted.Remove(objClicked.GetComponent<Ship>());
            if (isEmpty() && isShip) {
                Game.getMesh().destroy();
            }
        }
    }

    private void HighlightMulti(Vector3 start, Vector3 end) {
        float xSmall, xBig, ySmall, yBig;
        xSmall = Mathf.Min(start.x, end.x);
        xBig = Mathf.Max(start.x, end.x);
        ySmall = Mathf.Min(start.z, end.z);
        yBig = Mathf.Max(start.z, end.z);

        foreach (Ship ship in Game.getMovableObj()) {
            Vector3 pos = ship.getObj().transform.position;
            float xs, xb, ys, yb;
            xs = ClickCoords.getXSpec(pos, new Vector3(xSmall, 0f, yBig));
            xb = ClickCoords.getXSpec(pos, new Vector3(xBig, 0f, ySmall));
            ys = ClickCoords.getZSpec(pos, new Vector3(xBig, 0f, ySmall));
            yb = ClickCoords.getZSpec(pos, new Vector3(xSmall, 0f, yBig));
            if (pos.x <= xb && pos.x >= xs && pos.z <= yb && pos.z >= ys) {
                highlight(ship.getObj(), true);
            }
        }
    }

    public bool doFit(Vector3 point, MultiVarFun TL, MultiVarFun BR) {
        Vector2 TLres = TL.calculate(point.y);
        Vector2 BRres = BR.calculate(point.y);
        // here BRres.x means x coordinates, but BRres.y means <<Z COORDINATES>>
        // Same with TLres
        return (point.x <= BRres.x && point.x >= TLres.x && point.z <= TLres.y && point.z >= BRres.y);
    }

    private GameObject onWatch;
    private float timer;
    private Vector3 startCoursor, endCoursor, pixStart, pixEnd;
    private bool drawBox = false;

    void Update () {
        float timeBetween = Time.time - timer;
        if (Input.GetMouseButtonDown(1)) {
            print("WOLOLOL");
            GameObject clicked = ShootLaser(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (clicked != null && clicked.GetComponent<Clickable>().isBuilding()) {
                print("CLICKED BUILDING");
                highlight(clicked, false);
                timeBuildClicked = Time.time;
                buildingClicked = clicked;
            }
            else if (clicked != null && clicked.GetComponent<Clickable>().isPlanet()) {
                Planet planet = clicked.GetComponent<Planet>();
                Game.getScnLoad().loadMiniScene(planet);
            }
        }
        if (Game.getInspectMode())
            return;
        if (Input.GetMouseButtonDown(0)) {
            onWatch = ShootLaser(Camera.main.ScreenPointToRay(Input.mousePosition));
            startCoursor = ClickCoords.getCords();
            // double click on a planet moves game to designated scene
            if (timeBetween <= 0.2f) {
                if (onWatch != null && onWatch.GetComponent<Clickable>().isPlanet()) {
                    Planet planet = onWatch.GetComponent<Planet>();
                    Game.getScnLoad().loadScene(planet);
                }
            }
            timer = Time.time;
            pixStart = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && timeBetween >= 0.2f) {
            drawBox = true;
            endCoursor = ClickCoords.getCords();
            pixEnd = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) {
            if (timeBetween < 0.2f) {
                if (onWatch != null && onWatch.GetComponent<Clickable>().isShip()) {
                    highlight(onWatch, true);
                }
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
            width = Mathf.Abs(pixStart.x - pixEnd.x);
            height = Mathf.Abs(pixStart.y - pixEnd.y);
            Rect box = new Rect(Mathf.Min(pixStart.x, pixEnd.x), Screen.height - Mathf.Max(pixStart.y, pixEnd.y), width, height);
            GUI.Box(box, "");
        }
    }

    public List<Ship> getObjHighlighted() {
        return objHighlighted;
    }

    public GameObject getBuildingClicked() {
        return buildingClicked;
    }

    public float getTimeBuildClick() {
        return timeBuildClicked;
    }
}
