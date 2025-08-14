using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CosmicCuration.Utilities
{
    public class GenericObjectPool<T> where T : class
    {
        private List<PooledItems<T>> _pooledItems = new List<PooledItems<T>>();

        protected T GetItem()
        {
            if(_pooledItems.Count > 0)
            {
                PooledItems<T> pooledItem = _pooledItems.Find(item => !item.isUsed);
                if(pooledItem != null)
                {
                    pooledItem.isUsed = true;
                    return pooledItem.Items;
                }
            }
            return CreateNewPooledItems();
        }
        private T CreateNewPooledItems()
        {
            PooledItems<T> newItems = new PooledItems<T>();
            newItems.Items = CreatItem();
            newItems.isUsed = true;
            _pooledItems.Add(newItems);
            return newItems.Items;
        }
       protected virtual T CreatItem()
        {
            // Beacuse Generic Object pool Cant create new object because it dont know whats its type 
            throw new NotImplementedException("Child Class Dont Have Implemented Created CreatItem() method");
        }

        public void ReturnToPool(T returnedItem)
        {
            PooledItems<T> pool = _pooledItems.Find(i=>i.Items.Equals(returnedItem));
            pool.isUsed = false;
        }
        

        public class PooledItems<T>
        {
            public T Items;
            public bool isUsed;
        }
    }
}