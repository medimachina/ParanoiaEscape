using UnityEngine;

public class TransformResetter
{
    private Vector3 _startScale;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    public TransformResetter(Transform source)
    {
        Save(source);
    }

    public void Save(Transform source)
    {
        _startScale = source.localScale;
        _startPosition = source.localPosition;
        _startRotation = source.localRotation;
    }

    public void Reset(Transform target)
    {
        target.localScale = _startScale;
        target.localRotation = _startRotation;
        target.localPosition = _startPosition;
    }
}
