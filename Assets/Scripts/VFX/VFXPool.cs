using CosmicCuration.Utilities;
using CosmicCuration.VFX;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.VFX
{
    public class VFXPool : GenericObjectPool<VFXController>
    {
        private VFXView vxfView;

        public VFXPool(VFXView _vxfView)
        {
            this.vxfView = _vxfView;
        }
        public VFXController GetVFX()=>GetItem<VFXController>();

        protected override VFXController CreateItem<U>() => new VFXController(vxfView);
        

    }
}