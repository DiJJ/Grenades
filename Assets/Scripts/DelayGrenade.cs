using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class DelayGrenade : Grenade
{
    [SerializeField] float _seconds;
    [SerializeField] bool useAsync;
    private Task explodeTask;

    private void Start()
    {
        if (useAsync)
        {
            explodeTask = new Task(async () =>
            {
                TimeSpan delay = new TimeSpan(0, 0, (int)_seconds);
                await Task.Delay(delay);
                Explode();
            });
            explodeTask.Wait();
        } else
            StartCoroutine(DelayCoroutine());
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(_seconds);
        Explode();
    }
}
