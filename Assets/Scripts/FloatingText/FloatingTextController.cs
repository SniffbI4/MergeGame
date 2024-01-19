using Scripts.Pool;
using UnityEngine;

namespace FloatingText
{
    public class FloatingTextController
    {
        private static FloatingText popupText;
        private static Canvas _canvas;
        private static Pool<FloatingText> _pool;

        public FloatingTextController(Canvas canvas)
        {
            if (!popupText)
                popupText = Resources.Load<FloatingText>("Prefabs/PopupParent");
            
            _canvas = canvas;

            _pool = new Pool<FloatingText>(10, popupText, true);
        }

        public FloatingText CreateFloatingText(string text, Transform location, bool needFollow = true)
        {
            FloatingText instance = _pool.GetObject();
            instance.SetText(text);

            instance.transform.SetParent(_canvas.transform, false);

            instance.SetPoint(location.position, needFollow);
            return instance;
        }

        public void CreateFloatingTextWithColor(string text, Transform location, Color color, bool needFollow = true)
        {
            FloatingText instance = CreateFloatingText(text, location, needFollow);
            instance.SetColor(color);
        }
    }
}