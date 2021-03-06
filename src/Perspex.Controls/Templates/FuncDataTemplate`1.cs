﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;

namespace Perspex.Controls.Templates
{
    /// <summary>
    /// Builds a control for a piece of data of specified type.
    /// </summary>
    /// <typeparam name="T">The type of the template's data.</typeparam>
    public class FuncDataTemplate<T> : FuncDataTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuncDataTemplate{T}"/> class.
        /// </summary>
        /// <param name="build">
        /// A function which when passed an object of <typeparamref name="T"/> returns a control.
        /// </param>
        public FuncDataTemplate(Func<T, IControl> build)
            : base(typeof(T), CastBuild(build))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncDataTemplate{T}"/> class.
        /// </summary>
        /// <param name="match">
        /// A function which determines whether the data template matches the specified data.
        /// </param>
        /// <param name="build">
        /// A function which when passed an object of <typeparamref name="T"/> returns a control.
        /// </param>
        public FuncDataTemplate(Func<T, bool> match, Func<T, IControl> build)
            : base(CastMatch(match), CastBuild(build))
        {
        }

        /// <summary>
        /// Casts a stongly typed match function to a weakly typed one.
        /// </summary>
        /// <param name="f">The strongly typed function.</param>
        /// <returns>The weakly typed function.</returns>
        private static Func<object, bool> CastMatch(Func<T, bool> f)
        {
            return o => (o is T) ? f((T)o) : false;
        }

        /// <summary>
        /// Casts a stongly typed build function to a weakly typed one.
        /// </summary>
        /// <typeparam name="TResult">The strong data type.</typeparam>
        /// <param name="f">The strongly typed function.</param>
        /// <returns>The weakly typed function.</returns>
        private static Func<object, TResult> CastBuild<TResult>(Func<T, TResult> f)
        {
            return o => f((T)o);
        }
    }
}