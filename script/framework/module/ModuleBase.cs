using Framework.Data;
using System;

namespace Framework.Module {
    public abstract class ModuleBase<T> : IModule where T :CacheData,new() {
  
        public  T data {
            get {
             return   DataCenter.instace.getData<T>();
            }
        }



        public virtual void onStart() {

        }

        public virtual void onUpdate(float dt) {

        }

    }
}