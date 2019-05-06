using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Steamworks
{
	public static class Realm
    {
		//
		// true = serverside
		//
		static Stack<bool> _stack = new Stack<bool>();

		public static bool IsServer => _stack.Count > 0 && _stack.Peek();

		public static bool IsClient	=> _stack.Count == 0 || !_stack.Peek();


		static public IDisposable Server()
		{
			_stack.Push( true );
			return new StackPopper();
		}

		static public IDisposable Client()
		{
			_stack.Push( false );
			return new StackPopper();
		}

		public struct StackPopper : IDisposable
		{
			public void Dispose()
			{
				_stack.Pop();
			}
		}

	}
}
