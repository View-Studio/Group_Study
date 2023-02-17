using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using Object = UnityEngine.Object;

namespace TutorialCommends
{
    public class TutorialComposite : ITutorial
    {
        List<ITutorial> _commends = new List<ITutorial>();
        public void AddCommend(ITutorial tutorial) => _commends.Add(tutorial);
        public void TutorialAction() => _commends.ForEach(x => x.TutorialAction());
        public bool EndCondition() => _commends.All(x => x.EndCondition());
        public void EndAction() => _commends.ForEach(x => x.EndAction());
    }

    public class ReadTextCommend : ITutorial
    {
        string _text;
        UI_PopupText _textUI;
        public ReadTextCommend(string text) => _text = text;

        public void TutorialAction()
        {
            _textUI = Managers.UI.ShowDefualtUI<UI_PopupText>();
            _textUI.SetPosition(new Vector2(0, 110));
            _textUI.Show(_text, Color.white);
            _textUI.GetComponent<Canvas>().sortingOrder = 3333; // 그냥 제일 높게
        }
        public void EndAction() => Object.Destroy(_textUI.gameObject);
        public bool EndCondition() => Input.GetMouseButtonUp(0);
    }

    public class SpotLightCommend : ITutorial
    {
        protected Vector3 spotPos;
        float _range;
        Light _light;
        public SpotLightCommend(Vector3 lightPos, float range = 10f) => (spotPos, _range) = (lightPos, range);

        public virtual void TutorialAction()
        {
            _light = Object.Instantiate(Resources.Load<Light>("Tutorial/SpotLight"));
            _light.range = _range;
            _light.gameObject.SetActive(true);
            _light.transform.position = spotPos;
        }
        public void EndAction() => Object.Destroy(_light.gameObject);
        public bool EndCondition() => Input.GetMouseButtonUp(0);
    }

    public class SpotLightActionCommend : SpotLightCommend
    {
        Func<Vector3> _getPos;
        public SpotLightActionCommend(Func<Vector3> getPos) : base(Vector3.zero) => _getPos = getPos;
        public override void TutorialAction()
        {
            spotPos = _getPos();
            base.TutorialAction();
        }
    }

    public class Highlight_UICommend : ITutorial
    {
        string _uiName;
        RectTransform chaseUI = null;
        public Highlight_UICommend(string uiName) => _uiName = uiName;

        public void TutorialAction()
        {
            var showUITransform = GameObject.Find(_uiName).GetComponent<RectTransform>();
            if (showUITransform != null) 
                SetBlindUI(showUITransform);

            void SetBlindUI(RectTransform target)
            {
                chaseUI = Object.Instantiate(Resources.Load<RectTransform>("Tutorial/Chase UI"));
                chaseUI.parent = target;
                chaseUI.gameObject.SetActive(true);
                chaseUI.pivot = target.pivot;
                chaseUI.anchorMin = target.anchorMin;
                chaseUI.anchorMax = target.anchorMax;
                chaseUI.position = target.position;
                chaseUI.sizeDelta = target.sizeDelta;
                chaseUI.parent = GameObject.Find("ForwardCanvas").transform; // 가리개 맨 앞으로 이동
                chaseUI.localScale = Vector3.one;
            }
        }

        public bool EndCondition() => Input.GetMouseButtonUp(0);

        public void EndAction() => Object.Destroy(chaseUI.gameObject);
    }

    public class ButtonClickCommend : ITutorial
    {
        string _uiName;
        public ButtonClickCommend(string uiName) => _uiName = uiName;

        Button button;
        bool _isDone = false;
        void End() => _isDone = true;

        public void TutorialAction()
        {
            button = GameObject.Find(_uiName).GetComponent<Button>();
            button.enabled = true;
            button.onClick.AddListener(End);
        }
        public bool EndCondition() => _isDone;
        public void EndAction() => button.onClick.RemoveListener(End);
    }

    public class ActionCommend : ITutorial
    {
        Action _tutorialAction;
        Func<bool> _endCondtion;
        Action _endActoin;
        public ActionCommend(Action tutorialAction, Func<bool> endCondtion = null, Action endActoin = null)
        {
            _tutorialAction = tutorialAction;
            _endCondtion = endCondtion;
            _endActoin = endActoin;
        }

        public void TutorialAction() => _tutorialAction?.Invoke();
        public bool EndCondition()
        {
            if (_endCondtion == null) return true;
            else return _endCondtion();
        }
        public void EndAction() => _endActoin?.Invoke();
    }
}