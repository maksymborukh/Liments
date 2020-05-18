using Liments.MVC.Core.Entities;
using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Liments.MVC.Core.DBneo4j
{
    public class LimentsNeoContext : ILimentsNeoContext
    {
        private GraphClient _client;


        public LimentsNeoContext()
        {
            _client = new GraphClient(new Uri("http://localhost:7474/db/data/"), "neo4j", "liments");
            _client.Connect();
        }

        public void Create(string username)
        {
            _client.Cypher
                .Create("(n:Person { username: {usern} })")
                .WithParam("usern", username)
                .ExecuteWithoutResults();
        }

        public void CreateRel(string usernameF, string usernameS)
        {
            var query = _client.Cypher
                .Match("(p1:Person { username: {u1} })", "(p2:Person { username: {u2} })")
                .WithParam("u1", usernameF)
                .WithParam("u2", usernameS)
                .CreateUnique("(p1)-[:Follow]->(p2)");
            query.ExecuteWithoutResults();
        }

        public void Delete(string usernameF, string usernameS)
        {
            _client.Cypher
              .Match("(p1:Person {username: {u1}})-[r:Follow]->(p2:Person {username: {u2}})")
              .WithParam("u1", usernameF)
              .WithParam("u2", usernameS)
              .Delete("r")
              .ExecuteWithoutResults();
        }

        public int ShortPath(string usernameF, string usernameS)
        {
            var query = _client.Cypher

                .Match("p = shortestPath((a:Person)-[:Follow*]->(b:Person))")
                .Where((Person a) => a.username == usernameF)
                .AndWhere((Person b) => b.username == usernameS)
                .Return(() => Return.As<IEnumerable<string>>("[n IN nodes(p) | n.username]"));

            IEnumerable<string> t;
            int len;
            try
            {
                t = query.Results.Single();
                len = t.Count() - 1;
            }
            catch
            {
                len = -1;
            }

            return len;
        }
    }
}
