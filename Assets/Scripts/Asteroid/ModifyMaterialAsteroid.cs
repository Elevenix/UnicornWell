using UnityEngine;
using System.Collections;

public class ModifyMaterialAsteroid : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private float transitionDuration = 1f;

    private MaterialPropertyBlock _mpb;
    private Coroutine _transitionCoroutine;

    private static readonly int TransitionID =
        Shader.PropertyToID("_transitionValue");

    private void Awake()
    {
        _mpb = new MaterialPropertyBlock();
        ChangeValueAtStart();
    }

    private void ChangeValueAtStart()
    {
        targetRenderer.GetPropertyBlock(_mpb);
        _mpb.SetFloat(TransitionID, -0.1f);
        targetRenderer.SetPropertyBlock(_mpb);
    }

    public void PlayTransition()
    {
        // Stoppe la transition précédente si elle existe
        if (_transitionCoroutine != null)
            StopCoroutine(_transitionCoroutine);

        _transitionCoroutine = StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        float time = 0f;

        while (time < transitionDuration)
        {
            time += Time.deltaTime;
            float value = Mathf.Clamp01(time / transitionDuration);

            targetRenderer.GetPropertyBlock(_mpb);
            _mpb.SetFloat(TransitionID, value);
            targetRenderer.SetPropertyBlock(_mpb);

            yield return null;
        }

        // S'assure que la valeur finale est bien à 1
        targetRenderer.GetPropertyBlock(_mpb);
        _mpb.SetFloat(TransitionID, 1f);
        targetRenderer.SetPropertyBlock(_mpb);

        _transitionCoroutine = null;
    }
}
