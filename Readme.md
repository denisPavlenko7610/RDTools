# RDTools

- Use RDTools nameSpace and periodically update the package 🙂
- Actual version - 1.0.0

# Play audio in inspector 
(by default)

![image](https://user-images.githubusercontent.com/13468920/201512579-c8a206aa-1150-4b0f-88c7-61438aea7ef2.png)

# Button

- Add Button attribute for methods to create button
```c#
[Button]
public void DoSomething() => Debug.Log("add params");
    
[Button("Add Params")] //add some name
private void DoSomething() => Debug.Log("add params");

[Button("Add Params", space:30f)] // add space
private void DoSomething() => Debug.Log("add params");

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


# Serialized vectors, quaternions

- UnityEngine.Vector2,3.. is not marked as Serializable. 
When attempting to save a Vector3 variable's value (or a Quaternion for that matter) to disk, you may find that Unity will throw a UnityEngine.Vector3 is not marked as Serializable error. To work around this, you can create a new SerializedVector struct that can be used to serialize your Vector3 and saved to disk.

```c#
[SerializeField] private SerializedVector2 _vector2;

[SerializeField] private SerializedVector3 _vector3;

[SerializeField] private SerializedQuaternion _quaternion;
```
# GameObject extensions

```c#
gameObject.GetOrAddComponent<Rigidbody>();

gameObject.SetParent(newObject);
```
# List extensions
```c#
private IList<string> list = new List<string>() { "string1", "string2", "string3" };
list.Random(); // return random Ilist element

list.Shuffle();  // shuffle elements

list.RemoveRandom(); // remove random element

list.IsNullOrEmpty(); // check is list empty or null

```
# Transform extensions
```c#

transform.DetachChildren(); // destroys all clildren of this transform

```

# Vector Extensions

```cs
// Simple Vector to VectorInt conversions

Vector2 vector2 = new Vector2(2f, 4.2f);

vector2.ToVector2Int(); // returns Vector2Int(2, 4);

Vector3 vector3 = new Vector3(5.2f, 3f, -1f);

vector3.ToVector3Int(); // returns Vector3Int(5, 3, -1);

// With and Add

Vector3 ourVector = new Vector3(4.3f, -3.5f, 0.2f);

ourVector.WithX(41f); // returns Vector3(41f, -3.5f, 0.2f);
ourVector.WithZ(84f); // returns Vector3(4.3f, -3.5f, 84f);

ourVector.AddX(100f); // returns Vector3(104.3f, -3.5f, 0.2f);
ourVector.AddY(100f); // returns Vector3(4.3f, 96.5f, 0.2f);

// Invert

Vector2 myVector = new Vector2(1f, 1f);

myVector.Invert(); // returns Vector2(-1f, -1f);

myVector.InvertX(); // returns Vector2(-1f, 1f);
myVector.InvertY(); // returns Vector2(1f, -1f);
```

# Color extensions

```c#

Color color = Color.green;
            
color.SetFullAlpha();

color.SetAlpha(0.5f);

color.SetNoAlpha();

```

# With Extentions

```c#

bool isTrue = true;
int someValue = 20;

Instantiate(newObject)
    .With(x => x.transform.localPosition = Vector3.zero)  // add chain to set some params
    .With(x => x.transform.rotation = Quaternion.Euler(0, 90, 0), isTrue)  // add condition value with some bool param
    .With(x => x.transform.localScale = Vector3.one, someValue > 10);  // or add condition with some expression

```

Scene Attribute
===========

Extension for Unity that allows directly assigning scenes in the Inspector.

```c#

[SerializeField] private Scene _scene;
    
```
At runtime, you can access the build index of the scene using the `Scene.BuildIndex` property:
```c#

SceneManager.LoadScene(scene.BuildIndex);

```
