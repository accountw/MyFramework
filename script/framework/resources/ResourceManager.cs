
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Resources {

    public class ResourceManager {

        /*--------单例模式-------------*/
        private static ResourceManager _instance;
        public static ResourceManager instace {
            get {
                if (_instance == null) {
                    _instance = new ResourceManager();
                }
                return _instance;
            }
        }

        /*使用字典保存需要加载的物体*/
        Dictionary<string, object> res = new Dictionary<string, object>();
        //T为加载物体的游戏数据类型
        public T load<T>(string resPath) where T : Object {
            if (res.ContainsKey(resPath)) {
                return res as T;
            }
            T t = UnityEngine.Resources.Load<T>(resPath);
            res[resPath] = t;
            Debug.Log("****** ResourceManager Load [" + resPath + "] ******");
            return t;


        }

        public T spawnGameObject<T>(string resPath) where T : Object {
            T gameObject = load<T>(resPath);
            return GameObject.Instantiate(gameObject);
        }
    }

}