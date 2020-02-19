using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mopsicus.Tab {

    /// <summary>
    /// View for indicate current active tab
    /// </summary>
    public class TabsIndicator : MonoBehaviour {

        /// <summary>
        /// Tabs controller to indicate for
        /// </summary>
        [SerializeField]
        private TabsController Controller = null;

        /// <summary>
        /// Array with images
        /// </summary>
        [SerializeField]
        private Image[] Indicators = null;

        /// <summary>
        /// Sprite to show active tab
        /// </summary>
        [SerializeField]
        private Sprite ActiveSprite = null;

        /// <summary>
        /// Sprite to show inactive tabs
        /// </summary>
        [SerializeField]
        private Sprite InactiveSprite = null;

        /// <summary>
        /// Init indicator
        /// </summary>
        void Start () {
            if (Indicators.Length == 0) {
#if DEBUG
                Debug.LogError ("Invalid indicators count in controller");
#endif
                return;
            }
            Controller.OnStateChange += OnStateChange;
        }

        /// <summary>
        /// Callback when tab changed
        /// </summary>
        /// <param name="index">New active index</param>
        void OnStateChange (int index) {
            foreach (Image item in Indicators) {
                item.sprite = InactiveSprite;
            }
            Indicators[index].sprite = ActiveSprite;
        }
    }

}