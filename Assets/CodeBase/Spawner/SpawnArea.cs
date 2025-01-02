using UnityEngine;

namespace CodeBase.Spawner
{
    public class SpawnArea : MonoBehaviour
    {
        [SerializeField] private Vector3 areaSize = new Vector3(); 

       
        public Vector3 GetSpawnPoint()
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                Random.Range(-areaSize.y / 2, areaSize.y / 2),
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );
            return transform.position + randomPoint;
        }

        
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(transform.position, areaSize);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, areaSize);
        }
    }
}