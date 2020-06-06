using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/***
 * UGUIAtlas.cs
 * 
 * @anthor administrator
 */
namespace GameEngine
{
    public class UGUIAtlas : ScriptableObject
    {
        public Texture2D MainTex;
        public List<Sprite> SpriteLists = new List<Sprite>();

        public Sprite GetSprite(string spritename)
        {
            return SpriteLists.Find((Sprite s) => {
                return s.name == spritename;
            });
        }

        public void SetSprite(ref Image image, string spriteName)
        {
            if (image == null) {
                return;
            }
            Sprite sp = GetSprite(spriteName);
            if (sp != null) {
                image.overrideSprite = sp;
            } else {
                Debug.Log("Not Exist Image:" + spriteName);
            }
        }
    }
}
