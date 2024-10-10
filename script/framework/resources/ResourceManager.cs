
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Resources {

    public class ResourceManager {

        /*--------����ģʽ-------------*/
        private static ResourceManager _instance;
        public static ResourceManager instace {
            get {
                if (_instance == null) {
                    _instance = new ResourceManager();
                }
                return _instance;
            }
        }

        /*ʹ���ֵ䱣����Ҫ���ص�����*/
        Dictionary<string, object> res = new Dictionary<string, object>();
        //TΪ�����������Ϸ��������
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