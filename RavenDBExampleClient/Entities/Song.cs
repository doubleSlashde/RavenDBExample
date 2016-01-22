using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDBExampleClient.Entities {
   class Song {

      public Artist Artist
      {
         get; set;
      }
      public string Title
      {
         get; set;
      }

      public override string ToString() {
         return string.Format($"{nameof(Title)}: {Title} | {nameof(Artist)}: {Artist}");
      }
   }
}
