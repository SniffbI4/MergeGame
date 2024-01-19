using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Pool
{
    public class Pool<T> where T : MonoBehaviour, IPoolObject
    {
        private static Transform mainContainer;

        private readonly List<T> PrefabList;
        private readonly bool AutoExpand;

        private Transform PoolContainer;
        private List<T> _pool;

        public Pool(int count, T prefab, bool autoExpand)
        {
            PrefabList = new List<T> { prefab };
            AutoExpand = autoExpand;

            CreateContainer();
            CreatePool(count);
        }

        public Pool(int count, List<T> prefabs, bool autoExpand)
        {
            PrefabList = prefabs;
            AutoExpand = autoExpand;

            CreateContainer();
            CreatePool(count);
        }

        private void CreateContainer()
        {
            if (mainContainer == null)
            {
                GameObject goContainer = new GameObject("==Pool Container==");
                mainContainer = goContainer.transform;
            }
            string containerName = $"{typeof(T).Name}_{PrefabList[0].name}";
            PoolContainer = new GameObject(containerName).transform;
            PoolContainer.SetParent(mainContainer);
        }

        public T GetObject()
        {
            if (HasFreeElement(out var element))
            {
                _pool.Remove(element);
                return element;
            }

            if (AutoExpand)
                return CreateObject(true);

            throw new Exception($"There is no free element in pool of type {typeof(T)} or pool is not auto expand");
        }

        private bool HasFreeElement(out T element)
        {
            if (_pool.Count != 0)
            {
                element = _pool[0];
                element.gameObject.SetActive(true);
                return true;
            }
            element = null;
            return false;
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool preActivated = false)
        {
            T template = PrefabList.Count == 1 ? PrefabList[0] : GetRandomObject();
            T createdObject = UnityEngine.Object.Instantiate(template, PoolContainer);
            createdObject.gameObject.SetActive(preActivated);
            createdObject.name = createdObject.name + createdObject.GetInstanceID();
            createdObject.OnObjectNeededToDeactivate += OnObjectDeactivated;

            if (!preActivated)
                _pool.Add(createdObject);
            return createdObject;
        }

        private T GetRandomObject()
        {
            return PrefabList[UnityEngine.Random.Range(0, PrefabList.Count)];
        }

        private void OnObjectDeactivated(IPoolObject obj)
        {
            _pool.Add(obj as T);
            (obj as T).transform.SetParent(PoolContainer);
            obj.ResetBeforeBackToPool();
        }
    }
}