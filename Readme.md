# RDTools
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/denisPavlenko7610/RDTools/blob/master/LICENSE.md)

- Use RDTools nameSpace and periodically update the package 🙂
- Actual version - 1.1.0
- Minimal Unity version - 2020.2

(some assets were taken from these repositories (https://github.com/baba-s/awesome-unity-open-source-on-github) and have been seriously modified according to the Mit or Apache 2.0 licenses)

# Play audio in inspector 
(by default)

![image](https://user-images.githubusercontent.com/13468920/201512579-c8a206aa-1150-4b0f-88c7-61438aea7ef2.png)


Add scene links (by default)
===========

Extension for Unity that allows directly assigning scenes in the Inspector.

```c#

[SerializeField] private Scene _scene;
    
```
At runtime, you can access the build index of the scene using the `Scene.BuildIndex` property:
```c#

SceneManager.LoadScene(scene.BuildIndex);

```

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

# With Extensions

```c#

bool isTrue = true;
int someValue = 20;

Instantiate(newObject)
    .With(x => x.transform.localPosition = Vector3.zero)  // add chain to set some params
    .With(x => x.transform.rotation = Quaternion.Euler(0, 90, 0), isTrue)  // add condition value with some bool param
    .With(x => x.transform.localScale = Vector3.one, someValue > 10);  // or add condition with some expression

```
## Special Attributes

### AllowNesting
This attribute must be used in some cases when you want meta attributes to work inside serializable nested structs or classes.
You can check in which cases you need to use it [here](https://dbrizov.github.io/na-docs/attributes/special_attributes/allow_nesting.html).

```csharp
public class NaughtyComponent : MonoBehaviour
{
    public MyStruct myStruct;
}

[System.Serializable]
public struct MyStruct
{
    public bool enableFlag;

    [EnableIf("enableFlag")]
    [AllowNesting] // Because it's nested we need to explicitly allow nesting
    public int integer;
}
```

## Drawer Attributes
Provide special draw options to serialized fields.
A field can have only one DrawerAttribute. If a field has more than one, only the bottom one will be used.

### AnimatorParam
Select an Animator paramater via dropdown interface.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	public Animator someAnimator;

	[AnimatorParam("someAnimator")]
	public int paramHash;

	[AnimatorParam("someAnimator")]
	public string paramName;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/AnimatorParam_Inspector.png)

### Button
A method can be marked as a button. A button appears in the inspector and executes the method if clicked.
Works both with instance and static methods.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[Button]
	private void MethodOne() { }

	[Button("Button Text")]
	private void MethodTwo() { }
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/Button_Inspector.png)

### CurveRange
Set bounds and modify curve color for AnimationCurves

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[CurveRange(-1, -1, 1, 1)]
	public AnimationCurve curve;
	
	[CurveRange(EColor.Orange)]
	public AnimationCurve curve1;
	
	[CurveRange(0, 0, 5, 5, EColor.Red)]
	public AnimationCurve curve2;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/CurveRange_Inspector.png)

### Dropdown
Provides an interface for dropdown value selection.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[Dropdown("intValues")]
	public int intValue;

	[Dropdown("StringValues")]
	public string stringValue;

	[Dropdown("GetVectorValues")]
	public Vector3 vectorValue;

	private int[] intValues = new int[] { 1, 2, 3, 4, 5 };

	private List<string> StringValues { get { return new List<string>() { "A", "B", "C", "D", "E" }; } }

	private DropdownList<Vector3> GetVectorValues()
	{
		return new DropdownList<Vector3>()
		{
			{ "Right",   Vector3.right },
			{ "Left",    Vector3.left },
			{ "Up",      Vector3.up },
			{ "Down",    Vector3.down },
			{ "Forward", Vector3.forward },
			{ "Back",    Vector3.back }
		};
	}
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/Dropdown_Inspector.gif)

### EnumFlags
Provides dropdown interface for setting enum flags.

