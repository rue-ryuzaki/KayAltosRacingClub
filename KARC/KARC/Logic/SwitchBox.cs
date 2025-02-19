﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KARC.Logic
{
    class SwitchBox:Button
    {
        protected SpriteFont font;
        protected string[] valuesArray;
        protected int currentIndex;
        public SwitchBox(Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList,  int _tabIndex, SpriteFont _font, string [] _valuesArray, int _currentIndex) : base(_pos, _layer, _loadTextList, _tabIndex)
        {
            tabIndex = _tabIndex;
            font = _font;            
            period = 100;
            valuesArray = new string[_valuesArray.Length];
            _valuesArray.CopyTo(valuesArray, 0);
            currentIndex = _currentIndex;            
        }

        public override void Update(int _time)
        {
            if (check)
                currentImage = images["light"];
            else
                currentImage = images["dark"];            
        }

        public void ChangeIndex (string direction, int _time)
        {
            //currentTime += _time;
            //if (currentTime > period)
           // {
                currentTime = 0;
                switch (direction)
                {
                    case "right":
                        {
                            currentIndex++;
                            if (currentIndex >= valuesArray.Length)
                                currentIndex = 0;
                            break;
                        }
                    case "left":
                        {
                            currentIndex--;
                            if (currentIndex < 0)
                                currentIndex = valuesArray.Length - 1;
                            break;
                        }
                }
           // }
        }

        public string GetValue ()
        {
            return valuesArray[currentIndex];
        }
        public static bool ParseValue (string _inputValue, out int [] _dimensions)
        {
            bool correct = true;
            _dimensions = null;
            if (_inputValue.Contains("x"))
            {
                try
                {
                    string[] dimensionsArray = _inputValue.Split('x');
                    int width = Convert.ToInt32(dimensionsArray[0]);
                    int height = Convert.ToInt32(dimensionsArray[1]);
                    _dimensions = new int[2] { width, height };
                }
                catch
                {
                    correct = false;
                }
            }
            else
            {
                correct = false;                
            }     
            return correct;
        }

        public static bool ParseValue(string _inputValue, out bool _flag)
        {
            bool correct = true;
            _flag = false;
            string input = _inputValue.ToLower();
            if (input == "да" || input == "yes")
                _flag = true;
            else if (input == "нет" || input == "no")
                _flag = false;
            else
                correct = false;
            return correct;
        }

        public static bool ParseValue(string _inputValue, out float _value)
        {
            bool correct = true;

            float outValue = 0.0f;
            _value = 0.0f;
            if (float.TryParse(_inputValue, out outValue))
            {
                _value = outValue / 100.0f;
                if (_value > 1.0f || _value < 0.0f)
                {
                    _value = 0.0f;
                    correct = false;
                }                    
            }
            else
            {
                correct = false;
            }
            return correct;
        }

        public override void drawObject(SpriteBatch _spriteBatch, int _time)//Метод отрисовки объекта
        {
            _spriteBatch.Draw(currentImage, pos, null, colDraw, MathHelper.ToRadians(angle), Vector2.Zero, scale, SpriteEffects.None, layer);
            Vector2 stringLength = font.MeasureString(valuesArray[currentIndex]);
            _spriteBatch.DrawString(font, valuesArray[currentIndex], new Vector2(pos.X+(currentImage.Width-stringLength.X)/2, 
                pos.Y + (currentImage.Height - stringLength.Y) / 2), Color.Black);            
        }

    }
}
