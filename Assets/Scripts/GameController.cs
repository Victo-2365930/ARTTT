using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controleur de la partie de Tic Tac Toe
/// </summary>
public class GameController : MonoBehaviour
{
    #region Variables
    public bool finDePartie = false;
    private bool joueur = false; // false = X | true = O
    private int caseActuelleInt = 0;
    private Collider caseActuelleCollider;
    public int[] jeuActuel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //0 = rien | 1 = X | 2 = O

    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI winnerText;

    public GameObject Prefab_X;
    public GameObject Prefab_O;


    #endregion

    public void JouerTour(int indexCase, Collider laCase)
    {
        if (finDePartie || jeuActuel[indexCase] != 0) return;
        caseActuelleInt = indexCase;
        caseActuelleCollider = laCase;
        PlacerPiece();
        VerifierFinDePartie();
        ChangementJoueur();
        caseActuelleInt = 0;
    }

    /// <summary>
    /// Prend la position de la caseActuelle et 
    ///     applique le X ou O selon le tour du joueur
    /// </summary>
    private void PlacerPiece()
    {
        Vector3 position = caseActuelleCollider.transform.position;
        Quaternion rotation = caseActuelleCollider.transform.rotation;
        GameObject nouveauPrefab = Instantiate(joueur?Prefab_O:Prefab_X, position, rotation);
        nouveauPrefab.transform.SetParent(caseActuelleCollider.transform);
        jeuActuel[caseActuelleInt] = joueur ? 2 : 1;
    }

    /// <summary>
    /// Pour effectuer la vérification des lignes
    /// Idée de l'utilisation de int[][] par Gemini
    /// </summary>
    private void VerifierFinDePartie()
    {
        /*   Cases
         *  0  1  2
         *  3  4  5
         *  6  7  8
         */
        int[][] combinaisons = new int[][]
        {
        new int[] { 0, 1, 2 }, // Ligne horizontale Haut
        new int[] { 3, 4, 5 }, // Ligne horizontale milieu
        new int[] { 6, 7, 8 }, // Ligne horizontale bas
        new int[] { 0, 3, 6 }, // Ligne verticale gauche
        new int[] { 1, 4, 7 }, // Ligne verticale milieu
        new int[] { 2, 5, 8 }, // Ligne verticale droite
        new int[] { 0, 4, 8 }, // Ligne diagonale \
        new int[] { 2, 4, 6 }, // Ligne verticale /
        };

        foreach (var ligne in combinaisons)
        {
            var caseA = jeuActuel[ligne[0]];
            var caseB = jeuActuel[ligne[1]];
            var caseC = jeuActuel[ligne[2]];

            if(caseA != 0 && 
                caseA ==  caseB && 
                caseB == caseC)
            {
                PartieTerminee();
                return;
            }
        }

        bool partieNulle = true;
        foreach (var laCase in jeuActuel)
        {
            if (laCase == 0)
            {
                partieNulle = false;
                break;
            }
        }

        if (partieNulle) PartieNulle();
    }

    private void PartieNulle()
    {
        Debug.Log("La partie est nulle");
        currentPlayerText.text = "Partie Terminée";
        finDePartie = true;
    }

    private void PartieTerminee()
    {
        winnerText.gameObject.SetActive(true);
        winnerText.text = "Joueur " + (joueur ? "O" : "X") + " a gagné!";

        currentPlayerText.text = "Partie Terminée";
        finDePartie = true;

    }

    private void ChangementJoueur()
    {
        joueur = !joueur;

        currentPlayerText.text = "Tour de : " + (joueur ? "O":"X");
    }
    /*
    private void ligneGagnante(int caseA, int caseB)
    {
        Vector3 positionA = groupeCase[caseActuelleInt].transform.position;
        Vector3 positionB = groupeCase[caseActuelleInt].transform.position;

        //placer le prefab et mettre la rotation?
    }
    */

    /// <summary>
    /// Pour remettre les variables du jeu au stade de début
    /// Logique FindGameObjectsWithTag par Gemini
    /// </summary>
    public void RecommencerPartie()
    {
        finDePartie = false;
        joueur = false;
        caseActuelleInt = 0;
        caseActuelleCollider = null;
        currentPlayerText.text = "Scanning";
        winnerText.gameObject.SetActive(false);
        winnerText.text = "";

        for (int i = 0; i < jeuActuel.Length; i++)
        {
            jeuActuel[i] = 0;
        }

        GameObject[] prefabsXO = GameObject.FindGameObjectsWithTag("XO");
        foreach (GameObject prefab in prefabsXO)
        {
            Destroy(prefab);
        }
    }
}
