# Facepunch.Steamworks
Another fucking c# Steamworks implementation

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

# Help

Wanna help? Go for it, pull requests, bug reports, yes, do it.

# License

MIT - do whatever you want.
