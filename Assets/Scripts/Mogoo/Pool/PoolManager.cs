using System.Collections.Generic;
using Mogoo.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Mogoo.Pool
{
    public class PoolData
    {
        private readonly GameObject _parent;
        private readonly List<GameObject> _poolList;

        public PoolData(GameObject gameObject, GameObject poolObject)
        {
            _parent = new GameObject(gameObject.name);
            _parent.transform.parent = poolObject.transform;
            _poolList = new List<GameObject>(){};
            PushObj(gameObject);
        }
        
        public GameObject GetObj()
        {
            var gameObject = _poolList[0];
            _poolList.RemoveAt(0);
            gameObject.SetActive(true);
            gameObject.transform.parent = null;
            return gameObject;
        }
        
        public void PushObj(GameObject gameObject)
        {
            _poolList.Add(gameObject);
            gameObject.transform.parent = _parent.transform;
            gameObject.SetActive(false);
        }

        public int GetCount()
        {
            return _poolList.Count;
        }
    }

    public class PoolManager : Singleton<PoolManager>
    {
        private readonly Dictionary<string, PoolData> _pools = new Dictionary<string, PoolData>();

        private GameObject _poolObject;

        /// <summary>
        /// 产生
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public void Spawn(string name, UnityAction<GameObject> callback)
        {
            if (_pools.ContainsKey(name) && _pools[name].GetCount() > 0)
            {
                callback(_pools[name].GetObj());
            }
            else
            {
                ResManager.Instance.LoadAsync(name, (GameObject obj) =>
                {
                    obj.name = name;
                    callback(obj);
                });
            }
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gameObject"></param>
        public void Despawn(string name, GameObject gameObject)
        {
            if (_poolObject == null) _poolObject = new GameObject("PoolManager");

            if (_pools.ContainsKey(name))
            {
                _pools[name].PushObj(gameObject);
            }
            else
            {
                _pools.Add(name, new PoolData(gameObject, _poolObject));
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _pools.Clear();
        }
    }
}

