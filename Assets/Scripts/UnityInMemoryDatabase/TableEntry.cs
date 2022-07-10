//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RenderHeads.InMemoryDatabase
{
	public abstract class TableEntry
	{
		#region Public Properties
		public Guid Id { get; set; }
		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public virtual string Print()
		{
			return $"Id({Id})";
		}

		public void Save<T>(Database database) where T : TableEntry
		{
			database.ReplaceOrInsert<T>(this as T);
		}
		#endregion

		#region Private Methods

		#endregion
	}
}