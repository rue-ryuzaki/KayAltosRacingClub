﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using KARC.GameObjsTemplates;

namespace KARC.ScenesTemplates
{
    abstract class Scene:IBehaviour
    {
        Dictionary<int, GameObject> _objDict;
        private int _id;
        public Scene ()
        {
            _objDict = new Dictionary<int, GameObject>();
            _id = 1;
        }

        public void AddObject (GameObject newObj)
        {
            _objDict.Add(_id, newObj);
            _id++;
        }

        public void RemoveObject (int key)
        {
            _objDict.Remove(key);
            _id--;
        }
        public virtual void Update()
        {
            List<int> deadObjsKeysList = new List<int>();
            foreach (var obj in _objDict)
            {
                obj.Value.Update();
                if (obj.Value.ToDelete == false)
                    deadObjsKeysList.Add(obj.Key);
            }

            foreach (var key in deadObjsKeysList)
            {
                RemoveObject(key);
            }
        }
    }
}
