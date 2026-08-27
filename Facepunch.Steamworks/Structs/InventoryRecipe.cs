using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// One craftable combination: a set of ingredients Steam will accept in exchange for a
	/// particular item. Recipes are declared in the item schema on the Steamworks partner site and
	/// parsed out of it here, so this is a read-only description of what is possible — use it to
	/// build a crafting UI and to work out what to pass to <c>SteamInventory.CraftItemAsync</c>.
	/// </summary>
	/// <remarks>
	/// Obtained from <see cref="InventoryDef.GetRecipes"/> (ways to make an item) or
	/// <see cref="InventoryDef.GetRecipesContainingThis"/> (things an item can be used for).
	/// Recipes are evaluated server-side and atomically: if what you submit does not match one of
	/// these exactly, the exchange fails and nothing is consumed.
	/// </remarks>
	/// <example>
	/// <code>
	/// foreach ( var recipe in tableDef.GetRecipes() ?? Array.Empty&lt;InventoryRecipe&gt;() )
	/// {
	///     var parts = recipe.Ingredients
	///         .Select( i =&gt; $"{i.Count}x {i.Definition?.Name ?? i.DefinitionId.ToString()}" );
	///
	///     Console.WriteLine( $"{recipe.Result.Name} = {string.Join( " + ", parts )}" );
	/// }
	/// </code>
	/// </example>
	public struct InventoryRecipe : IEquatable<InventoryRecipe>
	{
		/// <summary>
		/// A single input to a recipe: which item type is required, and how many of it.
		/// </summary>
		public struct Ingredient
		{
			/// <summary>
			/// The definition ID of the ingredient.
			/// </summary>
			public int DefinitionId;

			/// <summary>
			/// If we don't know about this item definition this might be null.
			/// In which case, DefinitionId should still hold the correct id.
			/// </summary>
			public InventoryDef Definition;

			/// <summary>
			/// The amount of this item needed. Generally this will be 1.
			/// </summary>
			public int Count;

			internal static Ingredient FromString( string part )
			{
				var i = new Ingredient();
				i.Count = 1;

				try
				{
					if ( part.Contains( "x" ) )
					{
						var idx = part.IndexOf( 'x' );

						int count = 0;
						if ( int.TryParse( part.Substring( idx + 1 ), out count ) )
							i.Count = count;

						part = part.Substring( 0, idx );
					}

					i.DefinitionId = int.Parse( part );
					i.Definition = SteamInventory.FindDefinition( i.DefinitionId );

				}
				catch ( System.Exception )
				{
					return i;
				}

				return i;
			}
		}

		/// <summary>
		/// The item that this will create.
		/// </summary>
		public InventoryDef Result;

		/// <summary>
		/// The items, with quantity required to create this item.
		/// </summary>
		public Ingredient[] Ingredients;

		/// <summary>
		/// The raw schema fragment this recipe was parsed from, kept verbatim. Useful for logging
		/// and for diagnosing a recipe that did not parse the way you expected; it is Valve's
		/// encoding, so do not build logic on its format.
		/// </summary>
		/// <remarks>
		/// This doubles as the recipe's identity — equality and hashing are both derived from it,
		/// so a recipe with a null <see cref="Source"/> (a default-constructed value that never
		/// came from the schema) throws when compared or hashed.
		/// </remarks>
		public string Source;

		internal static InventoryRecipe FromString( string part, InventoryDef Result )
		{
			var r = new InventoryRecipe
			{
				Result = Result,
				Source = part
			};

			var parts = part.Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries );

			r.Ingredients = parts.Select( x => Ingredient.FromString( x ) ).Where( x => x.DefinitionId != 0 ).ToArray();
			return r;
		}

		internal bool ContainsIngredient( InventoryDef inventoryDef )
		{
			return Ingredients.Any( x => x.DefinitionId == inventoryDef.Id );
		}

		/// <summary>
		/// Compares two recipes by their <see cref="Source"/> text.
		/// </summary>
		/// <param name="a">The first recipe.</param>
		/// <param name="b">The second recipe.</param>
		/// <returns><c>true</c> if the two recipes are considered the same.</returns>
		/// <remarks>
		/// Compares hash codes rather than the strings themselves, so two different recipes whose
		/// source text happens to collide will compare equal. Compare <see cref="Source"/> directly
		/// if you need certainty. Throws if either side has a null <see cref="Source"/>.
		/// </remarks>
		public static bool operator ==( InventoryRecipe a, InventoryRecipe b ) => a.GetHashCode() == b.GetHashCode();

		/// <summary>
		/// The inverse of the equality operator.
		/// </summary>
		/// <param name="a">The first recipe.</param>
		/// <param name="b">The second recipe.</param>
		/// <returns><c>true</c> if the two recipes are considered different.</returns>
		public static bool operator !=( InventoryRecipe a, InventoryRecipe b ) => a.GetHashCode() != b.GetHashCode();

		/// <summary>
		/// Compares this recipe to an arbitrary object.
		/// </summary>
		/// <param name="p">The object to compare against.</param>
		/// <returns><c>true</c> if it is a recipe considered the same as this one.</returns>
		/// <remarks>
		/// Casts rather than type-testing, so anything that is not an
		/// <see cref="InventoryRecipe"/> throws instead of returning <c>false</c>.
		/// </remarks>
		public override bool Equals( object p ) => this.Equals( (InventoryRecipe)p );

		/// <summary>
		/// Hash code derived from the raw <see cref="Source"/> text.
		/// </summary>
		/// <returns>The hash code of <see cref="Source"/>.</returns>
		/// <remarks>
		/// Throws <see cref="NullReferenceException"/> on a default-constructed recipe, because
		/// such a value has no <see cref="Source"/>. Only recipes obtained from
		/// <see cref="InventoryDef.GetRecipes"/> are safe to hash or put in a collection.
		/// </remarks>
		public override int GetHashCode()
		{
			return Source.GetHashCode();
		}

		/// <summary>
		/// Compares this recipe to another.
		/// </summary>
		/// <param name="p">The recipe to compare against.</param>
		/// <returns><c>true</c> if the two recipes are considered the same.</returns>
		/// <remarks>
		/// Compares hash codes, not the underlying <see cref="Source"/> strings, so a collision
		/// reports a false match.
		/// </remarks>
		public bool Equals( InventoryRecipe p ) => p.GetHashCode() == GetHashCode();
	}
}