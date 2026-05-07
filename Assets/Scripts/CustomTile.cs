using UnityEngine;

[ExecuteAlways] public class CustomTile : MonoBehaviour{
	[SerializeField] private bool	isCube;
	[SerializeField] private float	TilingX = 1f;
	[SerializeField] private float	TilingY = 1f;

	private Renderer				_renderer;
	private MaterialPropertyBlock	_propBlock;
	private float					multiplier;

	private void	OnEnable(){
		UpdateTiling();
	}

	private void	OnValidate(){
		UpdateTiling();
	}

	private void	UpdateTiling(){
		// Costructor
		if (_renderer == null) _renderer = GetComponent<Renderer>();
		if (_propBlock == null) _propBlock = new MaterialPropertyBlock();

		// Renderer
		if (_renderer != null){
			_renderer.GetPropertyBlock(_propBlock);
			_propBlock.SetVector(
				"_BaseMap_ST", new Vector4(TilingX, TilingY, 0f, 0f)
			);
			_renderer.SetPropertyBlock(_propBlock);
		}

		// Transform
		multiplier = isCube ? 1f : 10f;
		transform.localScale = new Vector3(
			TilingX / multiplier, transform.localScale.y, TilingY / multiplier
		);
	}
}
