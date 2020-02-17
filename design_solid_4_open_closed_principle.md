# Design SOLID 4 - Open Closed Principle

*Après cette quête tu comprendras le principe Ouvert/Fermé*

## Objectifs

* Comprendre les états ouvert/fermé
* Utiliser le mot-clé `sealed`

## Etapes

### Ouvert à l'extension, fermé à la modification

L'idéal lorsque l'on écrit un code est de ne jamais avoir à modifier celui-ci afin de corriger un bug ou de rajouter une fonctionnalité. Ca tombe bien, un principe SOLID vient nous aider à réaliser cet objectif, tout cela à l'aide d'un énoncé simple:
**Les entités logicielles: classes, espaces de noms, méthodes ect ... doivent être ouvertes à l'extension, mais fermées à la modification.**

> Qu'est-ce que ça peut bien vouloir dire ?

Résumé simplement, le principe ouvert/fermé t'oblige à écrire un code **une fois pour toute**, cela en **fermant** ton code à la modification et en l'**ouvrant** à l'extension. Cela permet au code d'être modulaire, et *réduit le couplage* entre les classes.

> Et en quoi ça participe à réduire le couplage ?

De manière classique, lorsque l'on veut partager des fonctionnalités entre objets, on écrit les comportements que partagent les objets dans une classe abstraite. C'est assez réaliste, cependant cela *couple* les enfants avec les parents et réduit la modularité du code.

> Alors quoi, je bannis les héritages ?

Il est vrai qu'utiliser l'héritage peut sembler être une solution très efficace. Elle l'est, toutefois, utiliser une chaîne d'héritage complexe rend un code source monolithique en *couplant* un grand nombre de classes entre elles. Donc ajouter une modification requiert une connaissance absolue du code source et une grande attention.

> Une solution ?

Il n'y a de solution à la complexité logicielle que ton cerveau et tes collaborateurs. Cependant, utiliser des interfaces plutôt que des classes abstraites participe à réduire le *couplage* de manière conséquente, puisqu'une interface ne décrit aucun détail d'implémentation. Par conséquent, une méthode qui prend en argument une interface permet une modularité plus grande car on peut utiliser dans cette méthode *tous les objets* implémentant l'interface. Les objets implémentant l'interface peuvent-être très différents.

> Je savais déjà tout ça ... merci

Et heureusement que tu le sais déjà, car l'utilisation des interfaces va grandement t'aider à écrire un code respectant l'OCP (ainsi que tous les autres principes).

> Et pourquoi c'est si utile ?

Si l'on analyse à nouveau l'énoncé, on comprend que les classes et comportements que l'on écrits *ne doivent pas être modifiés*. Les classes implémentant une interface sont:
* Fermées à la modification: l'objet utilisant une interface ne dépend pas de l'implémentation de l'interface qu'il utilise
* Ouvertes à l'extension: on peut aisément créer d'autres classes implémentant la mếmé interface. On peut alors aisément les réutiliser dans tout le code sans dépendre de leur implémentation.

#### Ressources

