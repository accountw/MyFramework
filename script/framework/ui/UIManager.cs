using Framework.Resources;
using Framework.Tool;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace Framework.UI
{
    public class UIManager : MonoBehaviour
    {
        public static Dictionary<Type, string> pathMap = new Dictionary<Type, string>();

        //单例
        public static UIManager instance;
        public Canvas rootCanvas;

        //存放UIManager动态生成的ui
        public List<BaseView> panels = new List<BaseView>();

        //使用UIManager.show()的ui
        private Dictionary<Type, BaseView> uiDictionary = new Dictionary<Type, BaseView>();

        //图层的数据
        private Dictionary<UILayer, LayerItem> layerDictionary = new Dictionary<UILayer, LayerItem>();


        private void Awake()
        {
            instance = this;
            initLayerDictionary();
            DontDestroyOnLoad(gameObject);
            rootCanvas = gameObject.transform.Find("RootCanvas").GetComponent<Canvas>();
            Debug.Log("****** UIManager Init ******");
        }


        private void initLayerDictionary()
        {
            layerDictionary.Add(UILayer.Bottom, new LayerItem(0, 0));
            layerDictionary.Add(UILayer.Middle, new LayerItem(10000, 0));
            layerDictionary.Add(UILayer.Top, new LayerItem(20000, 0));
            layerDictionary.Add(UILayer.Dialog, new LayerItem(40000, 0));
        }


        public void addChild(BaseView panel)
        {
            if (panel == null) return;
            UILayer layer = panel.layer;
            if (panel.uiObject.transform.parent != rootCanvas.transform)
            {
                panel.uiObject.transform.SetParent(rootCanvas.transform);
            }

            UILayer beforeLayerType = getPanelLayer(panel); //获取以前的层

            //从以前的层删除
            if (beforeLayerType != default(UILayer) && beforeLayerType == layer)
            {
                removeChild(panel);
            }

            LayerItem currentLayerItem = layerDictionary[layer];
            if (!currentLayerItem.panels.Contains(panel))
            {
                currentLayerItem.panels.Add(panel);
            }

            currentLayerItem.z = currentLayerItem.z + 1;
            int z = currentLayerItem.startZ + currentLayerItem.z;
            Canvas canvas = panel.uiObject.GetComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = z;
        }


        public void removeChild(BaseView panel)
        {
            if (panel != null) return;
            if (layerDictionary[panel.layer].panels.Contains(panel))
            {
                panel.uiObject.transform.parent = null;
                LayerItem item = layerDictionary[panel.layer];
                int index = item.panels.IndexOf(panel);
                item.panels.RemoveAt(index);
                item.z--;
            }
        }

        //获取一个Panel所在的UI层
        private UILayer getPanelLayer(BaseView panel)
        {
            foreach (UILayer key in layerDictionary.Keys)
            {
                if (layerDictionary[key].panels.IndexOf(panel) > -1)
                {
                    return key;
                }
            }

            return default(UILayer);
        }

        public T create<T>() where T : BaseView, new()
        {
            T ui = new T();
            panels.Add(ui);
            Type type = ui.GetType();
            //判断是否在类上使用特性
            if (type.IsDefined(typeof(UIPath), true))
            {
                UIPath uiAttribute = (UIPath)type.GetCustomAttribute(typeof(UIPath), true);
                string path = uiAttribute.desc;
                GameObject uiObject = ResourceManager.instace.spawnGameObject<GameObject>(path);
                ui.uiObject = uiObject;
                ui.rootCanvas = uiObject.transform.Find("RootCanvas").GetComponent<Canvas>();
            }

            excuteFunc(type, "start", ui);
            return ui;
        }

        private void excuteFunc(Type type, string func, object o)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var load = type.GetMethod(func, flag);
            if (load == null && type.BaseType != null)
            {
                excuteFunc(type.BaseType, func, o);
            }
            else
            {
                load.Invoke(o, null);
            }
        }

        public T show<T>(object[] parameters = null) where T : BaseView, new()
        {
            T ui = null;
            if (uiDictionary.ContainsKey(typeof(T)))
            {
                ui = uiDictionary[typeof(T)] as T;
            }
            else
            {
                ui = create<T>();
                uiDictionary.Add(typeof(T), ui);
            }

            Type type = ui.GetType();
            excuteFunc(type, "show", ui);
            return ui;
        }

        public T hide<T>() where T : BaseView
        {
            T ui = null;
            if (uiDictionary.ContainsKey(typeof(T)))
            {
                ui = uiDictionary[typeof(T)] as T;
                Type type = ui.GetType();
                excuteFunc(type, "hide", ui);
            }

            return ui;
        }

        public void destroy<T>() where T : BaseView
        {
            T uiBase = null;
            if (uiDictionary.ContainsKey(typeof(T)))
            {
                uiBase = uiDictionary[typeof(T)] as T;
                uiBase.destroy();
            }
        }


        private void Update()
        {
            for (int i = 0; i < panels.Count; i++)
            {
                BaseView panel = panels[i];
                if (panel.visible)
                {
                    panels[i].onUpdate(Time.deltaTime);
                }
            }
        }
    }
}