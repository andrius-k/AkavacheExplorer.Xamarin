using System;
using System.Collections.Generic;
using AkavacheExplorerSample.Objects;
namespace AkavacheExplorerSample
{
    public static class Helpers
    {
        public static IList<Band> GenerateSampleData()
        {
            return new List<Band>
            {
                new Band
                {
                    Id = 1,
                    Name = "Queen",
                    Genre = "Rock",
                    Songs = new List<Song>
                    {
                        new Song
                        {
                            Title = "Hammer To Fall",
                            Length = 3.40
                        },
                        new Song
                        {
                            Title = "Killer Queen",
                            Length = 3.01
                        },
                        new Song
                        {
                            Title = "Radio Ga Ga",
                            Length = 5.43
                        },
                        new Song
                        {
                            Title = "Another One Bites the Dust",
                            Length = 3.35
                        },
                        new Song
                        {
                            Title = "Innuendo",
                            Length = 6.29
                        },
                        new Song
                        {
                            Title = "I Want It All",
                            Length = 4.01
                        }
                    },
                    Musicians = new List<Musician>
                    {
                        new Musician
                        {
                            Name = "Freddie Mercury",
                            Role = Role.Singer
                        },
                        new Musician
                        {
                            Name = "Brian May",
                            Role = Role.Guitarist
                        },
                        new Musician
                        {
                            Name = "Roger Taylor",
                            Role = Role.Drummer
                        },
                        new Musician
                        {
                            Name = "John Deacon",
                            Role = Role.Bassist
                        },
                    },
                },
                new Band
                {
                    Id = 2,
					Name = "Guns N' Roses",
					Genre = "Rock",
					Songs = new List<Song>
					{
						new Song
						{
							Title = "November Rain",
							Length = 8.57
						},
						new Song
						{
							Title = "Patience",
							Length = 5.56
						},
						new Song
						{
							Title = "Knockin on Heaven's Door",
							Length = 5.36
						},
						new Song
						{
							Title = "Yesterdays",
							Length = 3.14
						}
					},
					Musicians = new List<Musician>
					{
						new Musician
						{
							Name = "Axl Rose",
							Role = Role.Singer
						},
						new Musician
						{
							Name = "Slash",
							Role = Role.Guitarist
						},
						new Musician
						{
							Name = "Duff McKagan",
							Role = Role.Bassist
						},
						new Musician
						{
							Name = "Dizzy Reed",
							Role = Role.Keyboardist
						},
						new Musician
						{
							Name = "Richard Fortus",
							Role = Role.Guitarist
						},
						new Musician
						{
							Name = "Frank Ferrer",
							Role = Role.Drummer
						},
						new Musician
						{
							Name = "Melissa Reese",
							Role = Role.Keyboardist
						},
					},
				}
            };
        }
    }
}
