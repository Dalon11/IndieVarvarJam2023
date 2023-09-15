using Jam.Fabric.Initable.Abstraction;
using Jam.Fabric.Updator.Abstraction;
using Jam.GameInput.Abstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : IInitable<IUpdator>, IInitable<GameObject>, IDisposable
{
    private GameObject _heroPrefab;
    private IUpdator _updator;
    private float _valueRotate;
    private float _forceRotate = 3f;
    private void Rotating()
    {
        _valueRotate += Input.GetAxis("Mouse X");

        _heroPrefab.transform.rotation = Quaternion.Euler(0f, _valueRotate * _forceRotate, 0f);
    }

    public void Dispose()
    {
        _updator.OnUpdate -= Rotating;
    }

    public void Init(GameObject model)
    {
        _heroPrefab= model;
    }

    public void Init(IUpdator model)
    {
        _updator = model;
        _updator.OnUpdate += Rotating;
    }
}
