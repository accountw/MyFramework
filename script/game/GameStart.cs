
using Framework.UI;
using UnityEngine;
using Framework.Module;
using System;

namespace Assets.script {
    public class GameStart : MonoBehaviour {

        UIPlay uiplay ;

        void Start() {
            uiplay= UIManager.instance.show<UIPlay>();
        }

        void Update() {
            ModuleManage.instace.onUpdate(Time.deltaTime);
            calculateFps();
            gameObject.GetComponent<CharacterController>().Move(new Vector3(1,0,0));
        }

       

        private double totalTime=0;
        private double count = 0;
        private void calculateFps() {
            count++;
            totalTime += Time.deltaTime;
            if(totalTime > 1) {
                if (uiplay != null) {
                    uiplay.fpsText.text = count.ToString();
                }
                totalTime = 0;
                count = 0;
          
           }
        }
    }
}
