#RDTools

- Use RDTools nameSpace

#Play audio in inspector (by default)

![image](https://user-images.githubusercontent.com/13468920/201512579-c8a206aa-1150-4b0f-88c7-61438aea7ef2.png)

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

# Extensions and utilities

Serialized vectors, quaternions

- UnityEngine.Vector2,3.. is not marked as Serializable. 
When attempting to save a Vector3 variable's value (or a Quaternion for that matter) to disk, you may find that Unity will throw a UnityEngine.Vector3 is not marked as Serializable error. To work around this, you can create a new SerializedVector struct that can be used to serialize your Vector3 and saved to disk.

```c#
[SerializeField] private SerializedVector2 _vector2;

[SerializeField] private SerializedVector3 _vector3;

[SerializeField] private SerializedQuaternion _quaternion;
```
GameObject extensions

```c#
gameObject.GetOrAddComponent<Rigidbody>();
```
List extensions
```c#
private IList<string> list = new List<string>() { "string1", "string2", "string3" };
list.Random(); // return random Ilist element

list.Shuffle();  // shuffle elements

list.RemoveRandom(); // remove random element

list.IsNullOrEmpty(); // check is list empty or null

```
Transform extensions
```c#

transform.DetachChildren(); // destroys all clildren of this transform

```

Color extensions

```c#

Color color = Color.green;
            
color.SetFullAlpha();

color.SetAlpha(0.5f);

color.SetNoAlpha();

```

Scene Field
===========

Extension for Unity that allows directly assigning scenes in the Inspector. At runtime, the build index of the scene can be retrieved to, for example, load the scene, without hard-coding or maintaining a list of scene paths. Using this field, the scene info will always be updated even when changing its name, folder, or build index. It's as lightweight as possible, and uses a simple custom inspector:

There are 3 sections in the inspector:

*   The slot with the scene asset, similar to any other asset selector in Unity.
*   A text indicating whether the assigned scene is added to builds. Appears in red if the "Required" checkbox is set.
*   A checkbox to indicate whether a scene that exists in builds must be assigned. If it isn't, builds will fail, and an error will appear in the editor when trying to get the scene build index.

The inspector correctly supports editing multiple objects, and the standard bold labels when overriding the fields in prefab instances.

Usage
To add a SceneField to a script, just use the `Scene` class for the field, which is inside the `RDTools` namespace:

```c#

[SerializeField] private Scene _scene;
    
```
At runtime, you can access the build index of the scene using the `Scene.BuildIndex` property:
```c#

SceneManager.LoadScene(scene.BuildIndex);

```
