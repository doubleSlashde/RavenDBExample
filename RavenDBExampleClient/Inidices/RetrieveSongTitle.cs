using System.Linq;
using Raven.Client.Indexes;
using RavenDBExampleClient.Entities;

namespace RavenDBExampleClient.Inidices {
   class RetrieveSongTitle : AbstractIndexCreationTask<Song> {
      public RetrieveSongTitle() {
         Map = songs => from song in songs
                        select new {
                           song.Title
                        };
      }
   }
}