```csharp
public enum Direction
{
	None = 0,
	Right = 1 << 0,
	Left = 1 << 1,
	Up = 1 << 2,
	Down = 1 << 3
}

public class NaughtyComponent : MonoBehaviour
{
	[EnumFlags]
	public Direction flags;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/EnumFlags_Inspector.png)

### Expandable
Make scriptable objects expandable.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[Expandable]
	public ScriptableObject scriptableObject;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/Expandable_Inspector.png)

### HorizontalLine

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[HorizontalLine(color: EColor.Red)]
	public int red;

	[HorizontalLine(color: EColor.Green)]
	public int green;

	[HorizontalLine(color: EColor.Blue)]
	public int blue;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/HorizontalLine_Inspector.png)

### InfoBox
Used for providing additional information.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[InfoBox("This is my int", EInfoBoxType.Normal)]
	public int myInt;

	[InfoBox("This is my float", EInfoBoxType.Warning)]
	public float myFloat;

	[InfoBox("This is my vector", EInfoBoxType.Error)]
	public Vector3 myVector;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/InfoBox_Inspector.png)

### InputAxis
Select an input axis via dropdown interface.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[InputAxis]
	public string inputAxis;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/InputAxis_Inspector.png)

### Layer
Select a layer via dropdown interface.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[Layer]
	public string layerName;

	[Layer]
	public int layerIndex;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/Layer_Inspector.png)

### MinMaxSlider
A double slider. The **min value** is saved to the **X** property, and the **max value** is saved to the **Y** property of a **Vector2** field.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[MinMaxSlider(0.0f, 100.0f)]
	public Vector2 minMaxSlider;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/MinMaxSlider_Inspector.png)

### ProgressBar
```csharp
public class NaughtyComponent : MonoBehaviour
{
	[ProgressBar("Health", 300, EColor.Red)]
	public int health = 250;

	[ProgressBar("Mana", 100, EColor.Blue)]
	public int mana = 25;

	[ProgressBar("Stamina", 200, EColor.Green)]
	public int stamina = 150;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/ProgressBar_Inspector.png)

### ReorderableList
Provides array type fields with an interface for easy reordering of elements.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[ReorderableList]
	public int[] intArray;

	[ReorderableList]
	public List<float> floatArray;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/ReorderableList_Inspector.gif)

### ResizableTextArea
A resizable text area where you can see the whole text.
Unlike Unity's **Multiline** and **TextArea** attributes where you can see only 3 rows of a given text, and in order to see it or modify it you have to manually scroll down to the desired row.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[ResizableTextArea]
	public string resizableTextArea;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/ResizableTextArea_Inspector.gif)

### Scene
Select a scene from the build settings via dropdown interface.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[Scene]
	public string bootScene; // scene name

	[Scene]
	public int tutorialScene; // scene index
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/Scene_Inspector.png)

### ShowAssetPreview
Shows the texture preview of a given asset (Sprite, Prefab...).

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[ShowAssetPreview]
	public Sprite sprite;

	[ShowAssetPreview(128, 128)]
	public GameObject prefab;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/ShowAssetPreview_Inspector.png)

### ShowNativeProperty
Shows native C# properties in the inspector.
All native properties are displayed at the bottom of the inspector after the non-serialized fields and before the method buttons.
It supports only certain types **(bool, int, long, float, double, string, Vector2, Vector3, Vector4, Color, Bounds, Rect, UnityEngine.Object)**.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	public List<Transform> transforms;

	[ShowNativeProperty]
	public int TransformsCount => transforms.Count;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/ShowNativeProperty_Inspector.png)

### ShowNonSerializedField
Shows non-serialized fields in the inspector.
All non-serialized fields are displayed at the bottom of the inspector before the method buttons.
Keep in mind that if you change a non-static non-serialized field in the code - the value in the inspector will be updated after you press **Play** in the editor.
There is no such issue with static non-serialized fields because their values are updated at compile time.
It supports only certain types **(bool, int, long, float, double, string, Vector2, Vector3, Vector4, Color, Bounds, Rect, UnityEngine.Object)**.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[ShowNonSerializedField]
	private int myInt = 10;

	[ShowNonSerializedField]
	private const float PI = 3.14159f;

	[ShowNonSerializedField]
	private static readonly Vector3 CONST_VECTOR = Vector3.one;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/ShowNonSerializedField_Inspector.png)

### SortingLayer
Select a sorting layer via dropdown interface.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[SortingLayer]
	public string layerName;

	[SortingLayer]
	public int layerId;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/SortingLayer_Inspector.png)

