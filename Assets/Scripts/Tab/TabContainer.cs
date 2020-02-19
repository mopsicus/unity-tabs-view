using System;
using UnityEngine;

namespace Mopsicus.Tab {

	/// <summary>
	/// Tab states
	/// </summary>
	public enum TabState {

		/// <summary>
		/// Hide tab
		/// </summary>
		DISABLE = 0,

		/// <summary>
		/// Show tab
		/// </summary>		
		ENABLE = 1
	}

	/// <summary>
	/// Tab container for data
	/// Just object to manage show/hide and position animation
	/// </summary>
	public class TabContainer : MonoBehaviour {

		/// <summary>
		/// Tab index
		/// </summary>
		[HideInInspector]
		public int Index = 0;

		/// <summary>
		/// Callback of tab change state
		/// </summary>
		public Action<TabContainer, bool> OnStateChange = delegate { };

		/// <summary>
		/// Current state
		/// </summary>
		private bool _state = false;

		/// <summary>
		/// View
		/// </summary>
		private TabView _view = null;

		/// <summary>
		/// Contructor
		/// </summary>
		void Awake () {
			_view = GetComponent<TabView> ();
		}

		/// <summary>
		/// Set state manually from controller
		/// </summary>
		/// <param name="state">State to change</param>
		/// <param name="direction">Swipe direction</param>
		public void SetState (bool state, TabDirection direction) {
			_state = state;
			_view.Switch (_state, direction);
		}

		/// <summary>
		/// Change tab state
		/// </summary>
		/// <param name="state">State to change</param>
		/// <param name="direction">Swipe direction</param>
		public void ChangeState (TabState state, TabDirection direction) {
			_state = (state == TabState.DISABLE) ? false : true;
			OnStateChange (this, _state);
			SetState (_state, direction);
		}

	}

}