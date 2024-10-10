

using Framework.UI;
using TMPro;
using UnityEngine.UI;

namespace Assets.script {

    [UIPath("ui/UIPlay")]
    public class UIPlay_Base : BaseView {

        public Button button;

        public TextMeshProUGUI fpsText;
        protected override void load() {
            button = findElement<Button>("button");
            fpsText = findElement<TextMeshProUGUI>("fpsText");
        }


    }



}




