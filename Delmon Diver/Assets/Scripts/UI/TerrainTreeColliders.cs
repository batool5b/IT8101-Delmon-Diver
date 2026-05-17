using UnityEngine;
using System.Collections.Generic;

public class TerrainTreeColliders : MonoBehaviour
{
    public Terrain terrain;
    public Transform player;
    public float colliderRadius = 20f;
    public float checkInterval = 0.2f; // seconds between updates

    private struct TreeColliderData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public Mesh mesh;
        public bool convex;
    }

    private List<TreeColliderData> _treeData = new();
    private Dictionary<int, GameObject> _activeColliders = new();
    private float _timer;

    void Start()
    {
        TerrainData data = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;
        TreePrototype[] prototypes = data.treePrototypes;

        foreach (TreeInstance tree in data.treeInstances)
        {
            GameObject prefab = prototypes[tree.prototypeIndex].prefab;
            MeshCollider[] meshColliders = prefab.GetComponentsInChildren<MeshCollider>();
            if (meshColliders.Length == 0) continue;

            Vector3 worldPos = Vector3.Scale(tree.position, data.size) + terrainPos;

            foreach (MeshCollider mc in meshColliders)
            {
                _treeData.Add(new TreeColliderData
                {
                    position = worldPos,
                    rotation = Quaternion.Euler(0, tree.rotation * Mathf.Rad2Deg, 0),
                    scale = new Vector3(tree.widthScale, tree.heightScale, tree.widthScale),
                    mesh = mc.sharedMesh,
                    convex = mc.convex
                });
            }
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < checkInterval) return;
        _timer = 0f;

        float radiusSq = colliderRadius * colliderRadius;
        Vector3 playerPos = player.position;

        HashSet<int> shouldBeActive = new();

        for (int i = 0; i < _treeData.Count; i++)
        {
            float distSq = (playerPos - _treeData[i].position).sqrMagnitude;
            if (distSq <= radiusSq)
                shouldBeActive.Add(i);
        }

        // Activate new ones
        foreach (int i in shouldBeActive)
        {
            if (_activeColliders.ContainsKey(i)) continue;

            var d = _treeData[i];
            GameObject go = new GameObject("TC");
            go.transform.SetPositionAndRotation(d.position, d.rotation);
            go.transform.localScale = d.scale;

            MeshCollider mc = go.AddComponent<MeshCollider>();
            mc.sharedMesh = d.mesh;
            mc.convex = d.convex;

            _activeColliders[i] = go;
        }

        // Deactivate ones out of range
        List<int> toRemove = new();
        foreach (var kvp in _activeColliders)
        {
            if (!shouldBeActive.Contains(kvp.Key))
            {
                Destroy(kvp.Value);
                toRemove.Add(kvp.Key);
            }
        }
        foreach (int i in toRemove)
            _activeColliders.Remove(i);
    }

    void OnDestroy()
    {
        foreach (var kvp in _activeColliders)
            Destroy(kvp.Value);
    }
}