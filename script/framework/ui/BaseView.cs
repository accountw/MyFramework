using Framework.Tool;
using UnityEngine;

namespace Framework.UI
{
    public abstract class BaseView
    {
        private GameObject _uiObject;
        private Canvas _rootCanvas;

        //是否可见
        private bool _visible;

        //UI层级
        protected UILayer _layer = UILayer.Bottom;

        public bool visible
        {
            get { return _visible; }
            private set
            {
                _visible = value;
                if (_visible)
                {
                    UIManager.instance.addChild(this);
                    rootCanvas.gameObject.SetActive(true);
                    onShow();
                }
                else
                {
                    UIManager.instance.removeChild(this);
                    rootCanvas.gameObject.SetActive(false);
                    onHide();
                }
            }
        }

        private void start()
        {
            load();
            Debug.Log("BaseView====================start");
            onStart();
        }


        public void show()
        {
            Debug.Log("BaseView====================show");
            visible = true;
        }

        public void hide()
        {
            Debug.Log("BaseView====================hide");
            visible = false;
        }


        public UILayer layer
        {
            get => _layer;
            set { _layer = value; }
        }

        public Canvas rootCanvas
        {
            get => _rootCanvas;
            set => _rootCanvas = value;
        }

        public GameObject uiObject
        {
            get => _uiObject;
            set => _uiObject = value;
        }

        public T findElement<T>(string name)
        {
            Transform[] array = rootCanvas.transform.GetComponentsInChildren<Transform>();
            T element = default(T);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].name.Equals(name))
                {
                    element = array[i].GetComponent<T>();
                }
            }

            return element;
        }

        public void destroy()
        {
            onDestroy();
            GameObject.Destroy(uiObject);
        }

        protected abstract void load();

        protected virtual void onShow()
        {
        }

        protected virtual void onHide()
        {
        }

        protected virtual void onStart()
        {
        }

        protected virtual void onDestroy()
        {
        }

        public virtual void onUpdate(float dt)
        {
        }
    }
}