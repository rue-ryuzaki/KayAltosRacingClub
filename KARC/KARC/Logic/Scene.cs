﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Scene //Класс, который содержит в себе объекты и карту их расстановки
    {
        public Dictionary<int,Object> objectList;//Список всех объектов сцены
        private Dictionary<int, Object> initialObjectList;//Начальное состояние сцены
        protected int[,] map;//Клетки карты (одна клетка, по идее, один экран)
        protected int scale;// Масштаб одного тайла(клетки) карты
        public int Id=1;
        private Keys [] _pushedButtons;//Нажатые клавиши в сцене

        //public Song song;

        public Scene (int [,] _map, int _scale, List<Object> _objList)
        {
            map = new int[_map.GetLength(0), _map.GetLength(1)];
            for (int y = 0; y < map.GetLength(1); y++)
                for (int x = 0; x < map.GetLength(0); x++)
                    map[x, y] = _map[x, y];

            scale = _scale;
            objectList = new Dictionary<int, Object>();
            
            foreach (var obj in _objList)
            {
                AddObject(obj);
            }  
        }

        public Scene (int[,] _map, int _scale)
        {
            map = new int[_map.GetLength(0), _map.GetLength(1)];
            for (int y = 0; y < map.GetLength(1); y++)
                for (int x = 0; x < map.GetLength(0); x++)
                    map[x, y] = _map[x, y];

            scale = _scale;
            objectList = new Dictionary<int, Object>();
        }

        public virtual void AddObject (Object aObject)
        {
            aObject.id = Id;
            objectList.Add(Id, aObject);
            if (aObject.player)
                Game1.playerId = Id;
            Id++;
        }

        public virtual void updateScene(int _time)
        {
            _pushedButtons = Keyboard.GetState().GetPressedKeys();  
            foreach (var obj in objectList)
                obj.Value.Update(_time);
        }
        
        public Keys[] GetPressedButtons()
        {   if (_pushedButtons!=null)
                return (Keys[])_pushedButtons.Clone();
            else
                return null;
        }
           
        public void scroll (Vector2 _shift)
        {
            foreach (var obj in objectList)
            {
                if (obj.Value.GetType() == typeof(Speedometer))
                    continue;
                obj.Value.pos += _shift;
                if (obj.Value.physical)
                {
                    PhysicalObject o = (PhysicalObject)obj.Value;
                    o.hitBox.X = (int)o.pos.X+5;
                    o.hitBox.Y = (int)o.pos.Y+5;
                }
            }
        }
    }
}
