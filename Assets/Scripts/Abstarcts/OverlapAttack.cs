using UnityEngine;

public partial class OverlapAttack : MonoBehaviour
{
    [Header("Masks")]
    public LayerMask _searchLayerMask;

    [Header("Overlap Area")]
    public Transform _overlapStartPoint;
    public Vector3 _offset;
    [Min(0f)] public float _sphereRadius = 70f;

    [Header("Gizmos")]
    public Color _gizmosColor = Color.cyan;

    private readonly Collider[] _overlapResults = new Collider[1];
    private int _overlapResultsCount;

    public void PerformAttack(float damage)
    {
        if (TryFindEnemies())
        {
            TryAttackEnemies(damage);
        }
    }

    public bool TryFindEnemies()
    {
        var position = _overlapStartPoint.TransformPoint(_offset);
        _overlapResultsCount = OverlapSphere(position);
        return _overlapResultsCount > 0;
    }
    private int OverlapSphere(Vector3 position)
    {
        return Physics.OverlapSphereNonAlloc(position, _sphereRadius * gameObject.transform.localScale.x, _overlapResults, _searchLayerMask.value);
    }

    private void TryAttackEnemies(float damage)
    {
        for (int i = 0; i < _overlapResultsCount; i++)
        {
            if (_overlapResults[i].TryGetComponent(out IDamageable damageable) == false)
            {
                continue;
            }

            damageable.ApplyDamage(damage);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        TryDrawGizmos();
    }

    private void TryDrawGizmos()
    {
        if (_overlapStartPoint == null)
            return;

        Gizmos.matrix = _overlapStartPoint.localToWorldMatrix;
        Gizmos.color = _gizmosColor;

        Gizmos.DrawSphere(_offset, _sphereRadius);
    }
#endif
}
