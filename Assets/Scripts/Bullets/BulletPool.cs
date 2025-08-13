using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Bullets
{
    public class BulletPool
    {
        private BulletView _bulletView;
        private BulletScriptableObject _bulletScriptableObject;
        private List<PooledBullet> pooledBullets = new List<PooledBullet>();

        public BulletPool(BulletView bulletView,BulletScriptableObject bulletScriptableObject)
        {
            this._bulletView = bulletView;
            this._bulletScriptableObject = bulletScriptableObject;
        }
        public BulletController GetBullet()
        {
            if(pooledBullets.Count>0)
            {
                PooledBullet pooledBullet = pooledBullets.Find(item => !item.isUsed);
                if(pooledBullet != null)
                {
                    pooledBullet.isUsed = true;
                    return pooledBullet.bulletController;
                }
            }
            return CreateNewPooledBullet();
        }
        public void ReturnToPool(BulletController returnedBullet)
        {
            PooledBullet pooledBullet = pooledBullets.Find(item=> item.bulletController.Equals(returnedBullet));
            pooledBullet.isUsed=false;
        }
        private BulletController CreateNewPooledBullet()
        {
            PooledBullet pooledBullet = new PooledBullet();
            pooledBullet.bulletController = new BulletController(_bulletView,_bulletScriptableObject);
            pooledBullet.isUsed=true;
            pooledBullets.Add(pooledBullet);

            return pooledBullet.bulletController;

        }


        public class PooledBullet
        {
          public BulletController bulletController;
          public bool isUsed;
        }

    }
}