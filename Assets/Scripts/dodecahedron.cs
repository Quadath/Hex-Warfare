using UnityEngine;

public class dodecahedron : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    // P(± r/√3, ± r/√3, ± r/√3)
    //
    // P(0, ± r/(√3*φ), ± (r*φ)/√3)
    //
    // P(± r/(√3*φ), ± (r*φ)/√3, 0)
    //
    // P(± (r*φ)/√3, 0, ± r/(√3*φ))


    private static float _fi = 1.618f;
    private Vector3[] _points =
    {
        new Vector3(1, 1, 1),
        new Vector3(-1, 1, 1),
        new Vector3(1, -1, 1),
        new Vector3(1, 1, -1),
        new Vector3(-1, -1, 1),
        new Vector3(-1, 1, -1),
        new Vector3(1, -1, -1),
        new Vector3(-1, -1, -1),
        
        new Vector3(0, 1/_fi, _fi),
        new Vector3(0, -1/_fi, _fi),
        new Vector3(0, 1/_fi, -_fi),
        new Vector3(0, -1/_fi, -_fi),
        
        new Vector3(1/_fi, _fi, 0),
        new Vector3(-1/_fi, _fi, 0),
        new Vector3(1/_fi, -_fi, 0),
        new Vector3(-1/_fi, -_fi, 0),
        
        new Vector3(1/_fi, 0, _fi),
        new Vector3(-1/_fi, 0, _fi),
        new Vector3(1/_fi, 0, -_fi),
        new Vector3(-1/_fi, 0, -_fi),
    };

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var point in _points)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
