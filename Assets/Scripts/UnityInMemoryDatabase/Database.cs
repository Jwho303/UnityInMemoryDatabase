//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace InMemoryDatabase
{
    public abstract class Database
    {
		#region Public Properties

		#endregion

		#region Private Properties
		protected Dictionary<string, Table> _database = new Dictionary<string, Table>();
		#endregion

		#region Public Methods
		public abstract void Initialize();

		public void CreateTable<T>() where T : TableEntry
		{
			Type t = typeof(T);
			if (!_database.ContainsKey(t.FullName))
			{
				Table set = new Table(t);
				_database.Add(t.FullName, set);
			}
			else
			{
				Debug.LogError($"[{this.GetType().Name}] Table ({t.FullName}) already exists!");
			}
		}

		public List<T> GetAll<T>() where T : TableEntry
		{
			Type t = typeof(T);
			return _database[t.FullName].GetAll<T>();
		}

		public T Insert<T>(T entry) where T : TableEntry
		{
			Type t = typeof(T);
			return _database[t.FullName].Insert(entry);
		}

		public bool Get<T>(Guid id, out T entry) where T : TableEntry
		{
			Type t = typeof(T);
			return _database[t.FullName].Get<T>(id, out entry);
		}

		public bool FindAll<T>(System.Predicate<T> predicate, out List<T> entries) where T : TableEntry
		{
			Type t = typeof(T);
			return _database[t.FullName].FindAll(predicate, out entries);
		}

		public bool Find<T>(System.Predicate<T> predicate, out T entry) where T : TableEntry
		{
			Type t = typeof(T);
			return _database[t.FullName].Find(predicate, out entry);
		}

		public bool Replace<T>(T entry) where T : TableEntry
		{
			Type t = typeof(T);
			return _database[t.FullName].Replace(entry);
		}
		internal T ReplaceOrInsert<T>(T entry) where T : TableEntry
		{
			Type t = typeof(T);
			return _database[t.FullName].ReplaceOrInsert(entry);
		}

		internal void PrintSnapShot()
		{
			string s = $"[{this.GetType().Name}]\n";

			foreach (string tableType in _database.Keys)
			{
				List<TableEntry> items = _database[tableType].GetAll();
				s += $"--{tableType} ({items.Count})--\n";
				for (int i = 0; i < items.Count; i++)
				{
					s += $"  {items[i].Print()}\n";
				}
			}

			Debug.Log(s);
		}

		internal virtual bool Save(string path)
		{
			bool result = false;

			string json = JsonUtility.ToJson(_database, true);

			try
			{
				File.WriteAllText(path, json);
				result = true;
			}
			catch(SystemException e)
			{
				Debug.LogError($"[{this.GetType().Name}] {e}");
			}

			return result;
		}

		internal virtual bool Load(string path)
		{
			bool result = false;

			try
			{
				string fileContents = File.ReadAllText(path);
				_database = JsonUtility.FromJson<Dictionary<string, Table>>(fileContents);
				result = true;
			}
			catch (SystemException e)
			{
				Debug.LogError($"[{this.GetType().Name}] {e}");
			}

			return result;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}