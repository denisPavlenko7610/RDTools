Use RDragonTools nameSpace

# Button

- Add Button attribute for methods to create button
```c#
[Button]
public void DoSomething()
{
    Debug.Log("add params");
}
    
[Button("Add Params")] //add some name
private void DoSomething()
{
    Debug.Log("add params");
}

[Button("Add Params", space:30f)] // add space
private void DoSomething()
{
    Debug.Log("add params");
}
```

# ReadOnly

- Add ReadOnly attribute to disable editing serializefield

```c#
[SerializeField, ReadOnly] private float _xSensitivity = 30f;
```

# Auto attach

- Add Attach attribute to get some component. Use empty to attach component on this object or with params - Attach scene, or     Attach parent, child to attach these objects.

Examples:

```c#
    public class AutoAttachDemo : MonoBehaviour
    {
        [Attach] //Get component on current gameObject
        private NavMeshAgent _agent;
        
        [Attach(Attach.Parent)] //Get component in parent gameObjects
        private Collider _colliderInParent;
        
        [Attach(Attach.Child)] //Get component in children gameObjects
        [SerializeField]
        private Collider _colliderInChildren;
        
        [Attach(Attach.Child, false)] //Get component in children gameObjects, can be changed in inspector
        [SerializeField]
        private Collider _colliderInChildrenModifiable;
        
        [Attach(Attach.Child)] //Get components in children gameObjects
        [SerializeField]
        private Renderer[] _rendererArray;
        
        [Attach(Attach.Parent)] //Get components in parent gameObjects
        [SerializeField]
        private List<Collider> _colliderList;
        
        [Attach(Attach.Child)] //Get components in children gameObjects
        [SerializeField]
        private List<MeshFilter> _meshFilterList;
        
        [AttachOrAdd] //Get or add if not exist (similar to RequireComponent)
        private NavMeshAgent _requiredAgent;
        
        [Attach(Attach.Scene)] //Get component in scene
        [SerializeField] private Camera _anyCamera;
        
        [Attach(Attach.Scene)] //Get components in scene
        [SerializeField] private Light[] _allLights;
    }
```

# Extentions and utilities

#Serialized vectors, quaternions

- UnityEngine.Vector2,3.. is not marked as Serializable. 
When attempting to save a Vector3 variable's value (or a Quaternion for that matter) to disk, you may find that Unity will throw a UnityEngine.Vector3 is not marked as Serializable error. To work around this, you can create a new SerializedVector struct that can be used to serialize your Vector3 and saved to disk.

```c#
[SerializeField] private SerializedVector2 _vector2;
[SerializeField] private SerializedVector3 _vector3;
[SerializeField] private SerializedQuaternion _quaternion;
```
