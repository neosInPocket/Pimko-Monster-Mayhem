using UnityEngine;

[CreateAssetMenu(menuName = "RingMaterials")]
public class RingMaterials : ScriptableObject
{
	[SerializeField] private Material[] materials;
	[SerializeField] private string[] materialNames;
	public Material[] Materials => materials;
	public string[] MaterialNames => materialNames;
}
