using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

namespace Jwho303.InMemoryDatabase
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

		public Table GetTable<T>() where T : TableEntry
		{
			Type t = typeof(T);
			if (!_database.ContainsKey(t.FullName))
			{
				return (_database[t.FullName]);
			}
			else
			{
				Debug.LogError($"[{this.GetType().Name}] Table ({t.FullName}) already exists!");
			}

			return null;
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

		internal bool Remove<T>(T entry) where T : TableEntry
		{
			Type t = typeof(T);
			return _database[t.FullName].Remove(entry);
		}

		public List<T2> Join<T1, T2>(Func<T1, T2, bool> joinPredicate)
		where T1 : TableEntry
		where T2 : TableEntry
        {
            Type t1 = typeof(T1);
			List<T1> list1 = _database[t1.FullName].GetAll<T1>();

            Type t2 = typeof(T2);
            List<T2> list2 = _database[t2.FullName].GetAll<T2>();

            List<T2> result = (from obj1 in list1
                               from obj2 in list2
                               where obj2 is T2 && joinPredicate(obj1, (T2)obj2)
                               select (T2)obj2).ToList();
            return result;
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

		public void Subscribe<T>(Action action)
		{
			Type t = typeof(T);
			_database[t.FullName].Subscribe(action);
		}

		public void Unsubscribe<T>(Action action)
		{
			Type t = typeof(T);
			_database[t.FullName].Unsubscribe(action);
		}

		//TODO
		//internal virtual bool Save(string path)
		//{
		//	bool result = false;

		//	string json = JsonUtility.ToJson(_database, true);
		//	try
		//	{
		//		File.WriteAllText(path, json);
		//		result = true;
		//	}
		//	catch(SystemException e)
		//	{
		//		Debug.LogError($"[{this.GetType().Name}] {e}");
		//	}

		//	return result;
		//}

		//internal virtual bool Load(string path)
		//{
		//	bool result = false;

		//	try
		//	{
		//		string fileContents = File.ReadAllText(path);
		//		_database = JsonUtility.FromJson<Dictionary<string, Table>>(fileContents);
		//		result = true;
		//	}
		//	catch (SystemException e)
		//	{
		//		Debug.LogError($"[{this.GetType().Name}] {e}");
		//	}

		//	return result;
		//}
		#endregion

		#region Private Methods

		#endregion
	}
}