### Tag
Select a tag via dropdown interface.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[Tag]
	public string tagField;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/Tag_Inspector.png)

## Meta Attributes
Give the fields meta data. A field can have more than one meta attributes.

### BoxGroup
Surrounds grouped fields with a box.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[BoxGroup("Integers")]
	public int firstInt;
	[BoxGroup("Integers")]
	public int secondInt;

	[BoxGroup("Floats")]
	public float firstFloat;
	[BoxGroup("Floats")]
	public float secondFloat;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/BoxGroup_Inspector.png)

### Foldout
Makes a foldout group.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[Foldout("Integers")]
	public int firstInt;
	[Foldout("Integers")]
	public int secondInt;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/Foldout_Inspector.gif)

### EnableIf / DisableIf
```csharp
public class NaughtyComponent : MonoBehaviour
{
	public bool enableMyInt;

	[EnableIf("enableMyInt")]
	public int myInt;

	[EnableIf("Enabled")]
	public float myFloat;

	[EnableIf("NotEnabled")]
	public Vector3 myVector;

	public bool Enabled() { return true; }

	public bool NotEnabled => false;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/EnableIf_Inspector.gif)

You can have more than one condition.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	public bool flag0;
	public bool flag1;

	[EnableIf(EConditionOperator.And, "flag0", "flag1")]
	public int enabledIfAll;

	[EnableIf(EConditionOperator.Or, "flag0", "flag1")]
	public int enabledIfAny;
}
```

### ShowIf / HideIf
```csharp
public class NaughtyComponent : MonoBehaviour
{
	public bool showInt;

	[ShowIf("showInt")]
	public int myInt;

	[ShowIf("AlwaysShow")]
	public float myFloat;

	[ShowIf("NeverShow")]
	public Vector3 myVector;

	public bool AlwaysShow() { return true; }

	public bool NeverShow => false;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/ShowIf_Inspector.gif)

You can have more than one condition.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	public bool flag0;
	public bool flag1;

	[ShowIf(EConditionOperator.And, "flag0", "flag1")]
	public int showIfAll;

	[ShowIf(EConditionOperator.Or, "flag0", "flag1")]
	public int showIfAny;
}
```

### Label
Override default field label.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[Label("Short Name")]
	public string veryVeryLongName;

	[Label("RGB")]
	public Vector3 vectorXYZ;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/Label_Inspector.png)

### OnValueChanged
Detects a value change and executes a callback.
Keep in mind that the event is detected only when the value is changed from the inspector.
If you want a runtime event, you should probably use an event/delegate and subscribe to it.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[OnValueChanged("OnValueChangedCallback")]
	public int myInt;

	private void OnValueChangedCallback()
	{
		Debug.Log(myInt);
	}
}
```

### ReadOnly
Make a field read only.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[ReadOnly]
	public Vector3 forwardVector = Vector3.forward;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/ReadOnly_Inspector.png)

## Validator Attributes
Used for validating the fields. A field can have infinite number of validator attributes.

### MinValue / MaxValue
Clamps integer and float fields.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[MinValue(0), MaxValue(10)]
	public int myInt;

	[MinValue(0.0f)]
	public float myFloat;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/MinValueMaxValue_Inspector.gif)

### Required
Used to remind the developer that a given reference type field is required.

```csharp
public class NaughtyComponent : MonoBehaviour
{
	[Required]
	public Transform myTransform;

	[Required("Custom required text")]
	public GameObject myGameObject;
}
```

![inspector](https://github.com/dbrizov/NaughtyAttributes/blob/master/Assets/NaughtyAttributes/Documentation~/Required_Inspector.png)

### ValidateInput
The most powerful ValidatorAttribute.

```csharp
public class _NaughtyComponent : MonoBehaviour
{
	[ValidateInput("IsNotNull")]
	public Transform myTransform;

	[ValidateInput("IsGreaterThanZero", "myInteger must be greater than zero")]
	public int myInt;

	private bool IsNotNull(Transform tr)
	{
		return tr != null;
	}

	private bool IsGreaterThanZero(int value)
	{
		return value > 0;
	}
}
```

