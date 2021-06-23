using MediatR;
using System;

namespace Ngx.Monorepo.Framework.Utilities.Commands
{
    /// <summary>
    /// Identified Commands is a wrapper around Commands which allows you to assign 
    /// a unique value to the command that is being send in order to avoid duplicate 
    /// invocation of the command.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    public class IdentifiedCommand<T, R> : IRequest<R>
        where T : IRequest<R>
    {
        /// <summary>
        /// Command that is being wrapped and needs to be idempotent.
        /// </summary>
        public T Command { get; }

        /// <summary>
        /// Unique identifier of the Command.
        /// </summary>
        public Guid Id { get; }

        public IdentifiedCommand(T command, Guid id)
        {
            Command = command;
            Id = id;
        }
    }
}
