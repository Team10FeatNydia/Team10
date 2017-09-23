using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour, IHasChanged {
	[SerializeField] Transform slots;
	[SerializeField] Text inventoryText;

	public GameObject CanvasObject;

	// Use this for initialization
	void Start () {
		HasChanged ();
	}

	#region IHasChanged implementation
	public void HasChanged ()
	{
		//using stringbuilder is for reduce the memory/garbagecollector 
		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		builder.Append (" - ");
		foreach (Transform slotTransform in slots){
			GameObject item = slotTransform.GetComponent<SlotScript>().item;
			if (item){
				builder.Append (item.name);
				builder.Append (" - ");
			}
		}
		inventoryText.text = builder.ToString ();
	}
	#endregion

	public void MakeActive()
	{
		CanvasObject.SetActive(true);
	}
}

namespace UnityEngine.EventSystems 
{
	public interface IHasChanged : IEventSystemHandler {
		void HasChanged();
	}
}