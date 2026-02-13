using TMPro;
using UnityEngine;

/// <summary>
/// Pour gérer la logique de la partie de Tic Tac Toe
/// </summary>
public class GameController : MonoBehaviour
{
    #region Variables

    //Gestion du jeu
    public bool plateauplace = false;
    public bool finDePartie = false;
    private bool joueur = false; // false = X | true = O
    private int caseActuelleInt = 0;
    private Collider caseActuelleCollider;
    private int[] jeuActuel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //0 = rien | 1 = X | 2 = O

    //Gestion du UI
    [Tooltip("Texte qui définie le joueur actuel")]
    public TextMeshProUGUI currentPlayerText;
    [Tooltip("Texte lors de la fin du jeu")]
    public TextMeshProUGUI endGameText;
    [Tooltip("Texte d'instruction")]
    public TextMeshProUGUI instruction;

    //Gestion des Prefabs
    [Tooltip("Prefab du plateau de tic tac toe")] 
    public GameObject Prefab_TTT;
    [Tooltip("Prefab du X")] 
    public GameObject Prefab_X;
    [Tooltip("Prefab du O")] 
    public GameObject Prefab_O;
    [SerializeField]
    private GameObject plateauTTT;
    public GameObject GetPlateauTTT()
    {
        return plateauTTT;
    }


    #endregion

    /// <summary>
    /// Pour jouer le tour de Tic Tac Toe
    /// </summary>
    /// <param name="indexCase">Numéro de la case affecté, int entre 0 et 8</param>
    /// <param name="laCase"> Collider de la case</param>
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

    /// <summary>
    /// Pour activer les éléments de la partie nulle
    /// </summary>
    private void PartieNulle()
    {
        endGameText.gameObject.SetActive(true);
        endGameText.text = "La partie est nulle :(";
        currentPlayerText.text = "Partie Terminée";
        finDePartie = true;
    }

    /// <summary>
    /// Pour activer les éléments d'une partie terminée
    /// </summary>
    private void PartieTerminee()
    {
        endGameText.gameObject.SetActive(true);
        endGameText.text = "Joueur " + (joueur ? "O" : "X") + " a gagné!";

        currentPlayerText.text = "Partie Terminée";
        finDePartie = true;

    }

    /// <summary>
    /// Pour effectuer le changement de joueur
    /// </summary>
    private void ChangementJoueur()
    {
        joueur = !joueur;

        currentPlayerText.text = "Tour de : " + (joueur ? "O":"X");
    }

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
        currentPlayerText.text = "Tour de : " + (joueur ? "O" : "X");
        endGameText.gameObject.SetActive(false);
        endGameText.text = "";

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

    /// <summary>
    /// Pour placer la planche de TicTacToe dans l'environnement
    /// </summary>
    /// <param name="position">Vector3 où placer la planche</param>
    /// <param name="rotation">Quaternion de la rotation de l'objet</param>
    public void PlacerTicTacToe(Vector3 position, Quaternion rotation)
    {
        if(plateauTTT is null)
        {
            plateauTTT = Instantiate(Prefab_TTT, position, rotation);
        }
        else
        {
            plateauTTT.SetActive(true);
            plateauTTT.transform.position = position;
            plateauTTT.transform.rotation = rotation;
        }
        instruction.gameObject.SetActive(false);
    }

    /// <summary>
    /// Pour masquer la planche de TicTacToe
    /// </summary>
    public void MasquerTicTacToe()
    {
        plateauTTT.SetActive(false);
        plateauplace = false;
        instruction.gameObject.SetActive(true);
    }


}
