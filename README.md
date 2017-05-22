# MultiPong [![Build Status](https://travis-ci.org/manio143/MultiPong.svg?branch=master)](https://travis-ci.org/manio143/MultiPong)
A simple multiplayer over TCP pong game. Client uses MonoGame, but could just as well use the console.

### Building
Requirements:

* .NET 4.0+ / Mono 4.0+
* C# 6.0 compatible compiler
* NuGet

```
    git clone https://github.com/manio143/MultiPong
    cd MultiPong
    nuget restore MultiPong.sln
    msbuild MultiPong.sln
```

Or just build it in Visual Studio.

### Running
Right now, the game only works on localhost. So run the `MultiPongServer.exe` and then two `MultiPongClient.exe`s to start the game.
