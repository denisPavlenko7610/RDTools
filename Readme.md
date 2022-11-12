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
public void DoSomething()
{
    Debug.Log("add params");
}

[Button("Add Params", space:30f)] // add space
public void DoSomething()
{
    Debug.Log("add params");
}
```

# ReadOnly

- Add ReadOnly attribute to disable editing serializefield

```c#
[SerializeField, ReadOnly] float xSensitivity = 30f;
```

# Auto attach

- Add Attach attribute to get some component. Use empty to attach component on this object or with params - Attach scene, or     Attach parent, child to attach these objects.

Examples:

```c#
    public class AutoAttachDemo : MonoBehaviour
    {
        [Attach] //Get component on current gameObject
        public NavMeshAgent agent;
        
        [Attach(Attach.Parent)] //Get component in parent gameObjects
        public Collider colliderInParent;
        
        [Attach(Attach.Child)] //Get component in children gameObjects
        [SerializeField]
        private Collider colliderInChildren;
        
        [Attach(Attach.Child, false)] //Get component in children gameObjects, can be changed in inspector
        [SerializeField]
        private Collider colliderInChildrenModifiable;
        
        [Attach(Attach.Child)] //Get components in children gameObjects
        [SerializeField]
        private Renderer[] rendererArray;
        
        [Attach(Attach.Parent)] //Get components in parent gameObjects
        [SerializeField]
        private List<Collider> colliderList;
        
        [Attach(Attach.Child)] //Get components in children gameObjects
        [SerializeField]
        private List<MeshFilter> meshFilterList;
        
        [AttachOrAdd] //Get or add if not exist (similar to RequireComponent)
        public NavMeshAgent requiredAgent;
        
        [Attach(Attach.Scene)] //Get component in scene
        [SerializeField] private Camera anyCamera;
        
        [Attach(Attach.Scene)] //Get components in scene
        [SerializeField] private Light[] allLights;
    }
```
