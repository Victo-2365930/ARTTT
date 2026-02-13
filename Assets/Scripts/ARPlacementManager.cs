using UnityEngine;
using UnityEngine.UIElements;

public class ARPlacementManager : MonoBehaviour
{
    public GameController controller;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.CompareTag("Case"))
                {
                    CaseData donnees = hit.collider.GetComponent<CaseData>();
                    controller.JouerTour(donnees.indexCase, hit.collider);
                }
                else if(controller.plateauplace == false)
                {
                    Vector3 position = hit.point;
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    controller.PlacerTicTacToe(position, rotation);
                    controller.plateauplace = true;
                }
            }
        }
    }

}
