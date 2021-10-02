
using UnityEngine;
using TMPro;

namespace TopDownShooter.UI{

    public class TextDisplay : MonoBehaviour{
        TextMeshProUGUI  textObj;

        private void Awake() {
            textObj = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text){
            //if (textObj == null) return;
            textObj.text = text;
        }
    }
    
}