// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System.Collections.Generic;
using System;

namespace DTTUnityCore
{
    public sealed class GuardsClauses
    {
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ArgumentListNotEmpty<T>(IList<T> list, string argumentName)
        {
            if (list.Count == 0)
            {
                throw new ArgumentException(argumentName + " list must not be empty");
            }
        }

        public static void ArgumentListCountGreaterOrEqualThan<T>(IList<T> list, int count, string argumentName)
        {
            if (list.Count < count)
            {
                throw new ArgumentException(argumentName + " element count must be greater or equal to " + count);
            }
        }

        public static void ArgumentListCountLesserOrEqualThan<T>(IList<T> list, int count, string argumentName)
        {
            if (list.Count > count)
            {
                throw new ArgumentException(argumentName + " element count must be lesser or equal to " + count);
            }
        }

        public static void ArgumentListNotNullOrEmpty<T>(IList<T> list, string argumentName)
        {
            ArgumentNotNull(list, argumentName);
            ArgumentListNotEmpty(list, argumentName);
        }

        public static void ArgumentArrayNotEmpty<T>(T[] array, string argumentName)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException(argumentName + " list must not be empty");
            }
        }

        public static void ArgumentArrayNotNullOrEmpty<T>(T[] array, string argumentName)
        {
            ArgumentNotNull(array, argumentName);
            ArgumentArrayNotEmpty(array, argumentName);
        }

        public static void ArgumentIndexOutOfListRange<T>(int argumentValue, IList<T> list, string argumentName)
        {
            if (IsIndexOutOfListRange(list.Count, argumentValue))
            {
                ThrowArgumenOutOfListRange(argumentValue, argumentName);
            }
        }

        public static void ArgumentIntOutOfListSize(int argumentValue, int listSize, string argumentName)
        {
            if (IsIndexOutOfListRange(listSize, argumentValue))
            {
                ThrowArgumenOutOfListRange(argumentValue, argumentName);
            }
        }

        public static void ArgumentIsNegative(int argumentValue, string argumentName)
        {
            if (argumentValue < 0)
            {
                throw new ArgumentException(argumentName + "Can`t be negative");
            }
        }

        public static void ArgumentIsEqualOrMinorThanZero(int argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
            {
                throw new ArgumentException(argumentName + "Must be greater than zero");
            }
        }

        public static void ArgumentIsEqualOrMinorThanZero(float argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
            {
                throw new ArgumentException(argumentName + "Must be greater than zero");
            }
        }

        public static void ArgumentIsTrue(bool argumentValue, string argumentName)
        {
            if (!argumentValue)
            {
                throw new ArgumentException(argumentName + "Must be True");
            }
        }

        public static void ArgumentIsFalse(bool argumentValue, string argumentName)
        {
            if (argumentValue)
            {
                throw new ArgumentException(argumentName + "Must be False");
            }
        }

        public static void ArgumentsAreDifferent<T>(T argumentValue1, T argumentValue2, string argumentName1, string argumentName2) where T : struct
        {
            if (!EqualityComparer<T>.Default.Equals(argumentValue1, argumentValue2))
            {
                throw new ArgumentException(argumentName1 + " and " + argumentName2 + " can`t be different");
            }
        }

        public static void ArgumentStringNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                ThrowArgumentNullOrEmpty(argumentValue, argumentName);
            }
        }

        public static void ThrowArgumentNullOrEmpty(string argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
            else if (argumentValue == string.Empty)
            {
                throw new ArgumentException(argumentName + "Must not be empty");
            }
        }

        public static void ThrowArgumenOutOfListRange(int argumentValue, string argumentName)
        {
            if (argumentValue < 0)
            {
                throw new ArgumentException(argumentName + "Is a negative index");
            }
            else
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        public static bool IsIndexOutOfListRange(int listSize, int index)
        {
            return (index < 0 || index >= listSize);
        }
    }
    
}


