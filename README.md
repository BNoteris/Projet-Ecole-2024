# Programmation orienté objet : Projet école

## Introduction
Ce projet vise à créer une application de gestion d'école, elle permet de créer des étudiants, des professeurs, des cours et les évaluations associées ainsi qu'une fonctionnalité supplémentaire qui permet de mettre un commentaire du professeur pour chaque note de l'étudiant

## Présentation de l'application
Je vais reprendre les différents menus de l'application afin de mieux comprendre son fonctionnement

### Création d'un étudiant
Pour créer un étudiant il suffit de remplir les différents informations demandées et il s'affiche dans la liste juste en dessous


![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/915e6c5c-7561-4b74-999d-beb16c2ac24c)


Si vous cliquez sur le nom d'un étudiant vous pourrez voir un menu apparaitre afin d'update ses données, le supprimer ou voir ses détails plus avancés mais nous y reviendrons plus tard


![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/df86e4d6-c6ec-4cf3-bc09-dcc4d1adbee1)

### Création d'un professeur
La méthode est la même que pour la création d'étudiants avec les mêmes options update, delete et details si on clique sur un nom

![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/0aa5ad18-0651-43ea-b742-d891db8a9f4e)


Ici on voit l'affichage quand on appuie sur "details"

![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/b15a21d1-ee21-4f12-bf5b-02107f93cfce)


### Création d'un cours
Pour créer un cours la méthode va encore être la même avec la petite différence que nous devons ajouter le nom d'un professeur auquel rattacher le cours.
Si on ne met pas un nom de professeur préexistant le cours ne sera pas rajouter à la liste.
Le même menu avec details, update et delete apparait si on clique sur un élément.

![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/dc63dd96-dbfb-49e8-9612-cdd7242741ba)


Ici la vue des détails du cours.


![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/bff58900-42c5-49bc-beae-c06c13acdd5a)


### Ajout d'une note
Pour pouvoir ajouter une note à un élève il faut aller dans les détails d'un élève donc nous retournons dans la liste des étudiants et cliquons sur "details" pour avoir ce menu dans lequel
nous retrouvons ses informations personnelles ainsi que sa moyenne générale calculée sur toutes les notes de la liste (ici il n'a pas encore de note donc la moyenne est bien de 0)


![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/6bd79179-1bf1-4245-a3db-0b7207adda95)


En appuyant sur le bouton "Add Entry" nous pouvons faire apparaitre le menu suivant, qui nous permet au choix de lui mettre une cote de 0 à 20 ainsi qu'un commentaire facultatif sur sa note.
Ou en remplissant une appréciation basé sur un système de lettres qui sont associées à des notes de faire la même chose que précédemment, à noter que si les lettres utilisées ne sont pas des appréciations reconnues
l'élève recevra un 0/20.
Les notes devant être associées à un cours, si le cours renseigné ne fait pas partie de la liste des cours existants alors aucune note ne sera ajoutée à l'étudiant


![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/9020a15b-2574-44d9-be0a-0a189a125afe)

Et donc ici nous pouvons voir à quoi peut ressembler le bulletin d'un étudiant avec plusieurs note, avec la possibilité comme toutes les autres listes de cliquer sur les notes pour les modifier au besoin


![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/1d2c2496-4d73-4d4b-9765-3e08dc483e85)



## Diagrammes
### Diagramme de classes
Ce diagramme représente toutes les classes utilisées dans la création de ce programme


![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/bcba248a-ef97-49e6-8ae3-6c8dcf2f67f7)


### Diagramme de séquence
Ce diagramme représente le déroulement de la crétion d'un professeur dans le programme



![image](https://github.com/BNoteris/Projet-Ecole-2024/assets/152865115/9b84cc30-51cf-41e1-b1f3-f94e39c099ab)



## Principe SOLID
Pour la réalisation de ce projet, il était demandé de respecter au moins 2 principes SOLID qui vont être décrits ci-dessous

### Open/closed Principle
Le premier principe mis en place est de faire en sorte que le code soit facile à adapter à de nouvelles demandes. Donc dans cette optique j'ai mis en place une interface reprenant les fonctions permettant aux classes de discuter avec la database.
Cette interface est utilisée pour les classes et sous-classes de "Person" car les autres classes avaient des besoins plus spécifiques, mais cette interface est parfaitement réutilisable si on veut rajouter des functions pour une nouvelle classe
"Proviseur" pour laquelle il suffirait de créer la classe "ProviseurFunctions".
```csharp 
﻿namespace Projet2023;

public interface DataFunctions<T>  //Interface de base pour la création de nouveaux types de personnes dans la database
{
     Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> GetIdByNameAsync(string name);
}
```

### Single Responsability Principle
En séparant le constructeur de mes différentes classes avec leurs attributs "Student", "Teacher", "Activity", etc,..  et les méthodes pour modifier la database cela me permet d'avoir un meilleur controle sur ce que fait chaque classe
```csharp 













