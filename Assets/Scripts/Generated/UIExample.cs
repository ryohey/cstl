using UnityEngine;
using UnityEngine.UI;

namespace CastleGenerated
{
    [ExecuteAlways]
    public class UIExample: CastleMonoBehaviour
    {
        [ExecuteAlways]
        public class MonoBehaviour_UIExample_0: CastleMonoBehaviour
        {
            
            
            private  RectTransform __Prop0;
            private  CanvasRenderer __Prop1;
            private  Text __Prop2;
            public override string TemplatePath
            {
                get
                {
                    return "Assets/StreamingAssets/Components/UIExample.cstl";
                }
            }
            
            public override void SetupComponents ()
            {
                __Prop0 = gameObject.GetComponent<RectTransform>();
                __Prop0 = __Prop0 != null ? __Prop0 : gameObject.AddComponent<RectTransform>();
                
                __Prop1 = gameObject.GetComponent<CanvasRenderer>();
                __Prop1 = __Prop1 != null ? __Prop1 : gameObject.AddComponent<CanvasRenderer>();
                
                __Prop2 = gameObject.GetComponent<Text>();
                __Prop2 = __Prop2 != null ? __Prop2 : gameObject.AddComponent<Text>();
                __Prop2.text = "Hello world!";
            }
            
            public  void Awake ()
            {
                SetupComponents();
            }
        }
        
        [ExecuteAlways]
        public class MonoBehaviour_UIExample_1: CastleMonoBehaviour
        {
            
            
            private  RectTransform __Prop0;
            private  CanvasRenderer __Prop1;
            private  Text __Prop2;
            public override string TemplatePath
            {
                get
                {
                    return "Assets/StreamingAssets/Components/UIExample.cstl";
                }
            }
            
            public override void SetupComponents ()
            {
                __Prop0 = gameObject.GetComponent<RectTransform>();
                __Prop0 = __Prop0 != null ? __Prop0 : gameObject.AddComponent<RectTransform>();
                
                __Prop1 = gameObject.GetComponent<CanvasRenderer>();
                __Prop1 = __Prop1 != null ? __Prop1 : gameObject.AddComponent<CanvasRenderer>();
                
                __Prop2 = gameObject.GetComponent<Text>();
                __Prop2 = __Prop2 != null ? __Prop2 : gameObject.AddComponent<Text>();
                __Prop2.text = "Yes, another child!";
            }
            
            public  void Awake ()
            {
                SetupComponents();
            }
        }
        
        [ExecuteAlways]
        public class MonoBehaviour_UIExample_2: CastleMonoBehaviour
        {
            [ExecuteAlways]
            public class MonoBehaviour_MonoBehaviour_UIExample_2_0: CastleMonoBehaviour
            {
                
                
                private  RectTransform __Prop0;
                private  CanvasRenderer __Prop1;
                private  Text __Prop2;
                public override string TemplatePath
                {
                    get
                    {
                        return "Assets/StreamingAssets/Components/UIExample.cstl";
                    }
                }
                
                public override void SetupComponents ()
                {
                    __Prop0 = gameObject.GetComponent<RectTransform>();
                    __Prop0 = __Prop0 != null ? __Prop0 : gameObject.AddComponent<RectTransform>();
                    
                    __Prop1 = gameObject.GetComponent<CanvasRenderer>();
                    __Prop1 = __Prop1 != null ? __Prop1 : gameObject.AddComponent<CanvasRenderer>();
                    
                    __Prop2 = gameObject.GetComponent<Text>();
                    __Prop2 = __Prop2 != null ? __Prop2 : gameObject.AddComponent<Text>();
                    __Prop2.text = "This is a button";
                }
                
                public  void Awake ()
                {
                    SetupComponents();
                }
            }
            
            private  RectTransform __Prop0;
            private  CanvasRenderer __Prop1;
            private  Image __Prop2;
            private  Button __Prop3;
            private  MonoBehaviour_MonoBehaviour_UIExample_2_0 __Prop4;
            public override string TemplatePath
            {
                get
                {
                    return "Assets/StreamingAssets/Components/UIExample.cstl";
                }
            }
            
            public override void SetupComponents ()
            {
                __Prop0 = gameObject.GetComponent<RectTransform>();
                __Prop0 = __Prop0 != null ? __Prop0 : gameObject.AddComponent<RectTransform>();
                __Prop0.sizeDelta = new Vector2(160, 30);
                __Prop1 = gameObject.GetComponent<CanvasRenderer>();
                __Prop1 = __Prop1 != null ? __Prop1 : gameObject.AddComponent<CanvasRenderer>();
                
                __Prop2 = gameObject.GetComponent<Image>();
                __Prop2 = __Prop2 != null ? __Prop2 : gameObject.AddComponent<Image>();
                
                __Prop3 = gameObject.GetComponent<Button>();
                __Prop3 = __Prop3 != null ? __Prop3 : gameObject.AddComponent<Button>();
                
                __Prop4 = gameObject.GetComponentInChildren<MonoBehaviour_MonoBehaviour_UIExample_2_0>();
                __Prop4 = __Prop4 != null ? __Prop4 : Instantiate(new GameObject(), transform).AddComponent<MonoBehaviour_MonoBehaviour_UIExample_2_0>();
                __Prop4.name = "Label";
            }
            
            public  void Awake ()
            {
                SetupComponents();
            }
        }
        
        private  RectTransform __Prop0;
        private  Canvas __Prop1;
        private  GraphicRaycaster __Prop2;
        private  CanvasScaler __Prop3;
        private  MonoBehaviour_UIExample_0 __Prop4;
        private  MonoBehaviour_UIExample_1 __Prop5;
        private  MonoBehaviour_UIExample_2 __Prop6;
        public override string TemplatePath
        {
            get
            {
                return "Assets/StreamingAssets/Components/UIExample.cstl";
            }
        }
        
        public override void SetupComponents ()
        {
            __Prop0 = gameObject.GetComponent<RectTransform>();
            __Prop0 = __Prop0 != null ? __Prop0 : gameObject.AddComponent<RectTransform>();
            
            __Prop1 = gameObject.GetComponent<Canvas>();
            __Prop1 = __Prop1 != null ? __Prop1 : gameObject.AddComponent<Canvas>();
            
            __Prop2 = gameObject.GetComponent<GraphicRaycaster>();
            __Prop2 = __Prop2 != null ? __Prop2 : gameObject.AddComponent<GraphicRaycaster>();
            
            __Prop3 = gameObject.GetComponent<CanvasScaler>();
            __Prop3 = __Prop3 != null ? __Prop3 : gameObject.AddComponent<CanvasScaler>();
            
            __Prop4 = gameObject.GetComponentInChildren<MonoBehaviour_UIExample_0>();
            __Prop4 = __Prop4 != null ? __Prop4 : Instantiate(new GameObject(), transform).AddComponent<MonoBehaviour_UIExample_0>();
            __Prop4.name = "Text Label";
            __Prop5 = gameObject.GetComponentInChildren<MonoBehaviour_UIExample_1>();
            __Prop5 = __Prop5 != null ? __Prop5 : Instantiate(new GameObject(), transform).AddComponent<MonoBehaviour_UIExample_1>();
            __Prop5.name = "Greetings";
            __Prop6 = gameObject.GetComponentInChildren<MonoBehaviour_UIExample_2>();
            __Prop6 = __Prop6 != null ? __Prop6 : Instantiate(new GameObject(), transform).AddComponent<MonoBehaviour_UIExample_2>();
            __Prop6.name = "Button";
        }
        
        public  void Awake ()
        {
            SetupComponents();
        }
    }
}