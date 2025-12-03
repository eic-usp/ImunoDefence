using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public Tower turretToBuild;
    private List<Color> standardColors = new List<Color>();
    public bool canDrag = false;
    public static BuildManager instance;
    private bool placingTower = false;
    public GameObject button = null;

    private GameObject selectedSlot = null;

    [SerializeField] Transform pistaDePouso;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Mais de um BuildManager");
            return;
        }

        instance = this;
    }

    void Update()
    {
        if (!placingTower)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Tower clickedTower = hit.collider.GetComponent<Tower>();

                    if (clickedTower != null)
                    {
                        Install(clickedTower);
                        placingTower = true;
                        if (button != null)
                        {
                            button.SetActive(true);
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }

            if (Input.GetMouseButtonDown(1))
            {
                CancelPurchase();
                if (button != null)
                {
                    button.SetActive(false);
                }
            }
        }
    }

    public void PlaceTower()
    {
        Vector3 targetPosition = GetMouseWorldPos();
        turretToBuild.transform.position = targetPosition;

        if (selectedSlot != null)
        {
            ChangeSlotColor(selectedSlot, Color.white);
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject slot = hit.collider.gameObject;
            selectedSlot = slot;
            ChangeSlotColor(slot, Color.yellow);
        }

        canDrag = false;
        placingTower = false;
    }

    private void CancelPurchase()
    {
        canDrag = false;
        Shopping.instance.EarnGold(turretToBuild.cost);
        Destroy(turretToBuild.gameObject);
        turretToBuild = null;
        placingTower = false;

        if (selectedSlot != null)
        {
            ChangeSlotColor(selectedSlot, Color.white);
            selectedSlot = null;
        }
    }

    public void Install(Tower tower)
    {
        if (turretToBuild == null)
        {
            if (!Shopping.instance.ShellOut(tower.cost))
                return;

            turretToBuild = Instantiate(tower, new Vector3(-1000, -1000, -1000), Quaternion.Euler(new Vector3(0, 0, 0)));

            standardColors.Clear();

            foreach (Renderer r in turretToBuild.GetComponentsInChildren<Renderer>())
            {
                standardColors.Add(r.material.color);
            }

            canDrag = true;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 15;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void SetTurretTransform(Transform t)
    {
        if (turretToBuild != null)
        {
            turretToBuild.transform.position = t.position;
            turretToBuild.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        }
    }

    public void SetNaturalKiller(Tower tower)
    {
        if (turretToBuild != null)
        {
            Vector3 pos = new Vector3(pistaDePouso.position.x, pistaDePouso.position.y, pistaDePouso.position.z - 10f);
            turretToBuild.transform.position = pos;
            Tower nK = turretToBuild.GetComponent<Tower>();
            nK.Activate();
            nK.targets.Add(tower.gameObject);
        }
    }

    public void changeTurretColor(Color clr)
    {
        int i = 0;
        foreach (Renderer r in turretToBuild.GetComponentsInChildren<Renderer>())
        {
            if (clr == Color.white)
            {
                r.material.color = standardColors[i];
                i++;
            }
            else
            {
                r.material.color = clr;
            }
        }
    }

    public void ChangeSlotColor(GameObject slot, Color color)
    {
        Renderer slotRenderer = slot.GetComponent<Renderer>();
        if (slotRenderer != null)
        {
            slotRenderer.material.color = color;
        }
    }
}
