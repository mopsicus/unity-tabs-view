using System;
using DG.Tweening;
using UnityEngine;

namespace Mopsicus.Tab {

    /// <summary>
    /// View for tab container 
    /// </summary>
    public class TabView : MonoBehaviour {

        /// <summary>
        /// Get this value from config
        /// </summary>
        const float REFERENCE_SCREEN_WIDTH = 1080f;

        /// <summary>
        /// Duration for move tab
        /// </summary>
        const float TAB_MOVE_DURATION = 0.4f;

        /// <summary>
        /// Content object to show/hide
        /// </summary>
        [SerializeField]
        private GameObject Content = null;

        /// <summary>
        /// Tab rect cache
        /// </summary>
        private RectTransform _transform = null;

        /// <summary>
        /// Contructor
        /// </summary>
        void Awake () {
            _transform = GetComponent<RectTransform> ();
        }

        /// <summary>
        /// Show/hide tab content
        /// </summary>
        /// <param name="value">State</param>
        /// <param name="direction">Direction to move tab</param>
        public void Switch (bool value, TabDirection direction) {
            if (direction == TabDirection.NONE) {
                Content.SetActive (value);
                _transform.DOAnchorPosX (0f, 0f);
                return;
            }
            if (value) {
                float position = (direction == TabDirection.LEFT) ? -REFERENCE_SCREEN_WIDTH : REFERENCE_SCREEN_WIDTH;
                _transform.DOAnchorPosX (position, 0f).OnComplete (() => {
                    Content.SetActive (true);
                    _transform.DOAnchorPosX (0f, TAB_MOVE_DURATION);
                });
            } else {
                float position = (direction == TabDirection.LEFT) ? REFERENCE_SCREEN_WIDTH : -REFERENCE_SCREEN_WIDTH;
                _transform.DOAnchorPosX (position, TAB_MOVE_DURATION).OnComplete (() => {
                    Content.SetActive (false);
                });
            }
        }

    }

}