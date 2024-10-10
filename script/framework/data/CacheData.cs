using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Framework.Data {
    public abstract class CacheData {

        
        protected void save() {
            DataCenter.instace.save(this);
        }

        protected void  onDataInit() {

        }

        protected void initDefaultData() {
            

        }
    }
}