* [Wikipédia - Code source monolithique](https://en.wikipedia.org/wiki/Monolithic_application)

### Composition over inheritance

Un débat houleux secoue le monde des développeurs. Son sujet est le suivant:
> Devrais-je utiliser une classe abstraite ou une interface ?

Un principe de programmation orienté objet existe pour résoudre le dilemme. Il propose tout bonnement de **ne plus utiliser l'héritage** mais d'***utiliser la composition***. Son nom est évocateur: ***Composition over Inheritance***.

Le principe est simple: à la place de faire hériter tes classes d'autres classes, il faut qu'elles implémentent une interface. Pour rappel, les interfaces *décrivent les actions possibles* d'une classe mais pas leur comportement. Ainsi, il est possible d'utiliser uniformément des objets différents, grâce au mécanisme de ***polymorphisme***. Le polymorphisme permet à un sous-type de données (une classe enfant d'une autre) d'être utilisé comme un de ses types parents. Et bien le polymorphisme n'est pas uniquement achevé grâce à l'héritage, il est aussi réalisable avec les interfaces: deux objets de types différents peuvent-être manipulés via la même interface.

En implémentant des interfaces, les entités logicielles utilisant les interfaces *ne connaissent pas* l'implémentation des objets qu'elles utilisent. Notre programme gagne en modularité et une évolution plus pérenne lui est assurée.

> Alors, plus jamais d'héritages ?

Et bien ... si. Dans certains cas l'héritage est un meilleur choix. Il ne faut pas appliquer naïvement la composition car un écrit simplement avec un héritage peut vite devenir un casse-tête avec la composition.

> Et le principe ouvert/fermé dans tout ça ?

#### Ressources

* [Composition or Inheritance ? - Thread StackOverflow](https://stackoverflow.com/questions/49002/prefer-composition-over-inheritance)

### Le mot-clé `sealed`

Il est aussi possible de fermer une classe à l'héritage. Cela apparraît souvent dans des bibliothèques logicielles où le concepteur empêche certains classes d'être héritées. Le mot-clé `sealed` indique qu'une classe quelconque ne peut-être héritée.
Ainsi, si on veut l'utiliser qu'une autre classe l'utilise, il va falloir utiliser la composition. Une classe marquée `sealed` a très souvent une interface lui étant destinée. L'utilisation du mot-clé `sealed` marque l'intention du développeur quant
à l'utilisation de la classe qu'il a écrite.

Voici un exemple de classe **scellée**:
```C#
// Can not create an instance of ParentClass
// This class is only meant to be inherited
// or used a polymorphic way.
public abstract ParentClass
{
   virtual void doSomething();
}

public sealed class SealedClass() : ParentClass
{
   void override doSomething()
   {
      Console.WriteLine("I am sealed");
   }
}
```

Il est aussi possible de sceller des méthodes en ajoutant le mot-clé `sealed` avant le type de la méthode.

#### Ressources

* [Mot-clé sealed](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed)

### Challenge

### Une violation de l'OPC

Les activités d'une industrie peuvent évoluer. Tantôt elle fabriquera des objets avec un matériel, tantôt avec un autre ou avec plusieurs autres. C'est pour cela qu'une industrie doit rester flexible et à l'écoute du changement. Son process d'adaptation au changement doit être optimisé au maximum pour qu'elle puisse suivre tous les virages abrupts du marché sans faire d'accidents. Une usine doit prévoir un changement de ses machines et de ses pièces. De même que le code qui permet de les faire fonctionner.

Dans ce challenge il faut permettre à une usine de produire des objets en plusieurs matières en appliquant le principe ouvert/fermé. Dans la fiche de code qui suit, l'usine produit des objets en bois et en métal. Cependant, on remarque vite qu'il y a une duplication de code dans les méthodes `CraftMetal` et `CraftWood` de classe `Factory`, il devrait y avoir un moyen de dédupliquer cela au moins ?

Plus que de dédupliquer le code, tu vas le rendre facilement extensible et ne donner aucune raison de modifier le code à l'avenir en le fermant à l'extension.

Voici le **bad smelling code**:
```C#
using System;
using System.Collections.Generic;

namespace OpenClosedPrincipleViolation
{
	public class Program
	{
		public static void Main()
		{
			Factory factory = new Factory();

			List<Wood> woodBlocks = new List<Wood>
			{
				new Wood(),
				new Wood()
			};

			List<Metal> metalBlocks = new List<Metal>
			{
				new Metal(),
				new Metal()
			};
			List<Production> manyProductions = new List<Production>
			{
				factory.CraftWood(woodBlocks),
				factory.CraftMetal(metalBlocks)
			};

			foreach (Production production in manyProductions)
			{
				Console.WriteLine(production);
			}
		}
	}

	public sealed class Factory
	{
		public Production CraftWood(IEnumerable<Wood> manyWoodBlocks)
		{
			Production newProduction = new Production(manyWoodBlocks);
			return newProduction;
		}

		public Production CraftMetal(IEnumerable<Metal> manyMetalBlocks)
		{
			Production newProduction = new Production(manyMetalBlocks);
			return newProduction;
		}
	}

	public class Production
	{
		public IEnumerable<Material> Materials { get; set; }

		public Production(IEnumerable<Material> manyMaterials)
		{
			Materials = manyMaterials;
		}

		public override string ToString()
		{
			string description = "Object is composed of\n:";
			foreach (Material Material in Materials)
			{
				description += "\t" + Material.Type;
			}
			return description;
		}
	}

	public abstract class Material
	{
		public virtual string Type { get; }
	}

	public class Wood : Material
	{
		public override string Type {
			get
			{
				return "Wood";
			}
		}
	}

	public class Metal : Material
	{
		public override string Type {
			get
			{
				return "Metal";
			}
		}
	}
}
```

### Critères de validation

* Les fichiers .sln, .csproj ainsi le code source doivent être hébergés sur GitHub
* La classe `Factory` est toujours scellée
* Un matériau (`Wood` , `Metal`) n'hérite plus de la classe Material
* Un matériau implémente l'interface `Craftable` qui déclare une seule propriété `Type`
* Il est possible d'accéder à `Craftable.Type` en *lecture* uniquement
* La classe `Factory` a une seule méthode `Craft`
* Ta fonction `Main` doit être exactement celle ci-dessous:
```C#
   public static void Main()
   {
			Factory factory = new Factory();

			IEnumerable<Craftable> craftables = new List<Craftable>
			{
				new Wood(),
				new Metal()
			};

			Production production = factory.Craft(craftables);

			Console.WriteLine(production);
   }
```
* L'objet de bois et de métal est affiché à l'écran
* Chaque classe est dans un fichier séparé
