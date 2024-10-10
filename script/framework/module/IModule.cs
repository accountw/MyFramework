using System.Collections;
using UnityEngine;

namespace Framework.Module {
    public interface IModule {


        void onStart();

        void onUpdate(float dt);
    }
}