

using Framework.Resources;
using Framework.UI;
using UnityEngine;

namespace Assets.script {
    public class UIPlay : UIPlay_Base {

      protected override void onStart() {
            //layer = UILayer.Middle;
            Debug.Log("UIPlay=================onStart");
            Debug.Log(button.transform.position);
            button.onClick.AddListener(onClick);

          
          
      
       }

       void onClick() {
            Debug.Log("++++++++++++++++++++++++++++click");
        }

    }
}

