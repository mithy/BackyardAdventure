using System.Collections.Generic;
using UnityEngine;

public class CarHandlerView : MonoBehaviour {

    private class Movement {
        public float speed;
        public float startTime;
        public float journeyLength;
        public Transform car;
        public Vector3 startpos;
        public Vector3 endpos;
        public bool isFinished;
    }

    [SerializeField]
    private Transform _car1;

    [SerializeField]
    private Transform _car2;

    [SerializeField]
    private Transform _car1Spawn;

    [SerializeField]
    private Transform _car1Destroy;

    [SerializeField]
    private Transform _car2Spawn;

    [SerializeField]
    private Transform _car2Destroy;

    private List<Movement> _mvm = new List<Movement>();
    private Movement _c1;
    private Movement _c2;

    void Start () {
        _c1 = new Movement() {
            journeyLength = Vector3.Distance(_car1Spawn.position, _car1Destroy.position),
            car = _car1,
            startpos = _car1Spawn.position,
            endpos = _car1Destroy.position,
            isFinished = true
        };

        _c2 = new Movement() {
            journeyLength = Vector3.Distance(_car2Spawn.position, _car2Destroy.position),
            car = _car2,
            startpos = _car2Spawn.position,
            endpos = _car2Destroy.position,
            isFinished = true
        };

        _mvm.Add(_c1);
        _mvm.Add(_c2);
    }
	
	private void Update () {
        for (int i = 0; i < _mvm.Count; i++) {
            Movement m = _mvm[i];

            float distCovered = (Time.time - m.startTime) * m.speed;
            float fracJourney = distCovered / m.journeyLength;
            m.car.position = Vector3.Lerp(m.startpos, m.endpos, fracJourney);

            if (Mathf.Approximately(fracJourney, 1) || fracJourney > 1) {
                m.isFinished = true;
            }
        }

        if (Random.Range(0, 200) < 45) {
            GenerateCarMovement();
        }
    }

    private void GenerateCarMovement() {
        Movement m = Random.Range(0, 2) == 0 ? _c1 : _c2;
        if (m.isFinished == true) {
            m.startTime = Time.time;
            m.speed = Random.Range(10, 50);
            m.isFinished = false;
        }
    }
}
