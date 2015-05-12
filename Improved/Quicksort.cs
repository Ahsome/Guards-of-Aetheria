namespace Improved{
    using System.Collections.Generic;
    internal class Quicksort{
        public static int Partition<T>(List<T> numbers,List<int> indices,int left,int right){//TODO: test
            var pivot=indices[left];
            while(true){
                while(indices[left]<pivot) left++;
                while(indices[right]>pivot) right--;
                if(left<right){
                    var temp=numbers[right];
                    numbers[right]=numbers[left];
                    numbers[left]=temp;
                    var temp2=indices[right];
                    indices[right]=indices[left];
                    indices[left]=temp2;
                } else return right;
            }
        }
        public static void Qsort<T>(List<T> arr,List<int> indices,int left,int right){
            while(true){
                if(left>=right) return;
                var pivot=Partition(arr,indices,left,right);
                if(pivot>1) Qsort(arr,indices,left,pivot-1);
                if(pivot+1<right){
                    left=pivot+1;
                    continue;
                }
                break;
            }
        }
    }
}
