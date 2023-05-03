using UnityEngine;
using UnityEngine.UI;

public class ShowRecord : MonoBehaviour
{
	[SerializeField] private Text recordText;
	
	private void Awake()
	{
		recordText.text = "Ваш рекорд: " + PlayerPrefs.GetInt("Record", 0);
	}
}
