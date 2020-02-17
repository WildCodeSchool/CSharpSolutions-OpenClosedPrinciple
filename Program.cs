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

namespace OpenClosedPrincipleSolution
{
	public class Program
	{
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
	}

	public sealed class Factory
	{
		public Production Craft(IEnumerable<Craftable> manyCraftables)
		{
			return new Production(manyCraftables);
		}
	}

	public class Production
	{
		public IEnumerable<Craftable> Craftables { get; set; }

		public Production(IEnumerable<Craftable> manyCraftables)
		{
			Craftables = manyCraftables;
		}

		public override string ToString()
		{
			string description = "Object is composed of\n:";
			foreach (Craftable Craftable in Craftables)
			{
				description += "\t" + Craftable.Type;
			}
			return description;
		}
	}

	public interface Craftable
	{
		string Type { get; }
	}

	public class Wood : Craftable
	{
		public string Type {
			get
			{
				return "Wood";
			}
		}
	}

	public class Metal : Craftable
	{
		public string Type {
			get
			{
				return "Metal";
			}
		}
	}
}
