using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class Laser : MonoBehaviour
{
    public float maxDistanceLimit;
    float currentMaxDistance = 1f;
    public float extendSpeed;
    Transform beamStart;
    VectorLine laserLine;

    List<float> beamDistances;
    List<float> previousBeamDistances;
    List<Vector3> beamPoints;
    List<GameObject> objectsHit;
    List<GameObject> previousObjectsHit = new List<GameObject>();

    public float laserPower;

    void Awake() {
        beamPoints = new List<Vector3>();
        beamStart = transform.Find("BeamStart");
        laserLine = new VectorLine("LaserLine", beamPoints, 5.0f, LineType.Continuous, Joins.Fill);
    }

    void Update() {
        currentMaxDistance += Time.deltaTime * extendSpeed;
        currentMaxDistance = Mathf.Min(currentMaxDistance, maxDistanceLimit);
        StartBeam();
        VisualizeBeam();
        CompareBeams();
    }

    void StartBeam() {
        beamPoints.Clear();
        objectsHit = new List<GameObject>();
        beamDistances = new List<float>();
        ShootBeam(beamStart.position, beamStart.up, currentMaxDistance);
    }

    void ShootBeam(Vector2 origin, Vector2 dir, float maxDistance) {
        var hit = Physics2D.Raycast(origin, dir, maxDistance);

        if(hit.collider == null) {
            CreateBeamSegment(origin, origin + dir * maxDistance);
            return;
        }
        // Check if starts inside object.
        if(hit.distance <= 0.1f) {
            return;
        }

        beamDistances.Add(hit.distance);

        var go = hit.collider.gameObject;
        CreateBeamSegment(origin, hit.point);

        objectsHit.Add(go);
        if(go.tag == "Mirror") {
            var newDir = Vector2.Reflect(dir, hit.normal);
            ShootBeam(hit.point - dir * 0.001f, newDir, maxDistance - hit.distance);
        }

        IDamageable damageable = go.GetComponentInParent<IDamageable>();
        if(damageable != null) {
            damageable.DamageIt(laserPower);
        }
    }

    void CreateBeamSegment(Vector2 start, Vector2 end) {
        if(beamPoints.Count == 0)
            beamPoints.Add(start);
        beamPoints.Add(end);
    }

    void VisualizeBeam() {
        laserLine.Draw();
        for(int i = 0; i < beamPoints.Count - 1; i++) {
            Debug.DrawLine(beamPoints[i], beamPoints[i + 1]);
        }
    }

    void CompareBeams() {
        // compare objectsHit to previousObjectsHit
        for(int i = 0; i < previousObjectsHit.Count; i++) {
            if (i >= objectsHit.Count || objectsHit[i] != previousObjectsHit[i]) {
                var dist = 0f; ;
                for(int j = 0; j < i; j++) {
                    dist += beamDistances[j];
                }
                dist += previousBeamDistances[i];
                currentMaxDistance = dist;
                break;
            }
        }
        // if changed clamp currentMaxDistance
        previousBeamDistances = beamDistances;
        previousObjectsHit = objectsHit;
    }
}
