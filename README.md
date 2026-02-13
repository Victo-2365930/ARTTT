# Tic Tac Toe en Réalité augmentée
## Par Yohan Rajotte
### Étudiant de 3e année au Cégep de Victoriaville
#### Version unity 6000.3.4f1, 2026-02-13

### Description
Projet en Unity 3D simulation utilisant Google XR.
Le projet permet de jouer une partie de tic tac toe dans un environnement virtuel de Unity.

### Packages utilisés
Google ARCore XR Plugin v6.3.3
AR Foundation
XR Core Utilities v2.5.3
XR Plugin Management v4.5.3
Pour la liste complète, lire le fichier ./Package/manifest.json

### Défis rencontrés
1) Initialisation de la logique entre l'utilisation du AR et la logique de jeu
2) Organisation et séparation des scripts dans l'environnement Unity
3) L'ancrage?
4) La motivation




### Liste exhaustive des prompts utilisés avec l'IA
Voici les prompts utilisés qui influent le code dans les scripts. À noter que l'utilisation de l'IA est aussi noté dans la description des 

Unity 3D Code C#, AR de Google
Lorsque je sélectionne un GameObject avec un tag, mais que je dois savoir lequel c'est parmi ce tag, comment est-ce que je l'identifie?

C#
Peux m'aider avec la façon d'écrire la logique
Au lieu de vérifier :
if((groupeCase[0] == groupeCase[1] & groupeCase[1] == groupeCase[2]) || (groupeCase[3] == groupeCase[4] && groupeCase[4] == groupeCase[5])

Faire un groupe de condition et utiliser une boucle pour vérifier chaque condition. Toujours 3 cases et je voudrais la logique pas la réponse.

Unity 3D, code C#
Comment faire dans mon script pour sélectionner toutes les instance d'un prefab x avec un tag?

Unity 3D, AR, google XR origin, script C#
J'aimerais désactiver le Trackable (scan) lorsque je place un gameObject
