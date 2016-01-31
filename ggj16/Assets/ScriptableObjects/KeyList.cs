using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class KeyList : ScriptableObject
{
    [System.Serializable]
    public class Key
    {
        public enum KeyID {A,B,C,D,E,F,G,H};
        static KeyCode[] KeyIdToKeyCode = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8 };

        public float TimeInMillisec;
        public KeyID KeyId;

        [HideInInspector]
        public KeyCode KeyCode
        {
            get
            {
                return KeyIdToKeyCode[(int)KeyId];
            }
        }

        [HideInInspector]
        public Image SliderMarkerImage;



        public bool Succeed {get; private set;}

        public Key()
        {
            Reset();
        }

        private void SetMarkerColor(Color color)
        {
            if (SliderMarkerImage)
                SliderMarkerImage.color = color;
        }

        public void Reset()
        {
            SetMarkerColor(new Color(0f, 0f, 1f));
            Succeed = false;
        }

        public void MarkAsSucceeed()
        {
            SetMarkerColor(new Color(0f, 1f, 0f));
            Succeed = true;
        }

        public bool CheckIfFailed()
        {
            if (!Succeed)
                SetMarkerColor(new Color(1f, 0f, 0f));

            return !Succeed;
        }
    }

    public Key[] Keys;
    public AudioClip AudioClip;
   

    [MenuItem("Assets/Create/KeyList")]
    public static void Create()
    {
        var item = ScriptableObject.CreateInstance<KeyList>();
        ProjectWindowUtil.CreateAsset(item, "NewKeyList.asset");
    }


    public void Reset()
    {
        foreach (Key key in Keys)
        {
            key.Reset();
        }
    }

}
