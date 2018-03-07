# ProtobufIL2CPP2018

## How to test

- Build to a real device.
- Start up and press Load, you should see ???-???-???. This is because you have no binary file yet.
- Press Save, this creates a binary file that contains protobuf binary format in your persistent path. Along with this you should see some data appeard.
- Press Load and this time nothing should change.
- Remember the text.
- Close the app completely and open again, press Load. You should see a previous text appeared. This shows that loading from binary to C# class is successful.

## Status

### Unity 2018.1.0b9
- iOS IL2CPP .NET Standard 2.0 - OK
- Android IL2CPP .NET Standard 2.0 - OK

Uses [protobuf-unity](https://github.com/5argon/protobuf-unity) to help with the compiling.
