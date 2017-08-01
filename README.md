# Facepunch.Steamworks
Another fucking c# Steamworks implementation

[![Build Status](http://build.facepunch.com/buildStatus/icon?job=Facepunch/Facepunch.Steamworks/master)](http://build.facepunch.com/job/Facepunch/job/Facepunch.Steamworks/job/master/)

## Why

The Steamworks C# implementations I found that were compatible with Unity have worked for a long time. But I hate them all. For a number of different reasons.

* They're not C#, they're just a collection of functions.
* They're not up to date.
* They require a 3rd party native dll.
* They can't be compiled into a standalone dll (in Unity).
* They have a license.

C# is meant to make things easier. So lets try to wrap it up in a way that makes it all easier.

## What

So say you want to print a list of your friends?

```csharp
foreach ( var friend in client.Friends.All )
{
    Console.WriteLine( "{0}: {1}", friend.Id, friend.Name );
}
```

But what if you want to get a list of friends playing the same game that we're playing?

```csharp
foreach ( var friend in client.Friends.All.Where( x => x.IsPlayingThisGame ) )
{
    Console.WriteLine( "{0}: {1}", friend.Id, friend.Name );
}
```

You can view examples of everything in the Facepunch.Steamworks.Test project.

# Usage

## Client

Compile and add the library to your project. To create a client you can do this.

```csharp
var client = new Facepunch.Steamworks.Client( 252490 );
```

Replace 252490 with the appid of your game. This should be a singleton - you should only create one client, right at the start of your game.

The client is disposable, so when you're closing your game should probably call..

```csharp
client.Dispose();
```

Or use it in a using block if you can.


## Server

To create a server do this.

```csharp
var server = new Facepunch.Steamworks.Server( 252490, 0, 28015, true, "MyGame453" );
```

This will register a secure server for game 252490, any ip, port 28015. Again, more usage in the Facepunch.Steamworks.Test project.

## Lobby

To create a Lobby do this.
```csharp
var client = new Facepunch.Steamworks.Client( 252490 );
client.Lobby.Create(Steamworks.Lobby.Type.Public, 10);
```

Created lobbies are auto-joined, but if you want to find a friend's lobby, you'd call
```csharp
client.LobbyList.Refresh();
//wait for the callback
client.LobbyList.OnLobbiesUpdated = () =>
{
    if (client.LobbyList.Finished)
    {
        foreach (LobbyList.Lobby lobby in client.LobbyList.Lobbies)
        {
            Console.WriteLine($"Found Lobby: {lobby.Name}");
        }
    }
};
//join a lobby you found
client.Lobby.Join(LobbyList.Lobbies[0]);
```


# Unity

Yeah this works under Unity. That's half the point of it.

There's another repo with an example project with it working in Unity. You can find it [here](https://github.com/Facepunch/Facepunch.Steamworks.Unity/blob/master/Assets/Scripts/SteamTest.cs).

The TLDR is before you create the Client or the Server, call this to let Facepunch.Steamworks know which platform we're on - because it can't tell the difference between osx and linux by itself.

```csharp
Facepunch.Steamworks.Config.ForUnity( Application.platform.ToString() );
```

You'll also want to put steam_api64.dll and steam_appid.txt (on windows 64) in your project root next to Assets.

# Help

Wanna help? Go for it, pull requests, bug reports, yes, do it.

You can also hit up the [Steamworks Thread](http://steamcommunity.com/groups/steamworks/discussions/0/1319961618833314524/) for help/discussion.

# License

MIT - do whatever you want.
