namespace Improved{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    public static class Lists{
        public static List<T> List<T>(params T[] i) {return i.ToList();}
        public static Dictionary<TKey,TValue> Dict<TKey,TValue>(params KeyValuePair<TKey,TValue>[] i){
            return i.ToDictionary(k=>k.Key,k=>k.Value);
        }
        public static Dictionary<TKey,TValue> ToDict<TKey,TValue>(this TKey[] l,Func<TKey,TValue> f){
            return l.ToDictionary(k=>k,f);
        }
        public static LinkedList<T> LList<T>(params T[] i) {return new LinkedList<T>(i);}
        public static LinkedListNode<T> Lln<T>(T i) {return new LinkedListNode<T>(i);}
        public static HashSet<T> HSet<T>(params T[] i) {return new HashSet<T>(i);}
        public static SortedDictionary<TKey,TValue> SDict<TKey,TValue>(params KeyValuePair<TKey,TValue>[] i){
            return new SortedDictionary<TKey,TValue>(Dict(i));
        }
        public static SortedList<TKey,TValue> SList<TKey,TValue>(params KeyValuePair<TKey,TValue>[] i){
            return new SortedList<TKey,TValue>(Dict(i));
        }
        public static SortedSet<T> SSet<T>(params T[] i) {return new SortedSet<T>(i);}
        public static Stack<T> Stack<T>(params T[] i) {return new Stack<T>(i);}
        public static Queue<T> Queue<T>(params T[] i) {return new Queue<T>(i);}
        public static KeyValuePair<TKey,TValue> Kvp<TKey,TValue>(TKey key,TValue value){
            return new KeyValuePair<TKey,TValue>(key,value);
        }
        public static BindingList<T> BList<T>(params T[] i) {return new BindingList<T>(i);}
        public static ConcurrentBag<T> CBag<T>(params T[] i) {return new ConcurrentBag<T>(i);}
        public static ConcurrentDictionary<TKey,TValue> CDict<TKey,TValue>(params KeyValuePair<TKey,TValue>[] i){
            return new ConcurrentDictionary<TKey,TValue>(i);
        }
        public static ConcurrentQueue<T> CQueue<T>(params T[] i) {return new ConcurrentQueue<T>(i);}
        public static ConcurrentStack<T> CStack<T>(params T[] i) {return new ConcurrentStack<T>(i);}
        public static BlockingCollection<T> BColl<T>(this T i) where T:IProducerConsumerCollection<T>{
            return new BlockingCollection<T>(i);
        }
        public static Collection<T> Coll<T>(params T[] i) {return new Collection<T>(i);}
        public static ObservableCollection<T> OColl<T>(params T[] i) {return new ObservableCollection<T>(i);}
        public static ReadOnlyCollection<T> RoColl<T>(params T[] i) {return new ReadOnlyCollection<T>(i);}
        public static ReadOnlyDictionary<TKey,TValue> RoDict<TKey,TValue>(params KeyValuePair<TKey,TValue>[] i){
            return new ReadOnlyDictionary<TKey,TValue>(Dict(i));
        }
        public static ReadOnlyObservableCollection<T> RooColl<T>(params T[] i){
            return new ReadOnlyObservableCollection<T>(OColl(i));
        }
        public static ArraySegment<T> ASeg<T>(int offset,int count,params T[] i){
            return new ArraySegment<T>(i,offset,count);
        }
        public static WeakReference<T> WRef<T>(T i) where T:class {return new WeakReference<T>(i);}
        public static Tuple<T> Tuple<T>(T i) {return new Tuple<T>(i);}
        public static Tuple<T1,T2> Tuple<T1,T2>(T1 i1,T2 i2) {return new Tuple<T1,T2>(i1,i2);}
        public static Tuple<T1,T2,T3> Tuple<T1,T2,T3>(T1 i1,T2 i2,T3 i3) {return new Tuple<T1,T2,T3>(i1,i2,i3);}
        public static Tuple<T1,T2,T3,T4> Tuple<T1,T2,T3,T4>(T1 i1,T2 i2,T3 i3,T4 i4){
            return new Tuple<T1,T2,T3,T4>(i1,i2,i3,i4);
        }
        public static Tuple<T1,T2,T3,T4,T5> Tuple<T1,T2,T3,T4,T5>(T1 i1,T2 i2,T3 i3,T4 i4,T5 i5){
            return new Tuple<T1,T2,T3,T4,T5>(i1,i2,i3,i4,i5);
        }
        public static Tuple<T1,T2,T3,T4,T5,T6> Tuple<T1,T2,T3,T4,T5,T6>(T1 i1,T2 i2,T3 i3,T4 i4,T5 i5,T6 i6){
            return new Tuple<T1,T2,T3,T4,T5,T6>(i1,i2,i3,i4,i5,i6);
        }
        public static Tuple<T1,T2,T3,T4,T5,T6,T7> Tuple<T1,T2,T3,T4,T5,T6,T7>(T1 i1,T2 i2,T3 i3,T4 i4,T5 i5,T6 i6,T7 i7){
            return new Tuple<T1,T2,T3,T4,T5,T6,T7>(i1,i2,i3,i4,i5,i6,i7);
        }
        public static Tuple<T1,T2,T3,T4,T5,T6,T7,TRest> Tuple<T1,T2,T3,T4,T5,T6,T7,TRest>(T1 i1,
            T2 i2,
            T3 i3,
            T4 i4,
            T5 i5,
            T6 i6,
            T7 i7,
            TRest iRest){
            return new Tuple<T1,T2,T3,T4,T5,T6,T7,TRest>(i1,i2,i3,i4,i5,i6,i7,iRest);
        }
    }
}
