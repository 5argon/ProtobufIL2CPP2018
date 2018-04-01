# ProtobufIL2CPP2018

## How to test

### Testing serialization - deserialization

- Build to a real device.
- Start up and press Load, you should see ???-???-???. This is because you have no binary file yet.
- Press Save, this creates a binary file that contains protobuf binary format in your persistent path. Along with this you should see some data appeard.
- Press Load and this time nothing should change.
- Remember the text.
- Close the app completely and open again, press Load. You should see a previous text appeared. This shows that loading from binary to C# class is successful.

### Testing reflection

- Just press the Test Reflection button. If everything works the button will becomes **{ "valuePt" : 500 }**. The button calls `ToString()` of Protobuf, that makes `JsonFormatter` call `Descriptor` that needs reflection.

## Status

### Unity 2018.1.0b13

- iOS IL2CPP .NET Standard 2.0 - Reflection failing.

```
ExecutionEngineException: Attempting to call method 'Google.Protobuf.Reflection.ReflectionUtil+ReflectionHelper`2[[AbsolutePosition, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null],[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]::.ctor' for which no ahead of time (AOT) code was generated.
  at System.Reflection.MonoCMethod.InternalInvoke (System.Object obj, System.Object[] parameters) [0x00000] in <00000000000000000000000000000000>:0
  at System.Activator.CreateInstance (System.Type type, System.Boolean nonPublic) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.ReflectionUtil.GetReflectionHelper (System.Type t1, System.Type t2) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.ReflectionUtil.CreateFuncIMessageObject (System.Reflection.MethodInfo method) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FieldAccessorBase..ctor (System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.SingleFieldAccessor..ctor (System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FieldDescriptor.CreateAccessor () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FieldDescriptor.CrossLink () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.MessageDescriptor.CrossLink () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FileDescriptor.CrossLink () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FileDescriptor.BuildFrom (Google.Protobuf.ByteString descriptorData, Google.Protobuf.Reflection.FileDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor[] dependencies, System.Boolean allowUnknownDependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FileDescriptor.FromGeneratedCode (System.Byte[] descriptorData, Google.Protobuf.Reflection.FileDescriptor[] dependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo) [0x00000] in <00000000000000000000000000000000>:0
  at AbsolutePositionProtoReflection..cctor () [0x00000] in <00000000000000000000000000000000>:0
  at AbsolutePosition.get_Descriptor () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.JsonFormatter.Format (Google.Protobuf.IMessage message, System.IO.TextWriter writer) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.JsonFormatter.Format (Google.Protobuf.IMessage message) [0x00000] in <00000000000000000000000000000000>:0
  at Logic.ReflectionTest () [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.Events.UnityEvent.Invoke () [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchPress (UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released) [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchEvents () [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.StandaloneInputModule.Process () [0x00000] in <00000000000000000000000000000000>:0
Rethrow as TargetInvocationException: Exception has been thrown by the target of an invocation.
  at System.Reflection.MonoCMethod.InternalInvoke (System.Object obj, System.Object[] parameters) [0x00000] in <00000000000000000000000000000000>:0
  at System.Activator.CreateInstance (System.Type type, System.Boolean nonPublic) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.ReflectionUtil.GetReflectionHelper (System.Type t1, System.Type t2) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.ReflectionUtil.CreateFuncIMessageObject (System.Reflection.MethodInfo method) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FieldAccessorBase..ctor (System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.SingleFieldAccessor..ctor (System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FieldDescriptor.CreateAccessor () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FieldDescriptor.CrossLink () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.MessageDescriptor.CrossLink () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FileDescriptor.CrossLink () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FileDescriptor.BuildFrom (Google.Protobuf.ByteString descriptorData, Google.Protobuf.Reflection.FileDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor[] dependencies, System.Boolean allowUnknownDependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.Reflection.FileDescriptor.FromGeneratedCode (System.Byte[] descriptorData, Google.Protobuf.Reflection.FileDescriptor[] dependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo) [0x00000] in <00000000000000000000000000000000>:0
  at AbsolutePositionProtoReflection..cctor () [0x00000] in <00000000000000000000000000000000>:0
  at AbsolutePosition.get_Descriptor () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.JsonFormatter.Format (Google.Protobuf.IMessage message, System.IO.TextWriter writer) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.JsonFormatter.Format (Google.Protobuf.IMessage message) [0x00000] in <00000000000000000000000000000000>:0
  at Logic.ReflectionTest () [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.Events.UnityEvent.Invoke () [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchPress (UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released) [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchEvents () [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.StandaloneInputModule.Process () [0x00000] in <00000000000000000000000000000000>:0
Rethrow as TypeInitializationException: The type initializer for 'AbsolutePositionProtoReflection' threw an exception.
  at AbsolutePosition.get_Descriptor () [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.JsonFormatter.Format (Google.Protobuf.IMessage message, System.IO.TextWriter writer) [0x00000] in <00000000000000000000000000000000>:0
  at Google.Protobuf.JsonFormatter.Format (Google.Protobuf.IMessage message) [0x00000] in <00000000000000000000000000000000>:0
  at Logic.ReflectionTest () [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.Events.UnityEvent.Invoke () [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchPress (UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released) [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchEvents () [0x00000] in <00000000000000000000000000000000>:0
  at UnityEngine.EventSystems.StandaloneInputModule.Process () [0x00000] in <00000000000000000000000000000000>:0
UnityEngine.EventSystems.ExecuteEvents:Execute(GameObject, BaseEventData, EventFunction`1)
UnityEngine.EventSystems.StandaloneInputModule:ProcessTouchPress(PointerEventData, Boolean, Boolean)
UnityEngine.EventSystems.StandaloneInputModule:ProcessTouchEvents()
UnityEngine.EventSystems.StandaloneInputModule:Process()
```


- Android IL2CPP .NET Standard 2.0 - OK

Uses [protobuf-unity](https://github.com/5argon/protobuf-unity) to help with the compiling.
