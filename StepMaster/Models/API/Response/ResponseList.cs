namespace Domain.Entity.API
{
    public class ResponseList<T>
    {
        public List<T> Result { get; set; }

       public ResponseList() 
        { 
            Result = new List<T>();        
        }
        public ResponseList(List<T> value) 
        { 
         Result = value;        
        }
    }
}
