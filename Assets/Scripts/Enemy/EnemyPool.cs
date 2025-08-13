using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    { 
        private EnemyView enemyView;
        private EnemyData enemyData;
        public List<PooledEnemy> pooledEnemys = new List<PooledEnemy>();
        public EnemyPool(EnemyView _enemyView, EnemyData _enemyData)
        { 
            this.enemyView = _enemyView;
            this.enemyData = _enemyData;
        }
        public EnemyController GetEnemy()
        {
            if(pooledEnemys.Count > 0)
            {
                PooledEnemy pooledEnemy = pooledEnemys.Find(item=>!item.isUsed);
                if(pooledEnemy != null)
                {
                    pooledEnemy.isUsed = true;
                    return pooledEnemy.enemyController;
                }
            }
            return CreateEnemyToPool();
        }
        public void RetunEnemy(EnemyController returnedEnemy)
        {
            PooledEnemy pooledEnemy = pooledEnemys.Find(items => items.enemyController.Equals(returnedEnemy));
            pooledEnemy.isUsed = false;
        }
        private EnemyController CreateEnemyToPool()
        {
            PooledEnemy pooledEnemy = new PooledEnemy();
            pooledEnemy.enemyController = new EnemyController(enemyView,enemyData); 
            pooledEnemy.isUsed=true;
            pooledEnemys.Add(pooledEnemy);

            return pooledEnemy.enemyController ;
        }

        public class PooledEnemy 
        { 
            public EnemyController enemyController;
            public bool isUsed;
        
        }

    }
}