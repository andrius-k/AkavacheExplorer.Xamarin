using System;
using System.Collections.Generic;
namespace AkavacheExplorerSample.Objects
{
    public class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public string Genre { get; set; }

        public IList<Musician> Musicians { get; set; }
        public IList<Song> Songs { get; set; }
    }
}
