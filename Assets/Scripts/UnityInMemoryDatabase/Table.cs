using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Jwho303.InMemoryDatabase
{
	public class Table
	{
		#region Public Properties
		public Type SetType;
		#endregion

		#region Private Properties
		private List<TableEntry> _entries = new List<TableEntry>();
		private Action _entryChangeSubscriptions = delegate { };
		#endregion

		#region Public Methods
		public Table(Type setType)
		{
			SetType = setType;
		}

		public T Insert<T>(T entry) where T : TableEntry
		{
			entry.Id = Guid.NewGuid();
			_entries.Add(entry);
			_entryChangeSubscriptions();
			return entry;
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
			_entryChangeSubscriptions();
			return result;
		}

		internal T ReplaceOrInsert<T>(T insertEntry) where T : TableEntry
		{
			T modifedEntry = insertEntry;
			if (!Replace(insertEntry))
			{
				modifedEntry = Insert(insertEntry);
			}

			return modifedEntry;
		}
		internal bool Remove<T>(T entry) where T : TableEntry
		{
			bool result = _entries.Remove(entry);
			_entryChangeSubscriptions();
			return result;
		}

		public void Subscribe(Action action)
		{
			_entryChangeSubscriptions += action;
		}

		public void Unsubscribe(Action action)
		{
			_entryChangeSubscriptions -= action;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}