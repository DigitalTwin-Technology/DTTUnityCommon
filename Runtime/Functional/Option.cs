// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using DTTUnityCommon.DataStructs;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DTTUnityCommon.Functional
{
    public readonly struct Option<T>
    {
        public static Option<T> None { get; } = new Option<T>();
        public static Option<T> Some(T value) => new Option<T>(value);

        private readonly T _value;
        private readonly bool _isSome;


        Option(T value)
        {
            _value = value;
            _isSome = true;
        }

        public bool IsSome([MaybeNullWhen(false)] out T value)
        {
            value = _value;
            return _isSome;
        }

        public Option<U> Map<U>(Func<T, U> f)
        {
            return _isSome ? Option<U>.Some(f(_value)) : Option<U>.None;
        }

        public Option<U> Bind<U>(Func<T, Option<U>> f)
        {
            return _isSome ? f(_value) : Option<U>.None;
        }

        public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none)
        {
            return _isSome ? some(_value) : none();
        }

        internal object Match(Func<DataNodeBase, DataNodeBase> value, DataNodeBase dataNodeBase)
        {
            throw new NotImplementedException();
        }
    }
}

