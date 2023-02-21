using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class WeekCardSwiping : MonoBehaviour
    {
        [SerializeField] private Color[] _colors;
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private ScrollRect _scrollView;
        [SerializeField] private GameObject _imageContent;
        [SerializeField] private float _minSpeedForStop;
        [SerializeField] private float _sharpness;

        private float _scrollPos = 0f;
        private float[] _pos;
        private float _time;
        private bool _runIt = false;
        private Button _takeTheBtn;
        private int _btnNumber;

        private void Update()
        {
            _pos = new float[_imageContent.transform.childCount];
            float distance = 1f / (_pos.Length - 1f);

            if (_runIt)
            {
                GecisiDuzenle(distance, _pos, _takeTheBtn);
                _time += Time.deltaTime;

                if (_time > 1f)
                {
                    _time = 0;
                    _runIt = false;
                }
            }

            for (int i = 0; i < _pos.Length; i++)
            {
                _pos[i] = distance * i;
            }

            if (!Input.GetMouseButton(0) && _scrollView.velocity.magnitude <= _minSpeedForStop)
            {
                for (int i = 0; i < _pos.Length; i++)
                {
                    if (_scrollbar.value < _pos[i] + (distance / 2) && _scrollbar.value > _pos[i] - (distance / 2))
                    {
                        _scrollbar.value = Mathf.Lerp(_scrollbar.value, _pos[i], _sharpness);
                    }
                }
            }

            for (int i = 0; i < _pos.Length; i++)
            {
                if (_scrollbar.value < _pos[i] + (distance / 2) && _scrollbar.value > _pos[i] - (distance / 2))
                {
                    //Debug.LogWarning("Current Selected Level" + i);
                    transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                    _imageContent.transform.GetChild(i).localScale = Vector2.Lerp(_imageContent.transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);
                    _imageContent.transform.GetChild(i).GetComponent<Image>().color = _colors[1];
                    for (int j = 0; j < _pos.Length; j++)
                    {
                        if (j != i)
                        {
                            _imageContent.transform.GetChild(j).GetComponent<Image>().color = _colors[0];
                            _imageContent.transform.GetChild(j).localScale = Vector2.Lerp(_imageContent.transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                            transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        }
                    }
                }
            }
        }

        private void GecisiDuzenle(float distance, float[] pos, Button btn)
        {
            // btnSayi = System.Int32.Parse(btn.transform.name);

            for (int i = 0; i < pos.Length; i++)
            {
                if (_scrollPos < pos[i] + (distance / 2) && _scrollPos > pos[i] - (distance / 2))
                {
                    _scrollbar.value = Mathf.Lerp(_scrollbar.value, pos[_btnNumber], 1f * Time.deltaTime);

                }
            }

            for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
            {
                btn.transform.name = ".";
            }
        }

        public void WhichBtnClicked(Button btn)
        {
            btn.transform.name = "clicked";
            for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
            {
                if (btn.transform.parent.transform.GetChild(i).transform.name == "clicked")
                {
                    _btnNumber = i;
                    _takeTheBtn = btn;
                    _time = 0;
                    _scrollPos = (_pos[_btnNumber]);
                    _runIt = true;
                }
            }
        }
    }
}
