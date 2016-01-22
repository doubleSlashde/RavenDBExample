using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using RavenDBExampleClient.Entities;
using RavenDBExampleClient.Inidices;
using static System.Console;

namespace RavenDBExampleClient {
   class Program {

      private static readonly IDocumentStore Store = DocumentStoreHolder.Store;

      private const string SongTitle1 = "Ein Kompliment";
      private const string SongTitle2 = "Ich Roque";
      private const string SongTitle3 = "Applaus, Applaus";
      private const string Artist = "Sportfreunde Stiller";

      private const string Id1 = "MyId1";
      private const string Id2 = "MyId2";

      static void Main(string[] args) {
         CreateIndexes();
         CreateData();
         ReadData();
         UpdateData();
         DeleteData();

         WriteLine("\nEnd of Program");
         ReadLine();
      }

      private static void CreateIndexes() {
         new RetrieveSongTitle().Execute(Store);

         WriteLine($"Index {nameof(RetrieveSongTitle)} has been created.");
         WriteDelimiter();
      }

      private static void CreateData() {
         using (var session = Store.OpenSession()) {
            var artist = new Artist { Name = Artist };

            var song1 = new Song { Title = SongTitle1, Artist = artist };
            var song2 = new Song { Title = SongTitle2, Artist = artist };
            var song3 = new Song { Title = SongTitle3, Artist = artist };

            session.Store(artist);
            session.Store(song1, Id1);
            session.Store(song2, Id2);
            session.Store(song3);

            session.SaveChanges();

            WriteLine("The following data records have been created:\n");
            WriteLine($"{nameof(Song)}: {song1}");
            WriteLine($"{nameof(Song)}: {song2}");
            WriteLine($"{nameof(Artist)}: {artist}");
         }
         WriteDelimiter();
      }


      private static void ReadData() {
         WriteLine("The following data records have been read:\n");

         using (var session = Store.OpenSession()) {
            session.Load<Song>();
            var retrievedData = session.Query<Song, RetrieveSongTitle>().Where(x => x.Title == SongTitle2).ToList();
            foreach (var song in retrievedData) {
               WriteLine(song);
            }
         }

         using (var session = Store.OpenSession()) {
            var retrievedData = session.Query<Song>().Where(x => x.Artist.Name == Artist).ToList();
            foreach (var song in retrievedData) {
               WriteLine(song);
            }
         }

         using (var session = Store.OpenSession()) {
            var retrievedData = session.Query<Artist>().Where(x => x.Name == Artist).ToList();
            foreach (var song in retrievedData) {
               WriteLine(song);
            }
         }

         WriteDelimiter();
      }

      private static void UpdateData() {
         WriteLine("The following data record will be updated:\n");

         using (var session = Store.OpenSession()) {
            var retrievedData = session.Load<Song>(Id2);

            WriteLine($"Old\n{retrievedData}");

            retrievedData.Title = "New Title";
            session.Store(retrievedData);
            session.SaveChanges();

            WriteLine($"New\n{retrievedData}");
         }

         WriteDelimiter();
      }

      private static void DeleteData() {
         WriteLine("The following data record will be deleted:\n");

         using (var session = Store.OpenSession()) {
            var retrievedData = session.Load<Song>(Id1);
            session.Delete(retrievedData);
            session.SaveChanges();
            WriteLine($"{retrievedData}");
         }

         WriteDelimiter();
      }

      private static void WriteDelimiter() {
         WriteLine("------------------------------------------------------------");
      }
   }
}
