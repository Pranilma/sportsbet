using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CodeChallenge.Tests")]
namespace CodeChallenge.Service.Exceptions
{
    internal class PositionDoesntExistException : Exception
    {
    }
}
