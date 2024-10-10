using Framework.Data;
using System;
using System.Collections.Generic;

namespace Framework.Module {
    public class ModuleManage {

        private static ModuleManage _instance;

        private Dictionary<Type, IModule> moduleDictionary = new Dictionary<Type, IModule>();
        private List<IModule> modules = new List<IModule>();
        public static ModuleManage instace {
            get {
                if (_instance == null) {
                    _instance = new ModuleManage();
                }
                return _instance;
            }
        }

        public void register<M, S>() where S : CacheData,new () where M : ModuleBase<S>, new() {
            registerModule<M, S>();
        }

        private void registerModule<M, S>() where S : CacheData, new() where M : ModuleBase<S>, new() {
            if (moduleDictionary.ContainsKey(typeof(M))) return;
            M module = new M();
            moduleDictionary.Add(typeof(M), module);
            modules.Add(module);
            module.onStart();
        }

        public M getModule<M>() where M : IModule {
            if (moduleDictionary.ContainsKey(typeof(M))) {
                return (M)moduleDictionary[typeof(M)];
            } else {
                return default(M);
            }
        }


        public void onUpdate(float dt) {
            modules.ForEach(module => {
                module.onUpdate(dt);
            });
        }
    }
}