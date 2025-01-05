using System.Collections;
using CodeBase.Spawner;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class FollowTargetByCoroutine : MonoBehaviour
    {
        private float _speed;
        private Coroutine _followCoroutine;
        private SpawnController _spawnerController;

        public void Init(SpawnController spawnerController, float speed)
        {
            _speed = speed;
            _spawnerController = spawnerController;
            
        }

        public void StartFollow()
        {
            _followCoroutine = StartCoroutine(FollowTarget());
        }

        public void StopFollow()
        {
            if (_followCoroutine != null)
            {
                StopCoroutine(_followCoroutine);
            }
        }
        
        private IEnumerator FollowTarget()
        {
            while (true)
            {
                if (_spawnerController != null)
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,  
                        _spawnerController.GetTargetTransform().position, 
                        _speed * Time.deltaTime
                    );
                    
                    Vector3 direction = _spawnerController.GetTargetTransform().position - transform.position;
                    if (direction != Vector3.zero)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Lerp(
                            transform.rotation, 
                            targetRotation, 
                            _speed * Time.deltaTime
                        );
                    }
                }
                
                yield return null;
            }
        }
    
    }
}
