//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace InMemoryDatabase
{
	public class Table
	{
		#region Public Properties
		public Type SetType;
		#endregion

		#region Private Properties
		private List<TableEntry> _entries = new List<TableEntry>();
		#endregion

		#region Public Methods
		public Table(Type setType)
		{
			SetType = setType;
		}

		public Guid Insert<T>(T entry) where T : TableEntry
		{
			entry.Id = new Guid();
			_entries.Add(entry);
			return entry.Id;
		}

		public List<TableEntry> GetAll()
		{
			return _entries;
		}

		public List<T> GetAll<T>()
		{
			return _entries.Cast<T>().ToList();
		}

		public bool Get<T>(Guid id, out T entry) where T : TableEntry
		{
			bool result = false;
			int count = _entries.Count;
			entry = default(T);


			for (int i = 0; i < count; i++)
			{
				if (_entries[i].Id == id)
				{
					entry = _entries[i] as T;
					result = true;
					break;
				}
			}
			return result;
		}

		public bool FindAll<T>(Predicate<T> predicate, out List<T> entries) where T : TableEntry
		{
			entries = new List<T>();
			if (_entries.Count > 0)
			{
				entries = GetAll<T>().FindAll(predicate);
			}
			return entries.Count > 0;
		}

		public bool Find<T>(Predicate<T> predicate, out T entry) where T : TableEntry
		{
			bool result = FindAll<T>(predicate, out List<T> entries);
			entry = entries.FirstOrDefault();
			return result;
		}

		public bool Replace<T>(T entry) where T : TableEntry
		{
			bool result = false;
			int count = _entries.Count;

			for (int i = 0; i < count; i++)
			{
				if (_entries[i].Id == entry.Id)
				{
					_entries[i] = entry;
					result = true;
					break;
				}
			}

			return result;
		}

		internal Guid ReplaceOrInsert<T>(T entry) where T : TableEntry
		{
			Guid guid = entry.Id;
			if (!Replace(entry))
			{
				guid = Insert(entry);
			}

			return guid;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}