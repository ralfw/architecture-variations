namespace messagehandling.pipeline
{
    public class QueryOutput : Output {
        public QueryResult Result { get; }

        public QueryOutput(QueryResult result) => Result = result;
    }
}