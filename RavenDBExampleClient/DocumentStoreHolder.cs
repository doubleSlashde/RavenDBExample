using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;

namespace RavenDBExampleClient {
   class DocumentStoreHolder {
      private static readonly Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

      public static IDocumentStore Store => store.Value;

      private static IDocumentStore CreateStore() {
         var concreteStore = new DocumentStore() {
            Url = "http://localhost:8080/",
            DefaultDatabase = "RavenDBTest"
         }.Initialize();

         return concreteStore;
      }
   }
}
