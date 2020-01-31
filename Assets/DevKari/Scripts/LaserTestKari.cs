using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class LaserTestKari : MonoBehaviour {
    public float beamReach;
    Transform beamStart;
    VectorLine laserLine;

    List<Vector3> beamPoints;

    public float laserPower;

    void Awake() {
        beamPoints = new List<Vector3>();
        beamStart = transform.Find("BeamStart");
        laserLine = new VectorLine("LaserLine", beamPoints, 5.0f, LineType.Continuous, Joins.Fill);
	}
	
	void Update () {
        StartBeam();
        VisualizeBeam();
	}

    void StartBeam() {
        beamPoints.Clear();
        ShootBeam(beamStart.position, beamStart.up, beamReach);
    }

    void ShootBeam(Vector2 origin, Vector2 dir, float maxDistance) {

        var hit = Physics2D.Raycast(origin, dir, maxDistance);

        // Check if starts inside object.
        if (hit.distance <= 0.1f) {
            return;
        }


        if (hit.collider == null) {
            CreateBeamSegment(origin, origin + dir * maxDistance);
            return;
        }

        // Vectors for additional rays on both sides of original ray
        var side = (Vector2)Vector3.Cross(dir, Vector3.forward).normalized;
        Vector2 orig1 = origin - side * 0.5f;
        Vector2 orig2 = origin + side * 0.5f;

        // Additional hits on both sides of original ray
        var hit1 = Physics2D.Raycast(orig1, dir, maxDistance);
        var hit2 = Physics2D.Raycast(orig2, dir, maxDistance);

        // Calculate averages for vectors;
        var mid = hit.normal;
        int hits = 1;
        Debug.Log("hit.normal x, y : " + hit.normal.x + " " + hit.normal.y);
        // for every valid hit: mid += hit.normal; hits++;
        if (hit1.collider != null) {
            hits++;
            mid += hit1.normal;
        }
        if (hit2.collider != null) {
            hits++;
            mid += hit2.normal;
        }

        //var mid = (hit.normal + hit1.normal + hit2.normal) / 3;
        mid /= hits;
        mid = mid.normalized;

        Debug.Log(mid);

        var go = hit.collider.gameObject;
        CreateBeamSegment(origin, hit.point);
        if (go.tag == "Mirror") {
            var newDir = Vector2.Reflect(dir, hit.normal);
            ShootBeam(hit.point - dir * 0.001f, newDir, maxDistance - hit.distance);
        
        }

        // TODO: check for laser hitting destructible things, etc.?
        
        // If enemy can be desctructed it must have the "DestructibleEnemy" script.
        // And... if enemy can heal itself it must additionally have the "TryToHealEnemy" script.
        
        // No need to check this below as "DestructibleEnemy" script has the "damageable" feature.
        //if (go.tag == "Destructible") {
            //Debug.Log("Some damage caused?");
            IDamageable damageable = go.GetComponentInParent<IDamageable>();
            if (damageable != null) {
                damageable.DamageIt(laserPower);
            }
        //}
    }

    void CreateBeamSegment(Vector2 start, Vector2 end) {
        if (beamPoints.Count == 0)
            beamPoints.Add(start);
        beamPoints.Add(end);
    }

    void VisualizeBeam() {
        laserLine.color = Color.red;
        laserLine.Draw();
        for (int i=0; i<beamPoints.Count-1; i++) {
            Debug.DrawLine(beamPoints[i], beamPoints[i + 1]);
        }
    }
}
