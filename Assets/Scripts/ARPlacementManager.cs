using UnityEngine;

public class ARPlacementManager : MonoBehaviour
{
    
    public GameObject tictactoePrefab;

    public GameController controller;

    private bool plateauPlace = false;

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
                    CaseData donnees = hit.collider.GetComponent<CaseData>(); //Gemini
                    controller.JouerTour(donnees.indexCase, hit.collider);
                }
                else if(plateauPlace == false)
                {
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    GameObject nouveauTicTacToe = Instantiate(tictactoePrefab, hit.point, rotation);
                    plateauPlace = true;
                }
            }
        }
    }

}
