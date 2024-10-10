using Framework.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Data {
    public class DataCenter{

        private static DataCenter _instance;


        private Dictionary<Type, CacheData> playerDictionary = new Dictionary<Type, CacheData>();

        public static DataCenter instace {
            get {
                if (_instance == null) {
                    _instance = new DataCenter();
                }
                return _instance;
            }
        }


        public T getData<T>() where T:CacheData,new () {
            if (playerDictionary.ContainsKey(typeof(T))) {
                return (T)playerDictionary[typeof(T)];
            } else {
                T data = new T();
                playerDictionary.Add(typeof(T), data);
                return data;
            }
        }

        public void save<T>(T data) where T : CacheData {
            playerDictionary.Add(typeof(T), data);
        }
    }
}