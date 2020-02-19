using System;
using LeopotamGroup.SystemUi.Widgets;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mopsicus.Tab {

	/// <summary>
	/// Swipe directions for tabs controller
	/// </summary>
	public enum TabDirection {

		/// <summary>
		/// Show from left
		/// </summary>
		LEFT,

		/// <summary>
		/// Show from right
		/// </summary>		
		RIGHT,

		/// <summary>
		/// Show without animation
		/// </summary>		
		NONE
	}

	/// <summary>
	/// Controller for tabs
	/// Manage tabs visibility
	/// </summary>
	[RequireComponent (typeof (NonVisualWidget))]
	public class TabsController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

		/// <summary>
		/// Min swipe distance to detect swipe
		/// </summary>
		const float MIN_SWIPE_DISTANCE = 100f;

		/// <summary>
		/// Array of tabs
		/// </summary>
		[SerializeField]
		private TabContainer[] Tabs = null;

		/// <summary>
		/// Callback on tabs change
		/// </summary>
		public Action<int> OnStateChange = delegate { };

		/// <summary>
		/// Previous key of changed tab
		/// </summary>
		private int _previousIndex = -1;

		/// <summary>
		/// Cache for drag position begin
		/// </summary>
		private Vector2 _startDragPosition = Vector2.zero;

		/// <summary>
		/// Cache for drag position end
		/// </summary>
		private Vector2 _endDragPosition = Vector2.zero;

		/// <summary>
		/// Init tabber
		/// </summary>
		private void Start () {
			if (Tabs.Length == 0) {
#if DEBUG
				Debug.LogError ("Invalid tabs count in controller");
#endif
				return;
			}
			int index = 0;
			foreach (TabContainer item in Tabs) {
				item.Index = index;
				item.OnStateChange = OnStateChage;
				index++;
			}
			Activate (0, TabDirection.NONE);
		}

		/// <summary>
		/// Tabs count
		/// </summary>
		public int Count {
			get {
				return Tabs.Length;
			}
		}

		/// <summary>
		/// Callback on tabs change
		/// </summary>
		/// <param name="tab">Tab container instance</param>
		/// <param name="state">Changed state</param>
		void OnStateChage (TabContainer tab, bool state) {
			if (_previousIndex == tab.Index) {
				return;
			}
			int previous = _previousIndex;
			_previousIndex = tab.Index;
			if (previous < 0) {
				return;
			}
			TabDirection direction = (previous > tab.Index) ? TabDirection.LEFT : TabDirection.RIGHT;
			Tabs[previous].SetState (false, direction);
			OnStateChange (tab.Index);
		}

		/// <summary>
		/// Activate tab
		/// </summary>
		/// <param name="index">Tab index</param>
		/// <param name="direction">Swipe direction</param>
		public void Activate (int index, TabDirection direction) {
			if (index < 0 || index > Tabs.Length - 1) {
				return;
			}
			Tabs[index].ChangeState (TabState.ENABLE, direction);
		}

		/// <summary>
		/// Event on begin swipe
		/// </summary>
		/// <param name="data">Event data</param>

		public void OnBeginDrag (PointerEventData data) {
			_startDragPosition = data.position;
		}

		/// <summary>
		/// Event end drag and release touch
		/// Detect swipe and direction
		/// </summary>
		/// <param name="data">Event data</param>
		public void OnEndDrag (PointerEventData data) {
			_endDragPosition = data.position;
			float distance = Vector2.Distance (_startDragPosition, _endDragPosition);
			if (distance < MIN_SWIPE_DISTANCE) {
				return;
			}
			if (_startDragPosition.x < _endDragPosition.x) {
				Activate (_previousIndex - 1, TabDirection.LEFT);
			} else {
				Activate (_previousIndex + 1, TabDirection.RIGHT);
			}
		}

		/// <summary>
		/// Event on swipe
		/// </summary>
		/// <param name="data">Event data</param>
		public void OnDrag (PointerEventData data) { }
	}

}