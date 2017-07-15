using System;

namespace FP3
{
    public class Maybe<T>
    {
        private T obj = default(T);
        private bool isNothing = true;

        public Maybe() { }

        public Maybe(T obj)
        {
            this.obj = obj;
            this.isNothing = false;
        }
        
        public static Maybe<T> Nothing()
        {
            return new Maybe<T>();
        }
        
        public static Maybe<T> Just(T obj)
        {
            return new Maybe<T>(obj);
        }

        // convert a Maybe<Maybe<TValue>> into a Maybe<TValue>
        public static Maybe<T> Join(Maybe<Maybe<T>> MaybeMaybe)
        {
            return MaybeMaybe.isNothing ? Nothing() : MaybeMaybe.obj;
        }

        // functor
        public Maybe<T2> FMap<T2>(Func<T, T2> functor)
        {
            return isNothing
                ? Maybe<T2>.Nothing()
                : Maybe<T2>.Just(functor(obj));
        }

        // applicative - maybe applies a maybe<f[m]> maybe<x[n]>
        // returns a Maybe 
        public Maybe<T2> LiftA<T2>(Maybe<Func<T, T2>> applicative)
        {
            return Maybe<T2>.Join(applicative.FMap(functor => FMap(functor)));
        }

        // monad - returns a joined monad of the result
        public Maybe<T2> LiftM<T2>(Func<T, Maybe<T2>> monad)
        {
            return Maybe<T2>.Join(FMap(monad));
        }

        public override string ToString()
        {
            return isNothing
                ? "Nothing"
                : "Just " + obj.ToString();
        }

        public bool IsNothing()
        {
            return isNothing;
        }

        public T PureValue()
        {
            return obj;
        }
    }
}