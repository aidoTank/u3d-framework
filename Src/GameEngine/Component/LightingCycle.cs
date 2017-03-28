using UnityEngine;
using System.Collections;

/***
 * LightingCycle.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class LightingCycle : MonoBehaviour
    {
        private Renderer m_renderer;
        private Material m_material;

        // 延迟时间
        public float delayTime = 0.5f;
        // 刷新频率
        public float cycleRate = 2.5f;
        // 扫光速度
        public float lightSpeed = 6f;

        void Start()
        {
            m_renderer = GetComponent<Renderer>();
            if (m_renderer != null) {
                m_material = m_renderer.material;
                InvokeRepeating("Lighting", delayTime, cycleRate);
            }
        }

        /// <summary>
        /// 闪光函数
        /// </summary>
        void Lighting()
        {
            StartCoroutine(StartLight());
        }

        IEnumerator StartLight()
        {
            float passTime = -5;
            while (true) {
                if (passTime >= 5) {
                    break;
                }
                passTime += Time.deltaTime * lightSpeed;
                m_material.SetFloat("_MaskRate", passTime);
                yield return null;
            }
        }

    }

}
